using System.Text;
using VRage.Utils;

namespace Equinox.Utils.Logging
{
    public class MyLoggingProxy : IMyLogging
    {
        public readonly IMyLoggingBase Backing;
        public readonly string Prefix;

        public MyLoggingProxy(IMyLoggingBase backing, string prefix)
        {
            Backing = backing;
            Prefix = prefix;
        }
        
        // TODO these should probably only apply to the proxy.
        public void IncreaseIndent()
        {
            Backing.IncreaseIndent();
        }

        public void DecreaseIndent()
        {
            Backing.DecreaseIndent();
        }

        public void Log(MyLogSeverity severity, string format, params object[] args)
        {
            Backing.Log(severity, Prefix, format, args);
        }

        public void Log(MyLogSeverity severity, StringBuilder message)
        {
            Backing.Log(severity, Prefix, message);
        }
    }
}