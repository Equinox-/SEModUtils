using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Equinox.Utils.Pool
{
    public class MyTypedObjectPool
    {
        private readonly Dictionary<Type, LinkedListNode<ConcurrentStack<object>>> m_cache = new Dictionary<Type, LinkedListNode<ConcurrentStack<object>>>();
        private readonly LinkedList<ConcurrentStack<object>> m_lruList = new LinkedList<ConcurrentStack<object>>();

        private int m_currentCount;
        private readonly int m_maxPerType, m_maxTotal;

        public MyTypedObjectPool(int maxPerType, int maxTotal)
        {
            m_maxPerType = maxPerType;
            m_maxTotal = maxTotal;
            m_currentCount = 0;
        }

        private ConcurrentStack<object> GetSafe(Type type)
        {
            lock (this)
            {
                LinkedListNode<ConcurrentStack<object>> entry;
                if (!m_cache.TryGetValue(type, out entry))
                    entry = m_cache[type] = m_lruList.AddLast(new ConcurrentStack<object>());
                else
                {
                    m_lruList.Remove(entry);
                    m_lruList.AddLast(entry);
                }
                return entry.Value;
            }
        }

        public T GetOrCreate<T>() where T : class, new()
        {
            var stack = GetSafe(typeof(T));
            object result;
            if (stack.TryPop(out result))
            {
                lock (this)
                    m_currentCount--;
                return (T)result;
            }
            return new T();
        }

        public void Return<T>(T value) where T : class
        {
            var stack = GetSafe(typeof(T));
            if (stack.Count >= m_maxPerType)
                return;
            stack.Push(value);
            lock (this)
            {
                m_currentCount++;
                var entry = m_lruList.First;
                while (entry != null && m_currentCount > m_maxTotal)
                {
                    object result;
                    while (m_currentCount > m_maxTotal && entry.Value.TryPop(out result))
                        m_currentCount--;
                    entry = entry.Next;
                }
            }
        }
    }
}
