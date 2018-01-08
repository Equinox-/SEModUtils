using System.Collections.Generic;
using System.Linq;

namespace Equinox.Utils.Collections
{
    public class MultiDictionary<T1, T2>
    {
        public Dictionary<T1, HashSet<T2>> Backing { get; } = new Dictionary<T1, HashSet<T2>>();

        public bool Add(T1 key, T2 value)
        {
            HashSet<T2> lst;
            if (!Backing.TryGetValue(key, out lst))
                Backing[key] = lst = new HashSet<T2>();
            return lst.Add(value);
        }

        public bool Remove(T1 key, T2 value)
        {
            HashSet<T2> lst;
            if (!Backing.TryGetValue(key, out lst))
                return false;
            var del = lst.Remove(value);
            if (lst.Count == 0)
                Backing.Remove(key);
            return del;
        }

        public bool Remove(T1 key)
        {
            return Backing.Remove(key);
        }

        public IEnumerable<T2> this[T1 key]
        {
            get
            {
                HashSet<T2> lst;
                return !Backing.TryGetValue(key, out lst) ? Enumerable.Empty<T2>() : lst;
            }
        }
    }
}
