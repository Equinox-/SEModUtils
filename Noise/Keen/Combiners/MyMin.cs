﻿
namespace Equinox.Utils.Noise.Keen.Combiners
{
    public class MyMin : IMyModule
    {
        public IMyModule Source1 { get; set; }
        public IMyModule Source2 { get; set; }

        public MyMin(IMyModule sourceModule1, IMyModule sourceModule2)
        {
            Source1 = sourceModule1;
            Source2 = sourceModule2;
        }

        public double GetValue(double x)
        {
            return System.Math.Min(Source1.GetValue(x), Source2.GetValue(x));
        }

        public double GetValue(double x, double y)
        {
            return System.Math.Min(Source1.GetValue(x, y), Source2.GetValue(x, y));
        }

        public double GetValue(double x, double y, double z)
        {
            return System.Math.Min(Source1.GetValue(x, y, z), Source2.GetValue(x, y, z));
        }
    }
}
