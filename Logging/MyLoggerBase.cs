using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equinox.Utils.Logging;
using Equinox.Utils.Session;
using VRage.Utils;

namespace Equinox.ProceduralWorld.Utils.Logging
{
    public abstract class MyLoggerBase : MyModSessionComponent, IMyLogging
    {
        private readonly StringBuilder m_messageBuilder = new StringBuilder();

        public MyLogSeverity LogLevel { get; set; } = (MyLogSeverity)0;

        private static readonly Type[] SuppliesDep = { typeof(MyLoggerBase) };
        public override IEnumerable<Type> SuppliesComponents => SuppliesDep;

        protected abstract void Write(MyLogSeverity severity, StringBuilder message);

        public void Log(MyLogSeverity severity, string prefix, string format, params object[] args)
        {
            if ((int)severity < (int)LogLevel) return;
            lock (m_messageBuilder)
            {
                m_messageBuilder.Append(prefix);
                m_messageBuilder.AppendFormat(format, args);
                Write(severity, m_messageBuilder);
                m_messageBuilder.Clear();
            }
        }
        public void Log(MyLogSeverity severity, string format, params object[] args)
        {
            Log(severity, "", format, args);
        }

        public void Log(MyLogSeverity severity, string prefix, StringBuilder message)
        {
            if ((int)severity < (int)LogLevel) return;
            lock (m_messageBuilder)
            {
                m_messageBuilder.Append(prefix);
                m_messageBuilder.Append(message);
                Write(severity, m_messageBuilder);
                m_messageBuilder.Clear();
            }
        }

        public void Log(MyLogSeverity severity, StringBuilder message)
        {
            if ((int)severity < (int)LogLevel) return;
            Write(severity, message);
        }


        private class MyLoggingProxy : IMyLogging
        {
            private readonly MyLoggerBase m_backing;
            private readonly string m_prefix;

            public MyLoggingProxy(MyLoggerBase backing, string prefix)
            {
                m_backing = backing;
                m_prefix = prefix;
            }

            public void Log(MyLogSeverity severity, string format, params object[] args)
            {
                m_backing.Log(severity, m_prefix, format, args);
            }

            public void Log(MyLogSeverity severity, StringBuilder message)
            {
                m_backing.Log(severity, m_prefix, message);
            }
        }

        public IMyLogging CreateProxy(string prefix)
        {
            return new MyLoggingProxy(this, prefix);
        }
    }
}
