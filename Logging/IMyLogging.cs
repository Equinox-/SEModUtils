using System;
using System.Text;
using VRage.Utils;

namespace Equinox.Utils.Logging
{
    public interface IMyLogging
    {
        void IncreaseIndent();

        void DecreaseIndent();

        void Log(MyLogSeverity severity, string format, params object[] args);

        void Log(MyLogSeverity severity, StringBuilder message);
    }

    public static class MyLoggingExtension
    {
        private struct IndentToken : IDisposable
        {
            private IMyLogging m_log;

            public IndentToken(IMyLogging logger)
            {
                m_log = logger;
                m_log.IncreaseIndent();
            }

            public void Dispose()
            {
                m_log.DecreaseIndent();
                m_log = null;
            }
        }

        public static IDisposable IndentUsing(this IMyLogging self)
        {
            return new IndentToken(self);
        }

        public static void Debug(this IMyLogging self, string message, params object[] args)
        {
            self.Log(MyLogSeverity.Debug, message, args);
        }
    
        public static void Debug(this IMyLogging self, StringBuilder buillder)
        {
            self.Log(MyLogSeverity.Debug, buillder);
        }

        public static void Info(this IMyLogging self, string message, params object[] args)
        {
            self.Log(MyLogSeverity.Info, message, args);
        }

        public static void Info(this IMyLogging self, StringBuilder buillder)
        {
            self.Log(MyLogSeverity.Info, buillder);
        }

        public static void Warning(this IMyLogging self, string message, params object[] args)
        {
            self.Log(MyLogSeverity.Warning, message, args);
        }

        public static void Warning(this IMyLogging self, StringBuilder buillder)
        {
            self.Log(MyLogSeverity.Warning, buillder);
        }

        public static void Error(this IMyLogging self, string message, params object[] args)
        {
            self.Log(MyLogSeverity.Error, message, args);
        }

        public static void Error(this IMyLogging self, StringBuilder buillder)
        {
            self.Log(MyLogSeverity.Error, buillder);
        }

        public static void Critical(this IMyLogging self, string message, params object[] args)
        {
            self.Log(MyLogSeverity.Critical, message, args);
        }

        public static void Critical(this IMyLogging self, StringBuilder buillder)
        {
            self.Log(MyLogSeverity.Critical, buillder);
        }
    }
}
