using System;
using System.Collections.Generic;
using VRage;

namespace Equinox.Utils.Stream
{
    public static class SerializerRegistry
    {
        private static readonly FastResourceLock Lock = new FastResourceLock();
        private static readonly Dictionary<Type, Serializer> Serializers = new Dictionary<Type, Serializer>();

        static SerializerRegistry()
        {
            SerializerExtensions.RegisterBuiltinTypes();
        }

        public static void RegisterSerializer<T>(Serializer<T> serializer)
        {
            using (Lock.AcquireExclusiveUsing())
                Serializers[typeof(T)] = serializer;
        }

        public static Serializer<T> Get<T>()
        {
            Serializer gen;
            using (Lock.AcquireSharedUsing())
                if (Serializers.TryGetValue(typeof(T), out gen))
                    return (Serializer<T>)gen;
            var serializer = new FallbackSerializer<T>();
            using (Lock.AcquireExclusiveUsing())
                Serializers[typeof(T)] = serializer;
            return serializer;
        }
    }
}
