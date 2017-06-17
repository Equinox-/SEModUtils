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
    public abstract class MySerializer
    {
        public abstract void Read(ref object output, MyMemoryStream stream);
        public abstract void Write(ref object input, MyMemoryStream stream);
    }

    public abstract class MySerializer<T> : MySerializer
    {
        public abstract void Read(ref T output, MyMemoryStream stream);
        public abstract void Write(ref T input, MyMemoryStream stream);

        public override void Read(ref object output, MyMemoryStream stream)
        {
            var result = (T)output;
            Read(ref result, stream);
            output = result;
        }

        public override void Write(ref object input, MyMemoryStream stream)
        {
            var unbox = (T)input;
            Write(ref unbox, stream);
        }
    }

    public class MyVec3Serializer : MySerializer<Vector3D>
    {
        public override void Read(ref Vector3D output, MyMemoryStream stream)
        {
            stream.Read(ref output);
        }

        public override void Write(ref Vector3D input, MyMemoryStream stream)
        {
            stream.Write(ref input);
        }
    }

    public class MySerializerBase<T> : MySerializer<T>
    {
        public delegate void MemoryStreamIo(ref T inout, MyMemoryStream stream);

        private readonly MemoryStreamIo m_read, m_write;

        public MySerializerBase(MemoryStreamIo read, MemoryStreamIo write)
        {
            m_read = read;
            m_write = write;
        }

        public override void Read(ref T output, MyMemoryStream stream)
        {
            m_read.Invoke(ref output, stream);
        }

        public override void Write(ref T input, MyMemoryStream stream)
        {
            m_write.Invoke(ref input, stream);
        }
    }

    public class MySerializerNoRefBase<T> : MySerializer<T>
    {
        public delegate void MemoryStreamIo(ref T inout, MyMemoryStream stream);

        private readonly Action<T, MyMemoryStream> m_write;
        private readonly Func<MyMemoryStream, T> m_read;

        public MySerializerNoRefBase(Func<MyMemoryStream, T> read, Action<T, MyMemoryStream> write)
        {
            m_read = read;
            m_write = write;
        }

        public override void Read(ref T output, MyMemoryStream stream)
        {
            output = m_read.Invoke(stream);
        }

        public override void Write(ref T input, MyMemoryStream stream)
        {
            m_write.Invoke(input, stream);
        }
    }

    // Improvable maybe?  Very short lived allocations so not too bad.
    public class MyFallbackSerializer<T> : MySerializer<T>
    {
        private bool m_tryProtobuf = true;

        public override void Read(ref T output, MyMemoryStream stream)
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

        public override void Write(ref T input, MyMemoryStream stream)
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
