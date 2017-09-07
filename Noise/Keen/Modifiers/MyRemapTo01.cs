namespace Equinox.Utils.Noise.Keen.Modifiers
{
    public class MyRemapTo01: IMyModule
    {
        public IMyModule Module {get; set;}

        public MyRemapTo01(IMyModule module)
        {
            this.Module = module;
        }

        public double GetValue(double x)
        {
            return (Module.GetValue(x) + 1.0)*0.5;
        }

        public double GetValue(double x, double y)
        {
            return (Module.GetValue(x, y) + 1.0)*0.5;
        }

        public double GetValue(double x, double y, double z)
        {
            return (Module.GetValue(x, y, z) + 1.0)*0.5;
        }
    }
}
