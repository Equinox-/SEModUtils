using System.Text;
using Equinox.Utils.Session;
using VRage.Utils;

namespace Equinox.Utils.Logging
{
    public class FallbackLogger : ILoggingBase
    {
        private readonly ModSessionManager m_manager;

        public FallbackLogger(ModSessionManager manager)
        {
            m_manager = manager;
        }

        public void IncreaseIndent()
        {
            var logger = m_manager.GetDependencyProvider<LoggerBase>();
            if (logger != null && logger.IsAttached)
                logger.IncreaseIndent();
            else
                MyLog.Default?.IncreaseIndent();
        }

        public void DecreaseIndent()
        {
            var logger = m_manager.GetDependencyProvider<LoggerBase>();
            if (logger != null && logger.IsAttached)
                logger.DecreaseIndent();
            else
                MyLog.Default?.DecreaseIndent();
        }

        private StringBuilder m_builder = new StringBuilder();

        public void Log(MyLogSeverity severity, string format, params object[] args)
        {
            var logger = m_manager.GetDependencyProvider<LoggerBase>();
            if (logger != null && logger.IsAttached)
                logger.Log(severity, format, args);
            else
                lock (m_builder)
                {
                    m_builder.EnsureCapacity(format.Length + 4);
                    m_builder.Append("EPW: ").AppendFormat(format, args);
                    MyLog.Default?.Log(severity, m_builder);
                    m_builder.Clear();
                }
        }

        public void Log(MyLogSeverity severity, StringBuilder message)
        {
            var logger = m_manager.GetDependencyProvider<LoggerBase>();
            if (logger != null && logger.IsAttached)
                logger.Log(severity, message);
            else
                lock (m_builder)
                {
                    m_builder.EnsureCapacity(message.Length + 4);
                    m_builder.Append("EPW: ").Append(message);
                    MyLog.Default?.Log(severity, m_builder);
                    m_builder.Clear();
                }
        }

        public void Log(MyLogSeverity severity, string prefix, string format, params object[] args)
        {
            var logger = m_manager.GetDependencyProvider<LoggerBase>();
            if (logger != null && logger.IsAttached)
                logger.Log(severity, prefix, format, args);
            else
                lock (m_builder)
                {
                    m_builder.EnsureCapacity(format.Length + 4);
                    m_builder.Append("EPW: ").Append(prefix).AppendFormat(format, args);
                    MyLog.Default?.Log(severity, m_builder);
                    m_builder.Clear();
                }
        }

        public void Log(MyLogSeverity severity, string prefix, StringBuilder message)
        {
            var logger = m_manager.GetDependencyProvider<LoggerBase>();
            if (logger != null && logger.IsAttached)
                logger.Log(severity, prefix, message);
            else
                lock (m_builder)
                {
                    m_builder.EnsureCapacity(message.Length + 4);
                    m_builder.Append("EPW: ").Append(prefix).Append(message);
                    MyLog.Default?.Log(severity, m_builder);
                    m_builder.Clear();
                }
        }
    }
}