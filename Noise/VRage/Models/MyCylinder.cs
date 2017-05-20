
using System;

namespace Equinox.Utils.Noise.VRage.Models
{
    /// <summary>
    /// Maps the output of a module onto a cylinder.
    /// </summary>
    class MyCylinder : IMyModule
    {
        public IMyModule Module { get; set; }

        public MyCylinder(IMyModule module)
        {
            Module = module;
        }

        public double GetValue(double x)
        {
            throw new NotSupportedException();
        }

        public double GetValue(double angle, double height)
        {
            double x = System.Math.Cos(angle);
            double y = height;
            double z = System.Math.Sin(angle);

            return Module.GetValue(x, y, z);
        }

        public double GetValue(double x, double y, double z)
        {
            throw new NotSupportedException();
        }
    }
}
