using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRageMath;

namespace Equinox.Utils
{
    // ReSharper disable InconsistentNaming
    public static class VectorSwizzling
    {
        public static Vector3D XYZ(this Vector4D vec)
        {
            return new Vector3D(vec.X, vec.Y, vec.Z);
        }
    }
}
