namespace Equinox.ProceduralWorld.Utils.Pool
{
    public interface IMyBufferPoolBase
    {
        byte[] GetOrCreate(int length);
        void Return(byte[] data);
    }
}
