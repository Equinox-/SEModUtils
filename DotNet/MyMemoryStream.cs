using System;
using System.Collections.Generic;
using System.Text;
using VRageMath;

namespace Equinox.Utils.DotNet
{
    public class MyMemoryStream : IDisposable
    {
        private readonly byte[] m_buffer = new byte[256];

        private static readonly Queue<byte[]> BufferCache = new Queue<byte[]>();
        public bool IsBufferShared { get; set; }
        public bool IsReallocationAllowed { get; set; }

        public byte[] Backing { get; private set; }
        public int WriteHead { get; set; }
        public int ReadHead { get; set; }

        /// <summary>
        /// For serializing to an automatically allocated buffer from a pool.
        /// Creates an empty read/write stream with the given initial capacity.
        /// </summary>
        /// <param name="initialCapacity">Initial capacity, in bytes</param>
        /// <param name="expandable">Should the stream auto-expand</param>
        /// <returns></returns>
        public static MyMemoryStream CreateEmptyStream(int initialCapacity, bool expandable = true)
        {
            var stream = new MyMemoryStream()
            {
                WriteHead = 0,
                ReadHead = 0,
                IsBufferShared = false,
                IsReallocationAllowed = true
            };
            stream.Resize(initialCapacity);
            stream.IsReallocationAllowed = false;
            return stream;
        }

        /// <summary>
        /// For serializing to a fixed buffer.
        /// Creates a non-expandable memory stream backed by the given buffer, with write and read heads at zero.
        /// </summary>
        /// <param name="buffer">Backing buffer</param>
        /// <returns></returns>
        public static MyMemoryStream CreateWriterFor(byte[] buffer)
        {
            var stream = new MyMemoryStream()
            {
                WriteHead = 0,
                ReadHead = 0,
                IsBufferShared = true,
                IsReallocationAllowed = false,
                Backing = buffer
            };
            return stream;
        }

        /// <summary>
        /// For deserializing from a buffer.
        /// Creates a non-expandable memory stream backed by the given buffer, with the write head at the end of the stream, and the read head at the beginning.
        /// </summary>
        /// <param name="buffer">Backing buffer</param>
        /// <returns></returns>
        public static MyMemoryStream CreateReaderFor(byte[] buffer)
        {
            return new MyMemoryStream()
            {
                IsBufferShared = true,
                IsReallocationAllowed = false,
                WriteHead = buffer.Length,
                Backing = buffer,
                ReadHead = 0
            };
        }

        private MyMemoryStream()
        {
        }

        public void Dispose()
        {
            if (Backing != null && !IsBufferShared)
                BufferCache.Enqueue(Backing);
        }

        public void Resize(int capacity)
        {
            if (Backing != null && Backing.Length > capacity) return;
            if (!IsReallocationAllowed) throw new InvalidOperationException("This memory stream doesn't allow re-allocation!");
            var nbuf = (byte[])null;
            while ((nbuf == null || nbuf.Length < capacity) && BufferCache.Count > 0)
                nbuf = BufferCache.Dequeue();
            if (nbuf == null || nbuf.Length < capacity)
            {
                var nsize = Backing?.Length * 2 ?? capacity;
                while (nsize < capacity)
                    nsize <<= 1;
                nbuf = new byte[capacity];
            }
            if (Backing != null)
            {
                Array.Copy(Backing, 0, nbuf, 0, WriteHead);
                BufferCache.Enqueue(Backing);
            }
            Backing = nbuf;
            IsBufferShared = false;
        }

        public void Write(byte[] data, int offset, int count)
        {
            Resize(WriteHead + count);
            Array.Copy(data, offset, Backing, WriteHead, count);
            WriteHead += count;
        }

        public int Read(byte[] data, int offset, int count)
        {
            var rc = Math.Min(count, WriteHead - ReadHead);
            if (rc <= 0) return rc;
            Array.Copy(Backing, ReadHead, data, offset, rc);
            ReadHead += rc;
            return rc;
        }

        public void Write(byte[] data)
        {
            Write(data, 0, data.Length);
        }

        public void Write(byte val)
        {
            Resize(WriteHead + 1);
            Backing[WriteHead++] = val;
        }

        public byte ReadByte()
        {
            if (WriteHead <= ReadHead + 1) throw new InvalidOperationException();
            return Backing[ReadHead++];
        }

        public void Write(short val)
        {
            Resize(WriteHead + 2);
            if (BitConverter.IsLittleEndian)
            {
                Backing[WriteHead++] = (byte)((val >> 0) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 8) & 0xFF);
            }
            else
            {
                Backing[WriteHead++] = (byte)((val >> 8) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 0) & 0xFF);
            }
        }
        public int ReadInt16()
        {
            if (WriteHead <= ReadHead + 2) throw new InvalidOperationException();
            var res = BitConverter.ToInt16(Backing, ReadHead);
            ReadHead += 2;
            return res;
        }

        public void Write(ushort val)
        {
            Write((short)val);
        }

        public uint ReadUInt16()
        {
            if (WriteHead <= ReadHead + 2) throw new InvalidOperationException();
            var res = BitConverter.ToUInt16(Backing, ReadHead);
            ReadHead += 2;
            return res;
        }


        public void Write(int val)
        {
            Resize(WriteHead + 4);
            if (BitConverter.IsLittleEndian)
            {
                Backing[WriteHead++] = (byte)((val >> 0) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 8) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 16) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 24) & 0xFF);
            }
            else
            {
                Backing[WriteHead++] = (byte)((val >> 24) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 16) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 8) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 0) & 0xFF);
            }
        }

        public int ReadInt32()
        {
            if (WriteHead <= ReadHead + 4) throw new InvalidOperationException();
            var res = BitConverter.ToInt32(Backing, ReadHead);
            ReadHead += 4;
            return res;
        }

        public void Write(uint val)
        {
            Write((int)val);
        }

        public uint ReadUInt32()
        {
            if (WriteHead <= ReadHead + 4) throw new InvalidOperationException();
            var res = BitConverter.ToUInt32(Backing, ReadHead);
            ReadHead += 4;
            return res;
        }

        public void Write(long val)
        {
            Resize(WriteHead + 8);
            if (BitConverter.IsLittleEndian)
            {
                Backing[WriteHead++] = (byte)((val >> 0) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 8) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 16) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 24) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 32) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 40) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 48) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 56) & 0xFF);
            }
            else
            {
                Backing[WriteHead++] = (byte)((val >> 56) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 48) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 40) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 32) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 24) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 16) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 8) & 0xFF);
                Backing[WriteHead++] = (byte)((val >> 0) & 0xFF);
            }
        }

        public long ReadInt64()
        {
            if (WriteHead <= ReadHead + 8) throw new InvalidOperationException();
            var res = BitConverter.ToInt64(Backing, ReadHead);
            ReadHead += 8;
            return res;
        }

        public void Write(ulong val)
        {
            Write((long)val);
        }

        public ulong ReadUInt64()
        {
            if (WriteHead <= ReadHead + 8) throw new InvalidOperationException();
            var res = BitConverter.ToUInt64(Backing, ReadHead);
            ReadHead += 8;
            return res;
        }

        public void Write(float val)
        {
            // This is _extremely_ annoying.
            Write(BitConverter.GetBytes(val));
        }
        public float ReadFloat()
        {
            return ReadSingle();
        }

        public float ReadSingle()
        {
            if (WriteHead <= ReadHead + 4) throw new InvalidOperationException();
            var res = BitConverter.ToSingle(Backing, ReadHead);
            ReadHead += 4;
            return res;
        }

        public void Write(double val)
        {
            Write(BitConverter.DoubleToInt64Bits(val));
        }

        public double ReadDouble()
        {
            if (WriteHead <= ReadHead + 8) throw new InvalidOperationException();
            var res = BitConverter.ToDouble(Backing, ReadHead);
            ReadHead += 8;
            return res;
        }

        #region string
        public void Write(string text, Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var byteCount = encoding.GetByteCount(text);
            var buffer = m_buffer;
            if (byteCount > buffer.Length)
                buffer = new byte[byteCount];
            var actualCount = encoding.GetBytes(text, 0, text.Length, buffer, 0);
            Write7BitEncodedInt(actualCount);
            Write(buffer, 0, actualCount);
        }

        public string ReadString(Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            if (WriteHead <= ReadHead) throw new InvalidOperationException();
            var byteCount = Backing[ReadHead++];
            if (WriteHead <= ReadHead + byteCount) throw new InvalidOperationException();
            var res = encoding.GetString(Backing, ReadHead, byteCount);
            ReadHead += byteCount;
            return res;
        }
        #endregion

        #region 7BitEncodedInt
        public void Write7BitEncodedInt(int value)
        {
            Resize(WriteHead + 5);
            var uval = (uint)value;
            while (uval >= 0x80U)
            {
                Backing[WriteHead++] = (byte)(uval | 0x80U);
                uval >>= 7;
            }
            Backing[WriteHead++] = (byte)uval;
        }

        public int Read7BitEncodedInt()
        {
            if (WriteHead <= ReadHead) throw new InvalidOperationException();
            var tmpval = (uint)Backing[ReadHead++];
            var outval = tmpval & ~0x80U;
            while (tmpval >= 0x80U)
            {
                if (WriteHead <= ReadHead) throw new InvalidOperationException();
                tmpval = Backing[ReadHead++];
                outval = (outval << 7) | (tmpval & ~0x80U);
            }
            return (int)outval;
        }
        #endregion

        #region Vector3
        public void Write(ref Vector3 v)
        {
            Write(v.X);
            Write(v.Y);
            Write(v.Z);
        }

        public void Write(Vector3 v)
        {
            Write(ref v);
        }

        public void Read(ref Vector3 v)
        {
            v.X = ReadSingle();
            v.Y = ReadSingle();
            v.Z = ReadSingle();
        }

        public Vector3 ReadVector3()
        {
            var tmp = new Vector3();
            Read(ref tmp);
            return tmp;
        }
        #endregion
        #region Vector3D
        public void Write(ref Vector3D v)
        {
            Write(v.X);
            Write(v.Y);
            Write(v.Z);
        }

        public void Write(Vector3D v)
        {
            Write(ref v);
        }

        public void Read(ref Vector3D v)
        {
            v.X = ReadDouble();
            v.Y = ReadDouble();
            v.Z = ReadDouble();
        }

        public Vector3D ReadVector3D()
        {
            var tmp = new Vector3();
            Read(ref tmp);
            return tmp;
        }
        #endregion
        #region Vector3I
        public void Write(ref Vector3I v)
        {
            Write(v.X);
            Write(v.Y);
            Write(v.Z);
        }

        public void Write(Vector3I v)
        {
            Write(ref v);
        }

        public void Read(ref Vector3I v)
        {
            v.X = ReadInt32();
            v.Y = ReadInt32();
            v.Z = ReadInt32();
        }

        public Vector3I ReadVector3I()
        {
            var tmp = new Vector3I();
            Read(ref tmp);
            return tmp;
        }
        #endregion

        #region Vector4
        public void Write(ref Vector4 v)
        {
            Write(v.X);
            Write(v.Y);
            Write(v.Z);
            Write(v.W);
        }

        public void Write(Vector4 v)
        {
            Write(ref v);
        }

        public void Read(ref Vector4 v)
        {
            v.X = ReadSingle();
            v.Y = ReadSingle();
            v.Z = ReadSingle();
            v.W = ReadSingle();
        }

        public Vector4 ReadVector4()
        {
            var tmp = new Vector4();
            Read(ref tmp);
            return tmp;
        }
        #endregion
        #region Vector4D
        public void Write(ref Vector4D v)
        {
            Write(v.X);
            Write(v.Y);
            Write(v.Z);
            Write(v.W);
        }

        public void Write(Vector4D v)
        {
            Write(ref v);
        }

        public void Read(ref Vector4D v)
        {
            v.X = ReadDouble();
            v.Y = ReadDouble();
            v.Z = ReadDouble();
            v.W = ReadDouble();
        }

        public Vector4D ReadVector4D()
        {
            var tmp = new Vector4D();
            Read(ref tmp);
            return tmp;
        }
        #endregion
        #region Vector4I
        public void Write(ref Vector4I v)
        {
            Write(v.X);
            Write(v.Y);
            Write(v.Z);
            Write(v.W);
        }

        public void Write(Vector4I v)
        {
            Write(ref v);
        }

        public void Read(ref Vector4I v)
        {
            v.X = ReadInt32();
            v.Y = ReadInt32();
            v.Z = ReadInt32();
            v.W = ReadInt32();
        }

        public Vector4I ReadVector4I()
        {
            var tmp = new Vector4I();
            Read(ref tmp);
            return tmp;
        }
        #endregion

        #region Quaternion
        public void Write(ref Quaternion v)
        {
            Write(v.X);
            Write(v.Y);
            Write(v.Z);
            Write(v.W);
        }

        public void Write(Quaternion v)
        {
            Write(ref v);
        }

        public void Read(ref Quaternion v)
        {
            v.X = ReadSingle();
            v.Y = ReadSingle();
            v.Z = ReadSingle();
            v.W = ReadSingle();
        }

        public Quaternion ReadQuaternion()
        {
            var tmp = new Quaternion();
            Read(ref tmp);
            return tmp;
        }
        #endregion
        #region QuaternionD
        public void Write(ref QuaternionD v)
        {
            Write(v.X);
            Write(v.Y);
            Write(v.Z);
            Write(v.W);
        }

        public void Write(QuaternionD v)
        {
            Write(ref v);
        }

        public void Read(ref QuaternionD v)
        {
            v.X = ReadDouble();
            v.Y = ReadDouble();
            v.Z = ReadDouble();
            v.W = ReadDouble();
        }

        public QuaternionD ReadQuaternionD()
        {
            var tmp = new QuaternionD();
            Read(ref tmp);
            return tmp;
        }
        #endregion

        #region Matrix
        public void Write(ref Matrix v)
        {
            Write(v.M11);
            Write(v.M21);
            Write(v.M31);
            Write(v.M41);

            Write(v.M12);
            Write(v.M22);
            Write(v.M32);
            Write(v.M42);

            Write(v.M13);
            Write(v.M23);
            Write(v.M33);
            Write(v.M43);

            Write(v.M14);
            Write(v.M24);
            Write(v.M34);
            Write(v.M44);
        }

        public void Write(Matrix v)
        {
            Write(ref v);
        }

        public void Read(ref Matrix v)
        {
            v.M11 = ReadSingle();
            v.M21 = ReadSingle();
            v.M31 = ReadSingle();
            v.M41 = ReadSingle();

            v.M12 = ReadSingle();
            v.M22 = ReadSingle();
            v.M32 = ReadSingle();
            v.M42 = ReadSingle();

            v.M13 = ReadSingle();
            v.M23 = ReadSingle();
            v.M33 = ReadSingle();
            v.M43 = ReadSingle();

            v.M14 = ReadSingle();
            v.M24 = ReadSingle();
            v.M34 = ReadSingle();
            v.M44 = ReadSingle();
        }

        public Matrix ReadMatrix()
        {
            var tmp = new Matrix();
            Read(ref tmp);
            return tmp;
        }
        #endregion
        #region MatrixD
        public void Write(ref MatrixD v)
        {
            Write(v.M11);
            Write(v.M21);
            Write(v.M31);
            Write(v.M41);

            Write(v.M12);
            Write(v.M22);
            Write(v.M32);
            Write(v.M42);

            Write(v.M13);
            Write(v.M23);
            Write(v.M33);
            Write(v.M43);

            Write(v.M14);
            Write(v.M24);
            Write(v.M34);
            Write(v.M44);
        }

        public void Write(MatrixD v)
        {
            Write(ref v);
        }

        public void Read(ref MatrixD v)
        {
            v.M11 = ReadDouble();
            v.M21 = ReadDouble();
            v.M31 = ReadDouble();
            v.M41 = ReadDouble();

            v.M12 = ReadDouble();
            v.M22 = ReadDouble();
            v.M32 = ReadDouble();
            v.M42 = ReadDouble();

            v.M13 = ReadDouble();
            v.M23 = ReadDouble();
            v.M33 = ReadDouble();
            v.M43 = ReadDouble();

            v.M14 = ReadDouble();
            v.M24 = ReadDouble();
            v.M34 = ReadDouble();
            v.M44 = ReadDouble();
        }

        public MatrixD ReadMatrixD()
        {
            var tmp = new MatrixD();
            Read(ref tmp);
            return tmp;
        }
        #endregion
        #region MatrixI
        public void Write(ref MatrixI v)
        {
            Write(ref v.Translation);
            Write(ref v.Backward);
            Write(ref v.Right);
            Write(ref v.Up);
        }

        public void Write(MatrixI v)
        {
            Write(ref v);
        }

        public void Read(ref MatrixI v)
        {
            Read(ref v.Translation);
            Read(ref v.Backward);
            Read(ref v.Right);
            Read(ref v.Up);
        }

        public MatrixI ReadMatrixI()
        {
            var tmp = new MatrixI();
            Read(ref tmp);
            return tmp;
        }
        #endregion

        #region Base6Directions.Direction
        public void Write(ref Base6Directions.Direction dir)
        {
            Write((byte)dir);
        }

        public void Write(Base6Directions.Direction dir)
        {
            Write(ref dir);
        }

        public void Read(ref Base6Directions.Direction dir)
        {
            dir = (Base6Directions.Direction)ReadByte();
        }

        public Base6Directions.Direction ReadBase6Direction()
        {
            var dir = Base6Directions.Direction.Forward;
            Read(ref dir);
            return dir;
        }
        #endregion

        #region BlockOrientation
        public void Write(ref MyBlockOrientation dir)
        {
            Write(ref dir.Forward);
            Write(ref dir.Up);
        }

        public void Write(MyBlockOrientation dir)
        {
            Write(ref dir);
        }

        public void Read(ref MyBlockOrientation dir)
        {
            Read(ref dir.Forward);
            Read(ref dir.Up);
        }

        public MyBlockOrientation ReadBlockOrientation()
        {
            var dir = new MyBlockOrientation();
            Read(ref dir);
            return dir;
        }
        #endregion
    }
}
