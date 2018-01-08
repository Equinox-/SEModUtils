using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Equinox.Utils.Session;
using Sandbox.Game;
using Sandbox.ModAPI;
using VRage.Collections;
using VRage.Utils;

namespace Equinox.Utils.Command
{
    public class CommandDispatchComponent : LoggingSessionComponent
    {
        private readonly Dictionary<string, Command> m_commands = new Dictionary<string, Command>();

        public static readonly Type[] SuppliedDeps = { typeof(CommandDispatchComponent) };
        public override IEnumerable<Type> SuppliedComponents => SuppliedDeps;

        protected override void Attach()
        {
            base.Attach();
            lock (m_commands)
                m_commands.Clear();
            if (Utilities.IsDecisionMaker)
                MyAPIGateway.Utilities.MessageRecieved += HandleGlobalCommand;
            if (Utilities.IsController)
                MyAPIGateway.Utilities.MessageEntered += HandleLocalCommand;
        }

        protected override void Detach()
        {
            base.Detach();
            if (Utilities.IsDecisionMaker)
                MyAPIGateway.Utilities.MessageRecieved -= HandleGlobalCommand;
            if (Utilities.IsController)
                MyAPIGateway.Utilities.MessageEntered -= HandleLocalCommand;
            lock (m_commands)
                m_commands.Clear();
        }

        public void AddCommand(Command c)
        {
            lock (m_commands)
            {
                foreach (var s in c.Names)
                    if (m_commands.ContainsKey(s))
                        throw new ArgumentException("Command with name " + c + " already exists");
                foreach (var s in c.Names)
                {
                    Log(MyLogSeverity.Debug, "Registering command {0} under {1}", c.GetType().Name, s);
                    m_commands[s] = c;
                }
            }
        }

        public void RemoveCommand(Command c)
        {
            lock (m_commands)
            {
                foreach (var s in c.Names)
                {
                    Log(MyLogSeverity.Debug, "Unregistering command {0} under {1}", c.GetType().Name, s);
                    m_commands.Remove(s);
                }
            }
        }

        private void HandleLocalCommand(string msg, ref bool sendToOthers)
        {
            if (msg.Length == 0 || msg[0] != '/') return;
            var cmdFeedback = new CommandFeedback((format, fields) =>
            {
                MyAPIGateway.Utilities.ShowMessage("EqUtils", string.Format(format, fields));
            });
            try
            {
                var args = ParseArguments(msg, 1);
                Command cmd;
                lock (m_commands)
                    if (!m_commands.TryGetValue(args[0], out cmd))
                    {
                        Log(MyLogSeverity.Debug, "Unknown command {0}", args[0]);
                        return;
                    }

                var player = MyAPIGateway.Session.Player;
                if (player == null)
                {
                    Log(MyLogSeverity.Warning, "Attempted to run a local command without a player.");
                    return;
                }
                sendToOthers = false;
                if (!cmd.AllowedSessionType.Flagged(MyAPIGateway.Session.SessionType()))
                {
                    Log(MyLogSeverity.Debug, "Unable to run {0} on a session of type {1}; it requires type {2}", args[0], MyAPIGateway.Session.SessionType(), cmd.AllowedSessionType);
                    return;
                }
                if (!cmd.CanPromotionLevelUse(player.PromoteLevel))
                {
                    cmdFeedback.Invoke("EqUtils", "You must be at least " + cmd.MinimumLevel + " to use this command");
                    return;
                }
                var result = cmd.Process(cmdFeedback, args);
                if (result != null)
                    cmdFeedback.Invoke(result);
            }
            catch (ArgumentException e)
            {
                Log(MyLogSeverity.Debug, "Failed to parse \"{0}\".  Error:\n{1}", msg, e.ToString());
            }
            catch (Exception e)
            {
                Log(MyLogSeverity.Critical, "Failed to process \"{0}\".  Error:\n{1}", msg, e.ToString());
            }
        }

        private void HandleGlobalCommand(ulong steamID, string msg)
        {
            if (msg.Length == 0 || msg[0] != '/') return;
            try
            {
                var args = ParseArguments(msg, 1);
                Command cmd;
                lock (m_commands)
                    if (!m_commands.TryGetValue(args[0], out cmd))
                    {
                        Log(MyLogSeverity.Debug, "Unknown command {0}", args[0]);
                        return;
                    }

                var player = MyAPIGateway.Players.GetPlayerBySteamId(steamID);
                if (player == null)
                {
                    Log(MyLogSeverity.Warning, "Attempted unable to determine player instance for Steam ID {0}", steamID);
                    return;
                }
                if (!MyAPIGateway.Session.SessionType().Flagged(cmd.AllowedSessionType))
                {
                    Log(MyLogSeverity.Debug, "Unable to run {0} on a session of type {1}; it requires type {2}", args[0], MyAPIGateway.Session.SessionType(), cmd.AllowedSessionType);
                    return;
                }
                var cmdFeedback = new CommandFeedback((format, fields) =>
                {
                    var content = string.Format(format, fields);
                    MyVisualScriptLogicProvider.SendChatMessage(content, "EqProcUtils", player.IdentityId);
                });
                if (!cmd.CanPromotionLevelUse(player.PromoteLevel))
                {
                    cmdFeedback.Invoke("You must be at least a {0} to use the {1} command.  You are are {2}", cmd.MinimumLevel, args[0], player.PromoteLevel);
                    Log(MyLogSeverity.Debug, "Player {0} ({1}) attempted to run {2} at level {3}", player.DisplayName, player.PromoteLevel, args[0], cmd.MinimumLevel);
                    return;
                }
                var result = cmd.Process(cmdFeedback, args);
                if (result != null)
                    cmdFeedback.Invoke(result);
            }
            catch (ArgumentException e)
            {
                Log(MyLogSeverity.Debug, "Failed to parse \"{0}\".  Error:\n{1}", msg, e.ToString());
            }
            catch (Exception e)
            {
                Log(MyLogSeverity.Critical, "Failed to process \"{0}\".  Error:\n{1}", msg, e.ToString());
            }
        }

        private static readonly MyConcurrentPool<List<string>> ArgsListPool = new MyConcurrentPool<List<string>>(4);
        private static readonly MyConcurrentPool<StringBuilder> ArgBuilderPool = new MyConcurrentPool<StringBuilder>(4);
        private static string[] ParseArguments(string data, int offset = 0)
        {
            var list = ArgsListPool.Get();
            var builder = ArgBuilderPool.Get();
            try
            {
                list.Clear();
                builder.Clear();
                char? currentQuote = null;

                var escaped = false;
                for (var head = offset; head < data.Length; head++)
                {
                    var ch = data[head];
                    if (ch == '\\' && !escaped)
                    {
                        escaped = true;
                        continue;
                    }
                    if (!escaped && (ch == '\'' || ch == '"'))
                    {
                        if (currentQuote.HasValue && currentQuote == ch)
                            currentQuote = null;
                        else if (!currentQuote.HasValue)
                            currentQuote = ch;
                        continue;
                    }
                    if (escaped)
                        switch (ch)
                        {
                            case '\\':
                            case '\'':
                            case '"':
                            case ' ':
                                break;
                            case 't':
                                ch = '\t';
                                break;
                            case 'n':
                                ch = '\n';
                                break;
                            case 'r':
                                ch = '\r';
                                break;
                            default:
                                throw new ArgumentException("Invalid escape sequence: \\" + ch);
                        }
                    if (!escaped && !currentQuote.HasValue && ch == ' ')
                    {
                        list.Add(builder.ToString());
                        builder.Clear();
                        continue;
                    }
                    builder.Append(ch);
                    escaped = false;
                }
                if (currentQuote.HasValue)
                    throw new ArgumentException("Unclosed quote " + currentQuote.Value);
                if (escaped)
                    throw new ArgumentException("Can't end with an escape sequence");
                list.Add(builder.ToString());
                return list.ToArray();
            }
            finally
            {
                list.Clear();
                builder.Clear();
                ArgsListPool.Return(list);
                ArgBuilderPool.Return(builder);
            }
        }
        public override void LoadConfiguration(Ob_ModSessionComponent config)
        {
            if (config == null) return;
            if (config is Ob_CommandDispatch) return;
            Log(MyLogSeverity.Critical, "Configuration type {0} doesn't match component type {1}", config.GetType(), GetType());
        }

        public override Ob_ModSessionComponent SaveConfiguration()
        {
            return new Ob_CommandDispatch();
        }
    }

    public class Ob_CommandDispatch : Ob_ModSessionComponent
    {
    }
}