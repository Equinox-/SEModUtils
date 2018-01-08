using System;
using System.Collections.Generic;
using Equinox.Utils.Network;
using Equinox.Utils.Session;
using Equinox.Utils.Stream;
using VRage.Utils;

namespace Equinox.Utils.Network
{
    public class RPCActionPacket : SubStreamPacket
    {
        public RPCActionPacket() : base(128)
        {
        }
    }

    public class Ob_RPC : Ob_ModSessionComponent
    {
        
    }

    public class RPCComponent : LoggingSessionComponent
    {
        public NetworkComponent Network { get; private set; }

        public RPCComponent()
        {
            DependsOn((NetworkComponent x) => Network = x);
        }

        public static readonly Type[] SuppliedDeps =  { typeof(RPCComponent) };
        public override IEnumerable<Type> SuppliedComponents => SuppliedDeps;

        private static void HandleRPCAction(RPCActionPacket packet)
        {
            var id = packet.Stream.ReadUInt64();
            var desc = RPCRegistry.GetByKey(id);
            desc.InvokeFrom(packet.Stream);
            packet.Stream.WriteHead = packet.Stream.ReadHead = 0;
        }

        protected override void Attach()
        {
            base.Attach();
            Network.RegisterPacketType<RPCActionPacket>(HandleRPCAction);
        }

        protected override void Detach()
        {
            base.Detach();
            Network.UnregisterPacketType<RPCActionPacket>(HandleRPCAction);
        }

        public override void LoadConfiguration(Ob_ModSessionComponent config)
        {
            if (config == null) return;
            if (config is Ob_RPC) return;
            Log(MyLogSeverity.Critical, "Configuration type {0} doesn't match component type {1}", config.GetType(), GetType());
        }

        public override Ob_ModSessionComponent SaveConfiguration()
        {
            return new Ob_RPC();
        }
    }
}
