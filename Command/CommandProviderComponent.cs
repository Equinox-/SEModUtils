using System.Collections.Generic;
using Equinox.Utils.Session;

namespace Equinox.Utils.Command
{
    public abstract class CommandProviderComponent : LoggingSessionComponent
    {
        public CommandDispatchComponent Dispatch { get; private set; }
        private readonly List<Command> m_commands = new List<Command>();

        protected CommandProviderComponent()
        {
            DependsOn((CommandDispatchComponent x) => { Dispatch = x; });
            m_isAttached = false;
        }

        protected Command Create(params string[] names)
        {
            var cmd = new Command(names);
            lock (m_commands)
            {
                m_commands.Add(cmd);
                if (m_isAttached)
                    Dispatch.AddCommand(cmd);
            }
            return cmd;
        }

        private bool m_isAttached;
        protected override void Attach()
        {
            base.Attach();
            lock (m_commands)
            {
                foreach (var c in m_commands)
                    Dispatch.AddCommand(c);
                m_isAttached = true;
            }
        }

        protected override void Detach()
        {
            base.Detach();
            lock (m_commands)
            {
                foreach (var c in m_commands)
                    Dispatch.RemoveCommand(c);
                m_isAttached = false;
            }
        }
    }
}
