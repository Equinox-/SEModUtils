namespace Equinox.Utils.Pool
{
    public interface IBufferPoolBase
    {
        byte[] GetOrCreate(int length);
        void Return(byte[] data);
    }
}
