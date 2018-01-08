using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Equinox.Utils.Pool;
using Sandbox.ModAPI;
using VRageMath;

namespace Equinox.Utils.Stream
{
    public abstract class Serializer
    {
        public abstract void Read(ref object output, MemoryStream stream);
        public abstract void Write(ref object input, MemoryStream stream);
    }

    public abstract class Serializer<T> : Serializer
    {
        public abstract void Read(ref T output, MemoryStream stream);
        public abstract void Write(ref T input, MemoryStream stream);

        public override void Read(ref object output, MemoryStream stream)
        {
            var result = (T)output;
            Read(ref result, stream);
            output = result;
        }

        public override void Write(ref object input, MemoryStream stream)
        {
            var unbox = (T)input;
            Write(ref unbox, stream);
        }
    }

    public class Vec3Serializer : Serializer<Vector3D>
    {
        public override void Read(ref Vector3D output, MemoryStream stream)
        {
            stream.Read(ref output);
        }

        public override void Write(ref Vector3D input, MemoryStream stream)
        {
            stream.Write(ref input);
        }
    }

    public class SerializerBase<T> : Serializer<T>
    {
        public delegate void MemoryStreamIo(ref T inout, MemoryStream stream);

        private readonly MemoryStreamIo m_read, m_write;

        public SerializerBase(MemoryStreamIo read, MemoryStreamIo write)
        {
            m_read = read;
            m_write = write;
        }

        public override void Read(ref T output, MemoryStream stream)
        {
            m_read.Invoke(ref output, stream);
        }

        public override void Write(ref T input, MemoryStream stream)
        {
            m_write.Invoke(ref input, stream);
        }
    }

    public class SerializerNoRefBase<T> : Serializer<T>
    {
        public delegate void MemoryStreamIo(ref T inout, MemoryStream stream);

        private readonly Action<T, MemoryStream> m_write;
        private readonly Func<MemoryStream, T> m_read;

        public SerializerNoRefBase(Func<MemoryStream, T> read, Action<T, MemoryStream> write)
        {
            m_read = read;
            m_write = write;
        }

        public override void Read(ref T output, MemoryStream stream)
        {
            output = m_read.Invoke(stream);
        }

        public override void Write(ref T input, MemoryStream stream)
        {
            m_write.Invoke(input, stream);
        }
    }

    // Improvable maybe?  Very short lived allocations so not too bad.
    public class FallbackSerializer<T> : Serializer<T>
    {
        private bool m_tryProtobuf = true;

        public override void Read(ref T output, MemoryStream stream)
        {
            if (m_tryProtobuf)
            {
                var len = stream.Read7BitEncodedInt();
                var data = new byte[len];
                stream.Read(data, 0, len);
                try
                {
                    output = MyAPIGateway.Utilities.SerializeFromBinary<T>(data);
                    return;
                }
                catch
                {
                    // Rewind.
                    m_tryProtobuf = false;
                    stream.WriteHead -= len;
                }
            }
            var str = stream.ReadString();
            output = MyAPIGateway.Utilities.SerializeFromXML<T>(str);
        }

        public override void Write(ref T input, MemoryStream stream)
        {
            if (m_tryProtobuf)
            {
                try
                {
                    var data = MyAPIGateway.Utilities.SerializeToBinary(input);
                    stream.Write7BitEncodedInt(data.Length);
                    stream.Write(data);
                    return;
                }
                catch
                {
                    m_tryProtobuf = false;
                }
            }
            var str = MyAPIGateway.Utilities.SerializeToXML(input);
            stream.Write(str);
        }
    }
}
