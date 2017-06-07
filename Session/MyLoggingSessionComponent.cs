using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equinox.ProceduralWorld.Utils.Logging;
using Equinox.Utils.Logging;
using Equinox.Utils.Session;
using Sandbox.ModAPI;
using VRage.Utils;

namespace Equinox.ProceduralWorld.Utils.Session
{
    public abstract class MyLoggingSessionComponent : MyModSessionComponent
    {
        private IMyLogging m_logger;

        protected MyLoggingSessionComponent()
        {
            m_logger = null;

            var builder = new StringBuilder(48);
            builder.Append(GetType().Name).Append(" ");
            while (builder.Length < 48)
                builder.Append(' ');
            var simpleName = builder.ToString();
            DependsOn((MyLoggerBase y) =>
            {
                m_logger = y?.CreateProxy(simpleName);
            });
        }

        public void Log(MyLogSeverity severity, string format, params object[] args)
        {
            if (m_logger != null)
                m_logger.Log(severity, format, args);
            else
                MyLog.Default?.Log(severity, format, args);
        }

        public override void Attach()
        {
            base.Attach();
            Log(MyLogSeverity.Debug, "Attached");
        }

        public override void Detach()
        {
            base.Detach();
            Log(MyLogSeverity.Debug, "Detached");
        }
    }
}
