using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage;

namespace Equinox.Utils.Network
{
    public static class MySyncObjectFactory
    {
        public delegate MySyncByReference CreateSyncByReferenceDelegate(MySyncObjectActivator info, MySyncComponent component, MySyncDirection direction, ulong id);

        public class MySyncObjectActivator
        {
            public readonly Type Type;
            public readonly ulong TypeID;
            public readonly CreateSyncByReferenceDelegate Activator;

            public MySyncObjectActivator(Type type, CreateSyncByReferenceDelegate activate)
            {
                Type = type;
                TypeID = type.FullName.Hash64();
                Activator = activate;
            }
        }


        private static readonly FastResourceLock Lock = new FastResourceLock();
        private static readonly Dictionary<Type, MySyncObjectActivator> ActivatorsByType = new Dictionary<Type, MySyncObjectActivator>();
        private static readonly Dictionary<ulong, MySyncObjectActivator> ActivatorsByID = new Dictionary<ulong, MySyncObjectActivator>();

        static MySyncObjectFactory()
        {
            MySyncObjectGen.RegisterAll();
        }

        public static void Register<T>(Type providedType) where T : MySyncByReference, new()
        {
            using (Lock.AcquireExclusiveUsing())
            {
                var activator = new MySyncObjectActivator(providedType, (info, sync, direction, id) =>
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

        public static MySyncByReference CreateSyncByReference(ulong typeID, MySyncComponent component, MySyncDirection direction, ulong id)
        {
            using (Lock.AcquireSharedUsing())
            {
                var info = ActivatorsByID[typeID];
                return info.Activator.Invoke(info, component, direction, id);
            }
        }

        public static MySyncByReference CreateSyncByReference(Type type, MySyncComponent component, MySyncDirection direction, ulong id)
        {
            using (Lock.AcquireSharedUsing())
            {
                var info = ActivatorsByType[type];
                return info.Activator.Invoke(info, component, direction, id);
            }
        }
    }
}
