using System;
using System.IO;
using System.Text;
using Equinox.Utils.Session;
using Sandbox.ModAPI;
using VRage;
using VRage.Utils;

namespace Equinox.Utils.Logging
{
    public class MyCustomLogger : MyLoggerBase
    {
        public const string DefaultLogFile = "ProceduralWorld.log";

        private readonly FastResourceLock m_lock, m_writeLock;
        private readonly StringBuilder m_cache;
        private string m_file;
        private TextWriter m_writer;
        private DateTime m_lastWriteTime;
        private int m_readyTicks;

        private const int WRITE_INTERVAL_TICKS = 30;
        private static readonly TimeSpan WriteIntervalTime = new TimeSpan(
            0, 0, 1);

        public MyCustomLogger()
        {
            m_file = DefaultLogFile;
            m_writer = null;
            m_lock = new FastResourceLock();
            m_writeLock = new FastResourceLock();
            m_cache = new StringBuilder();
            m_readyTicks = 0;
            m_lastWriteTime = DateTime.Now;
        }

        protected override void Attach()
        {
            base.Attach();
            MyLog.Default.WriteLineAndConsole("Starting logger for Equinox on (" + m_file + ")");
        }

        public override void UpdateAfterSimulation()
        {
            base.UpdateAfterSimulation();
            var requiresUpdate = false;
            using (m_lock.AcquireExclusiveUsing())
                requiresUpdate = m_cache.Length > 0;
            if (requiresUpdate)
                m_readyTicks++;
            else
                m_readyTicks = 0;
            if (m_readyTicks <= WRITE_INTERVAL_TICKS) return;
            Flush();
            m_readyTicks = 0;
        }

        public void Flush()
        {
            if (MyAPIGateway.Utilities != null)
                MyAPIGateway.Parallel.StartBackground(() =>
                {
                    try
                    {
                        if (m_writer == null)
                        {
                            using (m_writeLock.AcquireExclusiveUsing())
                            {
                                if (m_writer == null)
                                {
                                    m_writer = MyAPIGateway.Session.IsDecider() ? 
                                        MyAPIGateway.Utilities.WriteFileInWorldStorage(m_file, typeof(MyCustomLogger)) : 
                                        MyAPIGateway.Utilities.WriteFileInLocalStorage(m_file, typeof(MyCustomLogger));
                                    MyLog.Default.WriteLine("Opened log for ProceduralWorld");
                                    MyLog.Default.Flush();
                                }
                            }
                        }
                        if (m_writer == null || m_cache.Length <= 0) return;
                        string cache = null;
                        using (m_lock.AcquireExclusiveUsing())
                        {
                            if (m_writer != null && m_cache.Length > 0)
                            {
                                cache = m_cache.ToString();
                                m_cache.Clear();
                                m_lastWriteTime = DateTime.UtcNow;
                            }
                        }
                        if (cache == null || m_writer == null) return;
                        using (m_writeLock.AcquireExclusiveUsing())
                        {
                            m_writer.Write(cache);
                            m_writer.Flush();
                        }
                    }
                    catch (Exception e)
                    {
                        MyLog.Default.WriteLine("Procedural LogDump: \r\n" + e.ToString());
                        MyLog.Default.Flush();
#if DEBUG
                        throw;
#endif
                    }
                });
        }

        protected override void Detach()
        {
            base.Detach();
            if (m_lock == null) return;
            string remains = null;
            if (m_cache != null)
                using (m_lock.AcquireExclusiveUsing())
                {
                    if (m_cache.Length > 0)
                    {
                        remains = m_cache.ToString();
                        m_cache.Clear();
                    }
                }
            if (m_writer == null) return;
            using (m_writeLock.AcquireExclusiveUsing())
            {
                if (remains != null)
                    m_writer.Write(remains);
                m_writer.Close();
                m_writer = null;
            }
        }

        private void WriteLineHeader()
        {
            var now = DateTime.Now;
            m_cache.AppendFormat("[{0,2:D2}:{1,2:D2}:{2,2:D2}] ", now.Hour, now.Minute, now.Second);
        }

        protected override void Write(StringBuilder message)
        {
            var shouldFlush = false;
            using (m_lock.AcquireExclusiveUsing())
            {
                WriteLineHeader();
                m_cache.Append(message);
                m_cache.Append("\r\n");
                shouldFlush = DateTime.UtcNow - m_lastWriteTime > WriteIntervalTime;
            }
            if (shouldFlush)
                Flush();
        }

        public override void LoadConfiguration(MyObjectBuilder_ModSessionComponent config)
        {
            base.LoadConfiguration(config);
            var up = config as MyObjectBuilder_CustomLogger;
            if (up == null)
            {
                Log(MyLogSeverity.Critical, "Configuration type {0} doesn't match component type {1}", config.GetType(), GetType());
                return;
            }
            m_file = up.Filename;
        }

        public override MyObjectBuilder_ModSessionComponent SaveConfiguration()
        {
            var config = new MyObjectBuilder_CustomLogger();
            config.LogLevel = LogLevel;
            config.Filename = m_file;
            return config;
        }
    }

    public class MyObjectBuilder_CustomLogger : MyObjectBuilder_LoggerBase
    {
        public string Filename = MyCustomLogger.DefaultLogFile;
    }
}