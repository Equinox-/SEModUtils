using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VRage;

namespace Equinox.ProceduralWorld.Utils.Pool
{
    public class MyBufferPool : IMyBufferPoolBase
    {
        private readonly LinkedListNode<ConcurrentStack<byte[]>>[] m_pools;
        private readonly LinkedList<ConcurrentStack<byte[]>> m_lruList;
        private readonly int m_minPooledSize, m_maxPooledSize;
        private readonly long m_maxPooledData;
        private long m_currentPooledData;

        public MyBufferPool(long maxPooledData, int minPooledSize, int maxPooledSize)
        {
            m_maxPooledData = maxPooledData;
            var poolCount = 0;
            while (minPooledSize * (1 << poolCount) < maxPooledSize)
                poolCount++;

            m_minPooledSize = minPooledSize;
            m_pools = new LinkedListNode<ConcurrentStack<byte[]>>[poolCount];
            m_lruList = new LinkedList<ConcurrentStack<byte[]>>();
            for (var i = 0; i < poolCount; i++)
                m_pools[i] = m_lruList.AddLast(new ConcurrentStack<byte[]>());
            m_maxPooledSize = m_minPooledSize * (1 << (m_pools.Length - 1));
            m_currentPooledData = 0;
        }

        private int BufferSizeForPool(int pool)
        {
            return m_minPooledSize * (1 << pool);
        }

        private int BufferPoolForLength(int length)
        {
            var poolLeft = 0;
            var poolRight = m_pools.Length - 1;
            while (poolLeft < poolRight)
            {
                var mid = (poolLeft + poolRight) / 2;
                var csize = BufferSizeForPool(mid);
                if (csize == length)
                    return mid;
                else if (csize < length)
                    poolLeft = mid + 1;
                else if (csize > length)
                    poolRight = mid - 1;
            }
            return poolLeft;
        }

        public byte[] GetOrCreate(int length)
        {
            if (length > m_maxPooledSize) return new byte[length];

            var poolID = BufferPoolForLength(length);
            while (BufferSizeForPool(poolID) < length)
                poolID++;
            var poolNode = m_pools[poolID];
            lock (this)
            {
                m_lruList.Remove(poolNode);
                m_lruList.AddLast(poolNode);
            }

            byte[] buff;
            if (!poolNode.Value.TryPop(out buff)) return new byte[BufferSizeForPool(poolID)];
            lock (this)
                m_currentPooledData -= buff.Length;
            return buff;
        }

        public void Return(byte[] data)
        {
            if (data.Length < m_minPooledSize || data.Length > m_maxPooledSize) return;

            var poolID = BufferPoolForLength(data.Length);
            while (BufferSizeForPool(poolID) > data.Length)
                poolID--;
            var poolNode = m_pools[poolID];
            if (poolNode.Previous == null || poolNode.Value.Count > 2) return;
            poolNode.Value.Push(data);
            lock (this)
            {
                m_lruList.Remove(poolNode);
                m_lruList.AddLast(poolNode);
                m_currentPooledData += data.Length;

                var rtpool = m_lruList.First;
                while (m_currentPooledData > m_maxPooledSize && rtpool != null)
                {
                    byte[] tmp;
                    while (rtpool.Value.TryPop(out tmp) && m_currentPooledData > m_maxPooledSize)
                    {
                        m_currentPooledData -= tmp.Length;
                    }
                    rtpool = rtpool.Next;
                }
            }
        }
    }
}
