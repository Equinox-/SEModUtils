using System;
using System.Collections.Generic;
using Equinox.Utils.Network;
using Equinox.Utils.Session;
using Equinox.Utils.Stream;
using VRage.Utils;

namespace Equinox.Utils.Network
{
    public class MyRPCActionPacket : MySubStreamPacket
    {
        public MyRPCActionPacket() : base(128)
        {
        }
    }

    public class MyObjectBuilder_RPC : MyObjectBuilder_ModSessionComponent
    {
        
    }

    public class MyRPCComponent : MyLoggingSessionComponent
    {
        public MyNetworkComponent Network { get; private set; }

        public MyRPCComponent()
        {
            DependsOn((MyNetworkComponent x) => Network = x);
        }

        private static readonly Type[] SuppliedDeps =  { typeof(MyRPCComponent) };
        public override IEnumerable<Type> SuppliedComponents => SuppliedDeps;

        private static void HandleRPCAction(MyRPCActionPacket packet)
        {
            var id = packet.Stream.ReadUInt64();
            var desc = MyRPCRegistry.GetByKey(id);
            desc.InvokeFrom(packet.Stream);
            packet.Stream.WriteHead = packet.Stream.ReadHead = 0;
        }

        protected override void Attach()
        {
            base.Attach();
            Network.RegisterPacketType<MyRPCActionPacket>(HandleRPCAction);
        }

        protected override void Detach()
        {
            base.Detach();
            Network.UnregisterPacketType<MyRPCActionPacket>(HandleRPCAction);
        }

        public override void LoadConfiguration(MyObjectBuilder_ModSessionComponent config)
        {
            if (config == null) return;
            if (config is MyObjectBuilder_RPC) return;
            Log(MyLogSeverity.Critical, "Configuration type {0} doesn't match component type {1}", config.GetType(), GetType());
        }

        public override MyObjectBuilder_ModSessionComponent SaveConfiguration()
        {
            return new MyObjectBuilder_RPC();
        }
    }
}
