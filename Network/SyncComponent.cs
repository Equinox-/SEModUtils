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
    public class SyncComponent : LoggingSessionComponent
    {
        private readonly FastResourceLock m_objectsLock = new FastResourceLock();
        private readonly Dictionary<ulong, SyncByReference> m_objects = new Dictionary<ulong, SyncByReference>();

        private readonly MyQueue<SyncByReference> m_updateQueue = new MyQueue<SyncByReference>(128);

        private NetworkComponent m_network;
        public SyncComponent()
        {
            DependsOn((NetworkComponent c) => m_network = c);
        }

        private static readonly Type[] SuppliedDeps = { typeof(SyncComponent) };
        public override IEnumerable<Type> SuppliedComponents => SuppliedDeps;

        private void HandlePacket(DestroySyncObjectPacket packet)
        {
            SyncByReference obj;
            if (!m_objects.TryGetValue(packet.ObjectID, out obj))
            {
                Log(MyLogSeverity.Warning, "Someone requested we destroy something that isn't allocated");
                return;
            }
            var direction = MyAPIGateway.Session.IsDecider() ? SyncDirection.ClientToServer : SyncDirection.ServerToClient;
            if (obj.SyncDirection != direction)
            {
                Log(MyLogSeverity.Warning, "Someone requested an improper destroy");
                return;
            }
            if (MyAPIGateway.Session.IsDecider())
                m_network.SendMessage(packet, obj.ReplicationPoints.Where(x => x != packet.Source && x != NetworkComponent.Id));
            m_objects.Remove(packet.ObjectID);
        }

        private void HandlePacket(UpdateSyncObjectPacket packet)
        {
            SyncByReference obj;
            if (!m_objects.TryGetValue(packet.ObjectID, out obj))
            {
                Log(MyLogSeverity.Warning, "Someone requested we update something that isn't allocated");
                return;
            }
            var direction = MyAPIGateway.Session.IsDecider() ? SyncDirection.ClientToServer : SyncDirection.ServerToClient;
            if (obj.SyncDirection != direction)
            {
                Log(MyLogSeverity.Warning, "Someone requested an improper update");
                return;
            }
            packet.Stream.ReadHead = 0;
            obj.ReadChanges(packet.Stream);
            m_network.SendMessage(packet, obj.ReplicationPoints.Where(x => x != packet.Source && x != NetworkComponent.Id));
        }

        private void HandlePacket(AllocateSyncObjectPacket packet)
        {
            if (m_objects.ContainsKey(packet.ObjectID))
            {
                Log(MyLogSeverity.Warning, "Someone requested we allocate something already allocated.  ID {0}", packet.ObjectID);
                return;
            }
            var result = SyncObjectFactory.CreateSyncByReference(packet.ObjectType, this, packet.SyncDirection, packet.ObjectID);
            result.StartReplication(NetworkComponent.Id);
            result.StartReplication(packet.Source);
            m_objects[result.SyncObjectID] = result;
            result.ReadChanges(packet.Stream);
        }

        public void Destroy(ISyncByReference obj)
        {
            if (MyAPIGateway.Session.IsDecider() && obj.SyncDirection == SyncDirection.ServerToClient)
            {
                var packet = m_network.AllocatePacket<DestroySyncObjectPacket>();
                packet.ObjectID = obj.SyncObjectID;
                m_network.SendMessage(packet, obj.ReplicationPoints.Where(x => x != NetworkComponent.Id));
                m_network.ReturnPacket(packet);
                m_objects.Remove(obj.SyncObjectID);
            }
            else if (!MyAPIGateway.Session.IsDecider() && obj.SyncDirection == SyncDirection.ClientToServer)
            {
                var packet = m_network.AllocatePacket<DestroySyncObjectPacket>();
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

        public T Allocate<T>() where T : ISyncByReference
        {
            // Rexxar will hate me
            var objectID = (ulong)MyRandom.Instance.NextLong() ^ NetworkComponent.Id;
            var result = SyncObjectFactory.CreateSyncByReference(typeof(T), this,
                MyAPIGateway.Session.IsDecider() ? SyncDirection.ServerToClient : SyncDirection.ClientToServer, objectID);
            result.StartReplication(NetworkComponent.Id);
            // this works, but direct cast doesn't?
            return (T)(ISyncByReference)result;
        }

        public void QueueUpdate(SyncByReference instance)
        {
            lock (m_updateQueue)
                m_updateQueue.Enqueue(instance);
        }

        protected override void Attach()
        {
            base.Attach();
            using (m_objectsLock.AcquireExclusiveUsing())
                m_objects.Clear();
            m_network.RegisterPacketType<DestroySyncObjectPacket>(HandlePacket);
            m_network.RegisterPacketType<UpdateSyncObjectPacket>(HandlePacket);
            m_network.RegisterPacketType<AllocateSyncObjectPacket>(HandlePacket);
        }

        protected override void Detach()
        {
            base.Detach();
            m_network.UnregisterPacketType<DestroySyncObjectPacket>(HandlePacket);
            m_network.UnregisterPacketType<UpdateSyncObjectPacket>(HandlePacket);
            m_network.UnregisterPacketType<AllocateSyncObjectPacket>(HandlePacket);
            using (m_objectsLock.AcquireExclusiveUsing())
                m_objects.Clear();
        }

        internal void AllocateObjectOn(SyncByReference obj, EndpointId dest)
        {
            if (dest == NetworkComponent.Id) return;
            if (!MyAPIGateway.Session.IsDecider())
            {
                if (dest != NetworkComponent.ServerID)
                {
                    Log(MyLogSeverity.Warning, "Client tried to instruct another client to allocate");
                    return;
                }
                if (obj.SyncDirection != SyncDirection.ClientToServer)
                {
                    Log(MyLogSeverity.Warning, "Client tried to instruction a server to client object to be allocated");
                    return;
                }
            }
            var packet = m_network.AllocatePacket<AllocateSyncObjectPacket>();
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

        internal void DestroyObjectOn(SyncByReference obj, EndpointId dest)
        {
            if (dest == NetworkComponent.Id) return;
            if (!MyAPIGateway.Session.IsDecider())
            {
                if (dest != NetworkComponent.ServerID)
                {
                    Log(MyLogSeverity.Warning, "Client tried to instruct another client to destroy");
                    return;
                }
                if (obj.SyncDirection != SyncDirection.ClientToServer)
                {
                    Log(MyLogSeverity.Warning, "Client tried to instruction a server to client object to be destroyed");
                    return;
                }
            }

            var packet = m_network.AllocatePacket<DestroySyncObjectPacket>();
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
                SyncByReference sync;
                lock (m_updateQueue)
                {
                    if (m_updateQueue.Count == 0) break;
                    sync = m_updateQueue.Dequeue();
                }
                lock (sync)
                {
                    sync.MarkClean();
                    var packet = m_network.AllocatePacket<UpdateSyncObjectPacket>();
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
        private class DestroySyncObjectPacket : Packet
        {
            public ulong ObjectID;

            public override void ReadFrom(MemoryStream input)
            {
                base.ReadFrom(input);
                input.Read(ref ObjectID);
            }

            public override void WriteTo(MemoryStream output)
            {
                base.WriteTo(output);
                output.Write(ObjectID);
            }
        }

        private class UpdateSyncObjectPacket : SubStreamPacket
        {
            public ulong ObjectID;

            public UpdateSyncObjectPacket() : base(32)
            {
            }

            public override void ReadFrom(MemoryStream input)
            {
                base.ReadFrom(input);
                input.Read(ref ObjectID);
            }

            public override void WriteTo(MemoryStream output)
            {
                base.WriteTo(output);
                output.Write(ObjectID);
            }
        }

        private class AllocateSyncObjectPacket : SubStreamPacket
        {
            public ulong ObjectID;
            public ulong ObjectType;
            public SyncDirection SyncDirection;

            public AllocateSyncObjectPacket() : base(32)
            {
            }

            public override void ReadFrom(MemoryStream input)
            {
                base.ReadFrom(input);
                input.Read(ref ObjectID);
                input.Read(ref ObjectType);
                byte dir = 0;
                input.Read(ref dir);
                SyncDirection = (SyncDirection)dir;
            }

            public override void WriteTo(MemoryStream output)
            {
                base.WriteTo(output);
                output.Write(ObjectID);
                output.Write(ObjectType);
                output.Write((byte)SyncDirection);
            }
        }
        #endregion


        public override void LoadConfiguration(Ob_ModSessionComponent config)
        {
            if (config == null) return;
            if (config is Ob_Sync) return;
            Log(MyLogSeverity.Critical, "Configuration type {0} doesn't match component type {1}", config.GetType(), GetType());
        }

        public override Ob_ModSessionComponent SaveConfiguration()
        {
            return new Ob_Sync();
        }
    }

    public class Ob_Sync : Ob_ModSessionComponent
    {

    }
}
