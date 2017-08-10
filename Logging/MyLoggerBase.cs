using System;
using System.Collections.Generic;
using System.Text;
using Equinox.Utils.Session;
using VRage.Game;
using VRage.Utils;

namespace Equinox.Utils.Logging
{
    public class MyObjectBuilder_LoggerBase : MyObjectBuilder_ModSessionComponent
    {
        public MyLogSeverity LogLevel = MyLogSeverity.Debug;
    }

    public abstract class MyLoggerBase : MyModSessionComponent, IMyLoggingBase
    {
        private readonly StringBuilder m_messageBuilder = new StringBuilder();
        private readonly StringBuilder m_indentBuilder = new StringBuilder();

        public MyLogSeverity LogLevel { get; set; } = (MyLogSeverity)0;

        private static readonly Type[] SuppliesDep = { typeof(MyLoggerBase) };
        public override IEnumerable<Type> SuppliedComponents => SuppliesDep;

        protected abstract void Write(StringBuilder message);

        public void IncreaseIndent()
        {
            m_indentBuilder.Append("    ");
        }

        public void DecreaseIndent()
        {
            if (m_indentBuilder.Length >= 4)
                m_indentBuilder.Remove(m_indentBuilder.Length - 4, 4);
        }

        public void Log(MyLogSeverity severity, string prefix, string format, params object[] args)
        {
            if ((int)severity < (int)LogLevel) return;
            lock (m_messageBuilder)
            {
                m_messageBuilder.Append(prefix);
                m_messageBuilder.Append(m_indentBuilder);
                m_messageBuilder.AppendFormat("{0}: ", severity);
                m_messageBuilder.AppendFormat(format, args);
                Write(m_messageBuilder);
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
                m_messageBuilder.Append(m_indentBuilder);
                m_messageBuilder.AppendFormat("{0}: ", severity);
                m_messageBuilder.Append(message);
                Write(m_messageBuilder);
                m_messageBuilder.Clear();
            }
        }

        public void Log(MyLogSeverity severity, StringBuilder message)
        {
            if ((int)severity < (int)LogLevel) return;
            Write(message);
        }
        
        public override void LoadConfiguration(MyObjectBuilder_ModSessionComponent config)
        {
            if (config == null)
                return;
            var up = config as MyObjectBuilder_LoggerBase;
            if (up == null)
            {
                Log(MyLogSeverity.Critical, "Configuration type {0} doesn't match component type {1}", config.GetType(), GetType());
                return;
            }
            LogLevel = up.LogLevel;
        }
    }
}
