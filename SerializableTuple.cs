using System;
using ProtoBuf;

namespace Equinox.Utils
{
    [Serializable, ProtoContract]
    public struct SerializableTuple<T1, T2>
    {
        [ProtoMember]
        public T1 Item1;
        [ProtoMember]
        public T2 Item2;
    }

    public static class SerializableTuple
    {
        public static SerializableTuple<T1, T2> Create<T1, T2>(T1 a, T2 b)
        {
            return new SerializableTuple<T1, T2>() { Item1 = a, Item2 = b };
        }
    }
}
