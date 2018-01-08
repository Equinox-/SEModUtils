using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equinox.Utils.Stream;

namespace Equinox.Utils.Network
{
    public interface ISyncByReference
    {
        ulong SyncObjectID { get; }
        SyncDirection SyncDirection { get; }
        bool StartReplication(EndpointId endpoint);
        bool StopReplication(EndpointId endpoint);
        IEnumerable<EndpointId> ReplicationPoints { get; }
    }

    public enum SyncDirection
    {
        ServerToClient = 0,
        ClientToServer = 1
    }

    public abstract class SyncByReference : ISyncByReference
    {
        public ulong SyncObjectID { get; private set; }
        private readonly HashSet<EndpointId> m_replicationPoints = new HashSet<EndpointId>();
        public IEnumerable<EndpointId> ReplicationPoints => m_replicationPoints;
        public SyncDirection SyncDirection { get; private set; }
        public SyncComponent SyncComponent { get; private set; }
        public SyncObjectFactory.SyncObjectActivator TypeInfo { get; private set; }

        public bool StartReplication(EndpointId endpoint)
        {
            lock (this)
                if (!m_replicationPoints.Add(endpoint)) return false;
            SyncComponent.AllocateObjectOn(this, endpoint);
            return true;
        }

        public bool StopReplication(EndpointId endpoint)
        {
            lock (this)
                if (!m_replicationPoints.Remove(endpoint)) return false;
            SyncComponent.DestroyObjectOn(this, endpoint);
            return true;
        }

        private bool m_isQueued = false;
        protected void MarkDirty()
        {
            lock (this)
            {
                if (m_isQueued) return;
                m_isQueued = true;
            }
            SyncComponent?.QueueUpdate(this);
        }

        internal void MarkClean()
        {
            m_isQueued = false;
        }

        public void Init(SyncObjectFactory.SyncObjectActivator info, SyncComponent sync, SyncDirection direction, ulong id)
        {
            TypeInfo = info;
            SyncDirection = direction;
            SyncComponent = sync;
            SyncObjectID = id;
        }

        public abstract bool WriteChanges(MemoryStream stream, bool fullCopy = false);

        public abstract void ReadChanges(MemoryStream stream);
    }
}
