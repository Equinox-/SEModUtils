using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage;

namespace Equinox.Utils.Network
{
    public static class SyncObjectFactory
    {
        public delegate SyncByReference CreateSyncByReferenceDelegate(SyncObjectActivator info, SyncComponent component, SyncDirection direction, ulong id);

        public class SyncObjectActivator
        {
            public readonly Type Type;
            public readonly ulong TypeID;
            public readonly CreateSyncByReferenceDelegate Activator;

            public SyncObjectActivator(Type type, CreateSyncByReferenceDelegate activate)
            {
                Type = type;
                TypeID = type.FullName.Hash64();
                Activator = activate;
            }
        }


        private static readonly FastResourceLock Lock = new FastResourceLock();
        private static readonly Dictionary<Type, SyncObjectActivator> ActivatorsByType = new Dictionary<Type, SyncObjectActivator>();
        private static readonly Dictionary<ulong, SyncObjectActivator> ActivatorsByID = new Dictionary<ulong, SyncObjectActivator>();

        static SyncObjectFactory()
        {
            SyncObjectGen.RegisterAll();
        }

        public static void Register<T>(Type providedType) where T : SyncByReference, new()
        {
            using (Lock.AcquireExclusiveUsing())
            {
                var activator = new SyncObjectActivator(providedType, (info, sync, direction, id) =>
                {
                    var res = new T();
                    res.Init(info, sync, direction, id);
                    return res;
                });
                ActivatorsByType[providedType] = activator;
                ActivatorsByType[typeof(T)] = activator;
                ActivatorsByID[activator.TypeID] = activator;
            }
        }

        public static SyncByReference CreateSyncByReference(ulong typeID, SyncComponent component, SyncDirection direction, ulong id)
        {
            using (Lock.AcquireSharedUsing())
            {
                var info = ActivatorsByID[typeID];
                return info.Activator.Invoke(info, component, direction, id);
            }
        }

        public static SyncByReference CreateSyncByReference(Type type, SyncComponent component, SyncDirection direction, ulong id)
        {
            using (Lock.AcquireSharedUsing())
            {
                var info = ActivatorsByType[type];
                return info.Activator.Invoke(info, component, direction, id);
            }
        }
    }
}
