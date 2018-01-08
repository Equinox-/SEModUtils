namespace Equinox.Utils.Cache
{
    public abstract class CacheBase<TK, TV>
    {
        public delegate TV CreateDelegate(TK key);

        public abstract TV GetOrCreate(TK key, CreateDelegate del);

        public abstract void Clear();
        public abstract TV Store(TK key, TV value);
        public abstract bool TryGet(TK key, out TV value);
    }
}
