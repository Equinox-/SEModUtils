using System;
using System.Collections.Generic;
using VRage;

namespace Equinox.Utils.Stream
{
    public static class MySerializerRegistry
    {
        private static readonly FastResourceLock Lock = new FastResourceLock();
        private static readonly Dictionary<Type, MySerializer> Serializers = new Dictionary<Type, MySerializer>();

        static MySerializerRegistry()
        {
            MySerializerExtensions.RegisterBuiltinTypes();
        }

        public static void RegisterSerializer<T>(MySerializer<T> serializer)
        {
            using (Lock.AcquireExclusiveUsing())
                Serializers[typeof(T)] = serializer;
        }

        public static MySerializer<T> Get<T>()
        {
            MySerializer gen;
            using (Lock.AcquireSharedUsing())
                if (Serializers.TryGetValue(typeof(T), out gen))
                    return (MySerializer<T>)gen;
            var serializer = new MyFallbackSerializer<T>();
            using (Lock.AcquireExclusiveUsing())
                Serializers[typeof(T)] = serializer;
            return serializer;
        }
    }
}
