using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equinox.Utils.Session;
using Equinox.Utils.Stream;
using Sandbox.ModAPI;
using VRage;
using VRage.Collections;
using VRage.Library.Utils;
using VRage.Utils;

namespace Equinox.Utils.Network
{
    public class MySyncComponent : MyLoggingSessionComponent
    {
        private readonly FastResourceLock m_objectsLock = new FastResourceLock();
        private readonly Dictionary<ulong, MySyncByReference> m_objects = new Dictionary<ulong, MySyncByReference>();

        private readonly MyQueue<MySyncByReference> m_updateQueue = new MyQueue<MySyncByReference>(128);

        private MyNetworkComponent m_network;
        public MySyncComponent()
        {
            DependsOn((MyNetworkComponent c) => m_network = c);
        }

        private static readonly Type[] SuppliedDeps = { typeof(MySyncComponent) };
        public override IEnumerable<Type> SuppliedComponents => SuppliedDeps;

        private void HandlePacket(MyDestroySyncObjectPacket packet)
        {
            MySyncByReference obj;
            if (!m_objects.TryGetValue(packet.ObjectID, out obj))
            {
                Log(MyLogSeverity.Warning, "Someone requested we destroy something that isn't allocated");
                return;
            }
            var direction = MyAPIGateway.Session.IsDecider() ? MySyncDirection.ClientToServer : MySyncDirection.ServerToClient;
            if (obj.SyncDirection != direction)
            {
                Log(MyLogSeverity.Warning, "Someone requested an improper destroy");
                return;
            }
            if (MyAPIGateway.Session.IsDecider())
                m_network.SendMessage(packet, obj.ReplicationPoints.Where(x => x != packet.Source && x != MyNetworkComponent.MyID));
            m_objects.Remove(packet.ObjectID);
        }

        private void HandlePacket(MyUpdateSyncObjectPacket packet)
        {
            MySyncByReference obj;
            if (!m_objects.TryGetValue(packet.ObjectID, out obj))
            {
                Log(MyLogSeverity.Warning, "Someone requested we update something that isn't allocated");
                return;
            }
            var direction = MyAPIGateway.Session.IsDecider() ? MySyncDirection.ClientToServer : MySyncDirection.ServerToClient;
            if (obj.SyncDirection != direction)
            {
                Log(MyLogSeverity.Warning, "Someone requested an improper update");
                return;
            }
            packet.Stream.ReadHead = 0;
            obj.ReadChanges(packet.Stream);
            m_network.SendMessage(packet, obj.ReplicationPoints.Where(x => x != packet.Source && x != MyNetworkComponent.MyID));
        }

        private void HandlePacket(MyAllocateSyncObjectPacket packet)
        {
            if (m_objects.ContainsKey(packet.ObjectID))
            {
                Log(MyLogSeverity.Warning, "Someone requested we allocate something already allocated.  ID {0}", packet.ObjectID);
                return;
            }
            var result = MySyncObjectFactory.CreateSyncByReference(packet.ObjectType, this, packet.SyncDirection, packet.ObjectID);
            result.StartReplication(MyNetworkComponent.MyID);
            result.StartReplication(packet.Source);
            m_objects[result.SyncObjectID] = result;
            result.ReadChanges(packet.Stream);
        }

        public void Destroy(IMySyncByReference obj)
        {
            if (MyAPIGateway.Session.IsDecider() && obj.SyncDirection == MySyncDirection.ServerToClient)
            {
                var packet = m_network.AllocatePacket<MyDestroySyncObjectPacket>();
                packet.ObjectID = obj.SyncObjectID;
                m_network.SendMessage(packet, obj.ReplicationPoints.Where(x => x != MyNetworkComponent.MyID));
                m_network.ReturnPacket(packet);
                m_objects.Remove(obj.SyncObjectID);
            }
            else if (!MyAPIGateway.Session.IsDecider() && obj.SyncDirection == MySyncDirection.ClientToServer)
            {
                var packet = m_network.AllocatePacket<MyDestroySyncObjectPacket>();
                packet.ObjectID = obj.SyncObjectID;
                m_network.SendMessageToServer(packet);
                m_network.ReturnPacket(packet);
                m_objects.Remove(obj.SyncObjectID);
            }
            else
            {
                Log(MyLogSeverity.Warning, "Tried to directly destroy something we don't control");
            }
        }

        public T Allocate<T>() where T : IMySyncByReference
        {
            // Rexxar will hate me
            var objectID = (ulong)MyRandom.Instance.NextLong() ^ MyNetworkComponent.MyID;
            var result = MySyncObjectFactory.CreateSyncByReference(typeof(T), this,
                MyAPIGateway.Session.IsDecider() ? MySyncDirection.ServerToClient : MySyncDirection.ClientToServer, objectID);
            result.StartReplication(MyNetworkComponent.MyID);
            // this works, but direct cast doesn't?
            return (T)(IMySyncByReference)result;
        }

        public void QueueUpdate(MySyncByReference instance)
        {
            lock (m_updateQueue)
                m_updateQueue.Enqueue(instance);
        }

        protected override void Attach()
        {
            base.Attach();
            using (m_objectsLock.AcquireExclusiveUsing())
                m_objects.Clear();
            m_network.RegisterPacketType<MyDestroySyncObjectPacket>(HandlePacket);
            m_network.RegisterPacketType<MyUpdateSyncObjectPacket>(HandlePacket);
            m_network.RegisterPacketType<MyAllocateSyncObjectPacket>(HandlePacket);
        }

        protected override void Detach()
        {
            base.Detach();
            m_network.UnregisterPacketType<MyDestroySyncObjectPacket>(HandlePacket);
            m_network.UnregisterPacketType<MyUpdateSyncObjectPacket>(HandlePacket);
            m_network.UnregisterPacketType<MyAllocateSyncObjectPacket>(HandlePacket);
            using (m_objectsLock.AcquireExclusiveUsing())
                m_objects.Clear();
        }

        internal void AllocateObjectOn(MySyncByReference obj, MyEndpointId dest)
        {
            if (dest == MyNetworkComponent.MyID) return;
            if (!MyAPIGateway.Session.IsDecider())
            {
                if (dest != MyNetworkComponent.ServerID)
                {
                    Log(MyLogSeverity.Warning, "Client tried to instruct another client to allocate");
                    return;
                }
                if (obj.SyncDirection != MySyncDirection.ClientToServer)
                {
                    Log(MyLogSeverity.Warning, "Client tried to instruction a server to client object to be allocated");
                    return;
                }
            }
            var packet = m_network.AllocatePacket<MyAllocateSyncObjectPacket>();
            packet.ObjectID = obj.SyncObjectID;
            packet.ObjectType = obj.TypeInfo.TypeID;
            packet.SyncDirection = obj.SyncDirection;
            packet.Stream.ReadHead = packet.Stream.WriteHead = 0;
            obj.WriteChanges(packet.Stream);
            if (MyAPIGateway.Session.IsDecider())
                m_network.SendMessage(packet, dest);
            else
                m_network.SendMessageToServer(packet);
            m_network.ReturnPacket(packet);
        }

        internal void DestroyObjectOn(MySyncByReference obj, MyEndpointId dest)
        {
            if (dest == MyNetworkComponent.MyID) return;
            if (!MyAPIGateway.Session.IsDecider())
            {
                if (dest != MyNetworkComponent.ServerID)
                {
                    Log(MyLogSeverity.Warning, "Client tried to instruct another client to destroy");
                    return;
                }
                if (obj.SyncDirection != MySyncDirection.ClientToServer)
                {
                    Log(MyLogSeverity.Warning, "Client tried to instruction a server to client object to be destroyed");
                    return;
                }
            }

            var packet = m_network.AllocatePacket<MyDestroySyncObjectPacket>();
            packet.ObjectID = obj.SyncObjectID;
            if (MyAPIGateway.Session.IsDecider())
                m_network.SendMessage(packet, dest);
            else
                m_network.SendMessageToServer(packet);
            m_network.ReturnPacket(packet);
        }

        public override void UpdateAfterSimulation()
        {
            while (true)
            {
                MySyncByReference sync;
                lock (m_updateQueue)
                {
                    if (m_updateQueue.Count == 0) break;
                    sync = m_updateQueue.Dequeue();
                }
                lock (sync)
                {
                    sync.MarkClean();
                    var packet = m_network.AllocatePacket<MyUpdateSyncObjectPacket>();
                    packet.ObjectID = sync.SyncObjectID;
                    packet.Stream.WriteHead = packet.Stream.ReadHead = 0;
                    sync.WriteChanges(packet.Stream);
                    if (MyAPIGateway.Session.IsDecider())
                        m_network.SendMessage(packet, sync.ReplicationPoints);
                    else
                        m_network.SendMessageToServer(packet);
                    m_network.ReturnPacket(packet);
                }
            }
        }

        #region PacketTypes
        private class MyDestroySyncObjectPacket : MyPacket
        {
            public ulong ObjectID;

            public override void ReadFrom(MyMemoryStream input)
            {
                base.ReadFrom(input);
                input.Read(ref ObjectID);
            }

            public override void WriteTo(MyMemoryStream output)
            {
                base.WriteTo(output);
                output.Write(ObjectID);
            }
        }

        private class MyUpdateSyncObjectPacket : MySubStreamPacket
        {
            public ulong ObjectID;

            public MyUpdateSyncObjectPacket() : base(32)
            {
            }

            public override void ReadFrom(MyMemoryStream input)
            {
                base.ReadFrom(input);
                input.Read(ref ObjectID);
            }

            public override void WriteTo(MyMemoryStream output)
            {
                base.WriteTo(output);
                output.Write(ObjectID);
            }
        }

        private class MyAllocateSyncObjectPacket : MySubStreamPacket
        {
            public ulong ObjectID;
            public ulong ObjectType;
            public MySyncDirection SyncDirection;

            public MyAllocateSyncObjectPacket() : base(32)
            {
            }

            public override void ReadFrom(MyMemoryStream input)
            {
                base.ReadFrom(input);
                input.Read(ref ObjectID);
                input.Read(ref ObjectType);
                byte dir = 0;
                input.Read(ref dir);
                SyncDirection = (MySyncDirection)dir;
            }

            public override void WriteTo(MyMemoryStream output)
            {
                base.WriteTo(output);
                output.Write(ObjectID);
                output.Write(ObjectType);
                output.Write((byte)SyncDirection);
            }
        }
        #endregion


        public override void LoadConfiguration(MyObjectBuilder_ModSessionComponent config)
        {
            if (config == null) return;
            if (config is MyObjectBuilder_Sync) return;
            Log(MyLogSeverity.Critical, "Configuration type {0} doesn't match component type {1}", config.GetType(), GetType());
        }

        public override MyObjectBuilder_ModSessionComponent SaveConfiguration()
        {
            return new MyObjectBuilder_Sync();
        }
    }

    public class MyObjectBuilder_Sync : MyObjectBuilder_ModSessionComponent
    {

    }
}
