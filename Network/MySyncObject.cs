using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equinox.Utils.Stream;

namespace Equinox.Utils.Network
{
    public interface IMySyncByReference
    {
        ulong SyncObjectID { get; }
        MySyncDirection SyncDirection { get; }
        bool StartReplication(MyEndpointId endpoint);
        bool StopReplication(MyEndpointId endpoint);
        IEnumerable<MyEndpointId> ReplicationPoints { get; }
    }

    public enum MySyncDirection
    {
        ServerToClient = 0,
        ClientToServer = 1
    }

    public abstract class MySyncByReference : IMySyncByReference
    {
        public ulong SyncObjectID { get; private set; }
        private readonly HashSet<MyEndpointId> m_replicationPoints = new HashSet<MyEndpointId>();
        public IEnumerable<MyEndpointId> ReplicationPoints => m_replicationPoints;
        public MySyncDirection SyncDirection { get; private set; }
        public MySyncComponent SyncComponent { get; private set; }
        public MySyncObjectFactory.MySyncObjectActivator TypeInfo { get; private set; }

        public bool StartReplication(MyEndpointId endpoint)
        {
            lock (this)
                if (!m_replicationPoints.Add(endpoint)) return false;
            SyncComponent.AllocateObjectOn(this, endpoint);
            return true;
        }

        public bool StopReplication(MyEndpointId endpoint)
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

        public void Init(MySyncObjectFactory.MySyncObjectActivator info, MySyncComponent sync, MySyncDirection direction, ulong id)
        {
            TypeInfo = info;
            SyncDirection = direction;
            SyncComponent = sync;
            SyncObjectID = id;
        }

        public abstract bool WriteChanges(MyMemoryStream stream, bool fullCopy = false);

        public abstract void ReadChanges(MyMemoryStream stream);
    }
}
