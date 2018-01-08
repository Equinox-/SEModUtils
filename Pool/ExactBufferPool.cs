using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Equinox.Utils.Pool
{
    public class ExactBufferPool : IBufferPoolBase
    {
        private class BufferPoolInternal
        {
            public ConcurrentStack<byte[]> Queue { get; } = new ConcurrentStack<byte[]>();
        }

        private long m_currentSize;
        private readonly long m_maximumSize;
        private readonly long m_maximumPooledSize;

        private readonly Dictionary<int, LinkedListNode<BufferPoolInternal>> m_pool = new Dictionary<int, LinkedListNode<BufferPoolInternal>>();
        private readonly LinkedList<BufferPoolInternal> m_bufferLRU = new LinkedList<BufferPoolInternal>();

        public ExactBufferPool(long maximumSize, long maximumPooledSize)
        {
            m_maximumSize = maximumSize;
            m_currentSize = 0;
            m_maximumPooledSize = maximumPooledSize;
        }

        private BufferPoolInternal GetInternalPool(int length)
        {
            lock (this)
            {
                LinkedListNode<BufferPoolInternal> pool;
                if (!m_pool.TryGetValue(length, out pool))
                    pool = m_pool[length] = m_bufferLRU.AddLast(new BufferPoolInternal());
                else
                {
                    m_bufferLRU.Remove(pool);
                    m_bufferLRU.AddLast(pool);
                }
                return pool.Value;
            }
        }

        public byte[] GetOrCreate(int length)
        {
            if (length > m_maximumPooledSize) return new byte[length];
            var pool = GetInternalPool(length);
            byte[] buff;
            if (!pool.Queue.TryPop(out buff)) return new byte[length];
            lock (this)
                m_currentSize -= length;
            return buff;
        }

        public void Return(byte[] buffer)
        {
            if (buffer.Length > m_maximumPooledSize) return;
            var pool = GetInternalPool(buffer.Length);
            if (pool.Queue.Count > 4) return;
            pool.Queue.Push(buffer);
            lock (this)
            {
                m_currentSize += buffer.Length;

                var rtpool = m_bufferLRU.First;
                while (m_currentSize > m_maximumSize && rtpool != null)
                {
                    byte[] tmp;
                    while (rtpool.Value.Queue.TryPop(out tmp) && m_currentSize > m_maximumSize)
                    {
                        m_currentSize -= tmp.Length;
                    }
                    rtpool = rtpool.Next;
                }
            }
        }
    }
}
