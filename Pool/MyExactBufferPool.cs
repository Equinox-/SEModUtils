﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equinox.ProceduralWorld.Utils.Pool;
using VRage.Collections;

namespace Equinox.Utils.Pool
{
    public class MyExactBufferPool : IMyBufferPoolBase
    {
        private class MyBufferPoolInternal
        {
            public ConcurrentStack<byte[]> Queue { get; } = new ConcurrentStack<byte[]>();
        }

        private long m_currentSize;
        private readonly long m_maximumSize;
        private readonly long m_maximumPooledSize;

        private readonly Dictionary<int, LinkedListNode<MyBufferPoolInternal>> m_pool = new Dictionary<int, LinkedListNode<MyBufferPoolInternal>>();
        private readonly LinkedList<MyBufferPoolInternal> m_bufferLRU = new LinkedList<MyBufferPoolInternal>();

        public MyExactBufferPool(long maximumSize, long maximumPooledSize)
        {
            m_maximumSize = maximumSize;
            m_currentSize = 0;
            m_maximumPooledSize = maximumPooledSize;
        }

        private MyBufferPoolInternal GetInternalPool(int length)
        {
            lock (this)
            {
                LinkedListNode<MyBufferPoolInternal> pool;
                if (!m_pool.TryGetValue(length, out pool))
                    pool = m_pool[length] = m_bufferLRU.AddLast(new MyBufferPoolInternal());
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