using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Equinox.Utils.Logging;
using Equinox.Utils.Stream;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Utils;

namespace Equinox.Utils.Session
{
    public class MySessionBootstrapper : MyModSessionComponent
    {
        public const ushort MessageChannel = 57655;
        private readonly Dictionary<int, MyModSessionComponent> m_hashCodeToComponent = new Dictionary<int, MyModSessionComponent>();

        public MySessionBootstrapper()
        {
            SaveToStorage = false;
        }

        protected override void Attach()
        {
            base.Attach();
            if (MyAPIGateway.Multiplayer == null || MyAPIGateway.Session.OnlineMode == MyOnlineModeEnum.OFFLINE) return;
            if (MyAPIGateway.Session.IsDecider())
            {
                Manager.ComponentAttached += OnComponentAttached;
                Manager.ComponentDetached += OnComponentRemoved;
                if (MyAPIGateway.Players.Count > 0 && !MyAPIGateway.Multiplayer.SendMessageToOthers(MessageChannel, CreateFullConfig()))
                    Manager.FallbackLogger.Log(MyLogSeverity.Critical, "Failed to send full config packet");
            }
            else
            {
                MyAPIGateway.Utilities.InvokeOnGameThread(() =>
                {
                    if (!MyAPIGateway.Multiplayer.SendMessageToServer(MessageChannel, CreateRequestConfig()))
                        Manager.FallbackLogger.Log(MyLogSeverity.Critical, "Failed to send config request packet");
                });
            }
            MyAPIGateway.Multiplayer.RegisterMessageHandler(MessageChannel, MessageHandler);
        }

        protected override void Detach()
        {
            base.Detach();

            if (MyAPIGateway.Session.IsDecider())
            {
                Manager.ComponentAttached -= OnComponentAttached;
                Manager.ComponentDetached -= OnComponentRemoved;
            }
            if (MyAPIGateway.Multiplayer == null || MyAPIGateway.Session.OnlineMode == MyOnlineModeEnum.OFFLINE) return;
            MyAPIGateway.Multiplayer.UnregisterMessageHandler(MessageChannel, MessageHandler);
        }

        private void OnComponentAttached(MyModSessionComponent k)
        {
            if (!k.SaveToStorage) return;
            if (MyAPIGateway.Players.Count == 0) return;
            if (!MyAPIGateway.Multiplayer.SendMessageToOthers(MessageChannel, CreateSingleConfig(k)))
                Manager.FallbackLogger.Log(MyLogSeverity.Critical, "Failed to send single component added packet");
        }

        private void OnComponentRemoved(MyModSessionComponent k)
        {
            if (!k.SaveToStorage) return;
            if (MyAPIGateway.Players.Count == 0) return;
            if (!MyAPIGateway.Multiplayer.SendMessageToOthers(MessageChannel, CreateRemoveConfig(k)))
                Manager.FallbackLogger.Log(MyLogSeverity.Critical, "Failed to send single component removed packet");
        }

        private const byte PacketFullConfig = 1;
        private const byte PacketAddModule = 2;
        private const byte PacketRemoveModule = 3;
        private const byte PacketRequestConfig = 4;
        
        private void MessageHandler(byte[] packet)
        {
            var stream = MyMemoryStream.CreateReaderFor(packet);
            var packetID = stream.ReadByte();
            switch (packetID)
            {
                case PacketFullConfig:
                    if (!MyAPIGateway.Session.IsDecider())
                    {
                        Manager.FallbackLogger.Log(MyLogSeverity.Debug, "Recieved full config packet");
                        using (Manager.FallbackLogger.IndentUsing())
                        {
                            Manager.TolerableLag = TimeSpan.FromSeconds(stream.ReadDouble());
                            // full config
                            while (stream.ReadByte() != 0)
                            {
                                MyModSessionComponent com;
                                MyObjectBuilder_ModSessionComponent ob;
                                ReadComponent(stream, out com, out ob);
                                Manager.Register(com, ob);
                            }
                        }
                    }
                    break;
                case PacketAddModule:
                    if (!MyAPIGateway.Session.IsDecider())
                    {
                        Manager.FallbackLogger.Log(MyLogSeverity.Debug, "Recieved add module packet");
                        using (Manager.FallbackLogger.IndentUsing())
                        {
                            // partial addition
                            MyModSessionComponent com;
                            MyObjectBuilder_ModSessionComponent ob;
                            ReadComponent(stream, out com, out ob);
                            Manager.Register(com, ob);
                        }
                    }
                    break;
                case PacketRemoveModule:
                    if (!MyAPIGateway.Session.IsDecider())
                    {
                        Manager.FallbackLogger.Log(MyLogSeverity.Debug, "Recieved remove module packet");
                        using (Manager.FallbackLogger.IndentUsing())
                        {
                            // partial removal
                            var code = stream.ReadInt32();
                            MyModSessionComponent com;
                            if (m_hashCodeToComponent.TryGetValue(code, out com))
                                Manager.Unregister(com);
                            else
                                Manager.FallbackLogger.Log(MyLogSeverity.Critical, "Unknown module ID {0}", code);
                        }
                    }
                    break;
                case PacketRequestConfig:
                    if (MyAPIGateway.Session.IsDecider())
                    {
                        Manager.FallbackLogger.Log(MyLogSeverity.Debug, "Recieved config request packet");
                        var endpoint = stream.ReadUInt64();
                        MyAPIGateway.Multiplayer.SendMessageTo(MessageChannel, CreateFullConfig(), endpoint);
                    }
                    break;
                default:
                    Manager.FallbackLogger.Log(MyLogSeverity.Critical, "Invalid bootstrapping packet ID: {0}", packetID);
                    break;
            }
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private int CreateIdentifier(MyModSessionComponent component)
        {
            return RuntimeHelpers.GetHashCode(component);
        }

        private void ReadComponent(MyMemoryStream stream, out MyModSessionComponent com, out MyObjectBuilder_ModSessionComponent ob)
        {
            var code = stream.ReadInt32();
            var desc = MyModSessionComponentRegistry.Get(stream.ReadUInt64());
            ob = desc.SerializeFromXml.Invoke(stream.ReadString());
            com = desc.Activator.Invoke();
            m_hashCodeToComponent.Add(code, com);
            Manager.FallbackLogger.Log(MyLogSeverity.Debug, "Read component of type {0} from bootstrapper. ID={1}", com.GetType().Name, code);
        }

        private void WriteComponent(MyModSessionComponent component, MyMemoryStream stream)
        {
            var code = CreateIdentifier(component);
            m_hashCodeToComponent[code] = component;
            stream.Write(code);
            var desc = MyModSessionComponentRegistry.Get(component.GetType());
            stream.Write(desc.TypeKey);
            stream.Write(MyAPIGateway.Utilities.SerializeToXML(component.SaveConfiguration()));
            Manager.FallbackLogger.Log(MyLogSeverity.Debug, "Wrote component of type {0} for bootstrapper. ID={1}", component.GetType().Name, code);
        }

        // ReSharper disable once MemberCanBeMadeStatic.Local
        private byte[] CreateRequestConfig()
        {
            var stream = MyMemoryStream.CreateEmptyStream(5);
            stream.Write(PacketRequestConfig);
            stream.Write(MyAPIGateway.Multiplayer.MyId);
            var copy = new byte[stream.WriteHead];
            Array.Copy(stream.Backing, 0, copy, 0, copy.Length);
            stream.Dispose();
            Manager.FallbackLogger.Log(MyLogSeverity.Debug, "Create config request packet");
            return copy;
        }

        private byte[] CreateFullConfig()
        {
            Manager.FallbackLogger.Log(MyLogSeverity.Debug, "Create full packet");
            using (Manager.FallbackLogger.IndentUsing())
            {
                var stream = MyMemoryStream.CreateEmptyStream(4096);
                stream.Write(PacketFullConfig);
                stream.Write(Manager.TolerableLag.TotalSeconds);
                foreach (var k in Manager.OrderedComponents)
                {
                    if (!k.SaveToStorage) continue;
                    stream.Write((byte)1);
                    WriteComponent(k, stream);
                }
                stream.Write((byte)0);
                var copy = new byte[stream.WriteHead];
                Array.Copy(stream.Backing, 0, copy, 0, copy.Length);
                stream.Dispose();
                return copy;
            }
        }

        private byte[] CreateSingleConfig(MyModSessionComponent k)
        {
            Manager.FallbackLogger.Log(MyLogSeverity.Debug, "Create add module packet");
            using (Manager.FallbackLogger.IndentUsing())
            {
                var stream = MyMemoryStream.CreateEmptyStream(512);
                stream.Write(PacketAddModule);
                WriteComponent(k, stream);
                var copy = new byte[stream.WriteHead];
                Array.Copy(stream.Backing, 0, copy, 0, copy.Length);
                stream.Dispose();
                return copy;
            }
        }

        private byte[] CreateRemoveConfig(MyModSessionComponent k)
        {
            Manager.FallbackLogger.Log(MyLogSeverity.Debug, "Create remove module packet");
            using (Manager.FallbackLogger.IndentUsing())
            {
                var stream = MyMemoryStream.CreateEmptyStream(5);
                stream.Write(PacketRemoveModule);
                stream.Write(CreateIdentifier(k));
                Manager.FallbackLogger.Log(MyLogSeverity.Debug, "Wrote module remove for component of type {0}", k.GetType().Name);
                var copy = new byte[stream.WriteHead];
                Array.Copy(stream.Backing, 0, copy, 0, copy.Length);
                stream.Dispose();
                return copy;
            }
        }

        public override void LoadConfiguration(MyObjectBuilder_ModSessionComponent config)
        {
            if (config == null) return;
            if (config is MyObjectBuilder_SessionBootstrapper) return;
            Manager?.FallbackLogger.Log(MyLogSeverity.Critical, "Configuration type {0} doesn't match component type {1}", config.GetType(), GetType());
        }

        public override MyObjectBuilder_ModSessionComponent SaveConfiguration()
        {
            return new MyObjectBuilder_SessionBootstrapper();
        }
    }

    public class MyObjectBuilder_SessionBootstrapper : MyObjectBuilder_ModSessionComponent
    {

    }
}
