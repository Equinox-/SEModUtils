using System;
using System.Text;
using Equinox.Utils.Pool;
using VRageMath;

namespace Equinox.Utils.Stream
{
    public class MemoryStream : IDisposable
    {
        private static readonly BufferPool BufferPool = new BufferPool(1024 * 1024 * 32, 1024, 1024 * 1024 * 8);
        private readonly byte[] m_buffer = new byte[256];
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
        public static MemoryStream CreateEmptyStream(int initialCapacity, bool expandable = true)
        {
            var stream = new MemoryStream()
            {
                WriteHead = 0,
                ReadHead = 0,
                IsBufferShared = false,
                IsReallocationAllowed = expandable
            };
            stream.Resize(initialCapacity);
            stream.IsReallocationAllowed = expandable;
            return stream;
        }

        /// <summary>
        /// For serializing to a fixed buffer.
        /// Creates a non-expandable memory stream backed by the given buffer, with write and read heads at zero.
        /// </summary>
        /// <param name="buffer">Backing buffer</param>
        /// <returns></returns>
        public static MemoryStream CreateWriterFor(byte[] buffer)
        {
            var stream = new MemoryStream()
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
        public static MemoryStream CreateReaderFor(byte[] buffer)
        {
            return new MemoryStream()
            {
                IsBufferShared = true,
                IsReallocationAllowed = false,
                WriteHead = buffer.Length,
                Backing = buffer,
                ReadHead = 0
            };
        }

        private MemoryStream()
        {
        }

        public void Dispose()
        {
            if (Backing != null && !IsBufferShared)
                BufferPool.Return(Backing);
        }

        public void Resize(int capacity)
        {
            if (Backing != null && Backing.Length > capacity) return;
            if (!IsReallocationAllowed) throw new InvalidOperationException("This memory stream doesn't allow re-allocation!");
            var nbuf = BufferPool.GetOrCreate(capacity);
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
                BufferPool.Return(Backing);
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

        public void Write(ref byte val)
        {
            Resize(WriteHead + 1);
            Backing[WriteHead++] = val;
        }

        public void Read(ref byte val)
        {
            if (WriteHead < ReadHead + 1) throw new InvalidOperationException($"Needs 1 byte.  WH={WriteHead}, RH={ReadHead}");
            val = Backing[ReadHead++];
        }

        public void Write(ref sbyte val)
        {
            Resize(WriteHead + 1);
            Backing[WriteHead++] = (byte) val;
        }

        public void Read(ref sbyte val)
        {
            if (WriteHead < ReadHead + 1) throw new InvalidOperationException($"Needs 1 byte.  WH={WriteHead}, RH={ReadHead}");
            val = (sbyte) Backing[ReadHead++];
        }

        public void Write(ref short val)
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
        public void Read(ref short val)
        {
            if (WriteHead < ReadHead + 2) throw new InvalidOperationException($"Needs 2 bytes.  WH={WriteHead}, RH={ReadHead}");
            val = BitConverter.ToInt16(Backing, ReadHead);
            ReadHead += 2;
        }

        public void Write(ref ushort val)
        {
            var conv = (short) val;
            Write(ref conv);
        }

        public void Read(ref ushort res)
        {
            if (WriteHead < ReadHead + 2) throw new InvalidOperationException($"Needs 2 bytes.  WH={WriteHead}, RH={ReadHead}");
            res = BitConverter.ToUInt16(Backing, ReadHead);
            ReadHead += 2;
        }

        public void Write(ref int val)
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

        public void Read(ref int res)
        {
            if (WriteHead < ReadHead +4) throw new InvalidOperationException($"Needs 4 bytes.  WH={WriteHead}, RH={ReadHead}");
            res = BitConverter.ToInt32(Backing, ReadHead);
            ReadHead += 4;
        }

        public void Write(ref uint val)
        {
            var ic2 = (int)val;
            Write(ref ic2);
        }

        public void Read(ref uint res)
        {
            if (WriteHead < ReadHead + 4) throw new InvalidOperationException($"Needs 4 bytes.  WH={WriteHead}, RH={ReadHead}");
            res = BitConverter.ToUInt32(Backing, ReadHead);
            ReadHead += 4;
        }

        public void Write(ref long val)
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

        public void Read(ref long val)
        {
            if (WriteHead < ReadHead + 8) throw new InvalidOperationException($"Needs 8 bytes.  WH={WriteHead}, RH={ReadHead}");
            val = BitConverter.ToInt64(Backing, ReadHead);
            ReadHead += 8;
        }

        public void Write(ref ulong val)
        {
            var ival = (long)val;
            Write(ref ival);
        }

        public void Read(ref ulong res)
        {
            if (WriteHead < ReadHead + 8) throw new InvalidOperationException($"Needs 8 bytes.  WH={WriteHead}, RH={ReadHead}");
            res = BitConverter.ToUInt64(Backing, ReadHead);
            ReadHead += 8;
        }

        public void Write(ref float val)
        {
            // This is _extremely_ annoying.
            Write(BitConverter.GetBytes(val));
        }
        public void Read(ref float res)
        {
            if (WriteHead < ReadHead + 4) throw new InvalidOperationException($"Needs 4 bytes.  WH={WriteHead}, RH={ReadHead}");
            res = BitConverter.ToSingle(Backing, ReadHead);
            ReadHead += 4;
        }

        public void Write(ref double val)
        {
            var lbits = BitConverter.DoubleToInt64Bits(val);
            Write(ref lbits);
        }

        public void Read(ref double res)
        {
            if (WriteHead < ReadHead + 8) throw new InvalidOperationException($"Needs 8 bytes.  WH={WriteHead}, RH={ReadHead}");
            res = BitConverter.ToDouble(Backing, ReadHead);
            ReadHead += 8;
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
            var byteCount = Read7BitEncodedInt();
            if (WriteHead < ReadHead + byteCount) throw new InvalidOperationException($"Need {byteCount} bytes.  WH={WriteHead}, RH={ReadHead}");
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
            if (WriteHead < ReadHead + 1) throw new InvalidOperationException($"Need 1 byte.  WH={WriteHead}, RH={ReadHead}");
            var tmpval = (uint)Backing[ReadHead++];
            var outval = tmpval & ~0x80U;
            var shift = 7;
            while (tmpval >= 0x80U)
            {
                if (WriteHead < ReadHead + 1) throw new InvalidOperationException($"Need 1 byte.  WH={WriteHead}, RH={ReadHead}");
                tmpval = Backing[ReadHead++];
                outval |= (tmpval & ~0x80U) << shift;
                shift += 7;
            }
            return (int)outval;
        }
        #endregion
    }
}
