using System.Text;
using Equinox.Utils.Logging;
using VRage.Utils;

namespace Equinox.Utils.Session
{
    public abstract class MyLoggingSessionComponent : MyModSessionComponent, IMyLogging
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

        public void IncreaseIndent()
        {
            if (m_logger != null)
                m_logger.IncreaseIndent();
            else if (Manager != null)
                Manager.FallbackLogger.IncreaseIndent();
            else
                MyLog.Default.IncreaseIndent();
        }

        public void DecreaseIndent()
        {
            if (m_logger!=null)
                m_logger.DecreaseIndent();
            else if (Manager!=null)
                Manager.FallbackLogger.DecreaseIndent();
            else
                MyLog.Default.DecreaseIndent();
        }

        public void Log(MyLogSeverity severity, string format, params object[] args)
        {
            if (m_logger != null)
                m_logger.Log(severity, format, args);
            else if (Manager != null)
                Manager.FallbackLogger.Log(severity, GetType().Name + ": " + format, args);
            else
                MyLog.Default.Log(severity, GetType().Name + ": " + format, args);
        }

        public void Log(MyLogSeverity severity, StringBuilder message)
        {
            if (m_logger != null)
                m_logger.Log(severity, message);
            else if (Manager != null)
                Manager.FallbackLogger.Log(severity, GetType().Name + ": " + message);
            else
                MyLog.Default.Log(severity, GetType().Name + ": " +message);
        }

        protected override void Attach()
        {
            base.Attach();
            Log(MyLogSeverity.Debug, "Attached");
        }

        protected override void Detach()
        {
            base.Detach();
            Log(MyLogSeverity.Debug, "Detached");
        }
    }
}
