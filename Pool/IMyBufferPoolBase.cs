using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Equinox.ProceduralWorld.Utils.Pool
{
    public interface IMyBufferPoolBase
    {
        byte[] GetOrCreate(int length);
        void Return(byte[] data);
    }
}
