using System;
using System.IO;
using System.Text;
using System.Threading;
using Equinox.ProceduralWorld.Utils.Logging;
using Equinox.Utils.Session;
using Sandbox.ModAPI;
using VRage;
using VRage.Utils;

namespace Equinox.Utils.Logging
{
    public class MyCustomLogger : MyLoggerBase
    {
        private readonly FastResourceLock m_lock, m_writeLock;
        private readonly StringBuilder m_cache;
        private readonly string m_file;
        private TextWriter m_writer;
        private DateTime m_lastWriteTime;
        private int m_readyTicks;

        private const int WRITE_INTERVAL_TICKS = 30;
        private static readonly TimeSpan WriteIntervalTime = new TimeSpan(
            0, 0, 1);

        public MyCustomLogger(string file)
        {
            m_file = file;
            m_writer = null;
            m_lock = new FastResourceLock();
            m_writeLock = new FastResourceLock();
            m_cache = new StringBuilder();
            m_readyTicks = 0;
            m_lastWriteTime = DateTime.Now;
        }

        public override void Attach()
        {
            base.Attach();
            MyLog.Default.WriteLineAndConsole("Starting logger for Equinox on (" + m_file + ")");
        }

        public override void UpdateAfterSimulation()
        {
            base.UpdateAfterSimulation();
            var requiresUpdate = false;
            try
            {
                m_lock.AcquireExclusive();
                requiresUpdate = m_cache.Length > 0;
            }
            finally
            {
                m_lock.ReleaseExclusive();
            }
            if (requiresUpdate)
                m_readyTicks++;
            else
                m_readyTicks = 0;
            if (m_readyTicks <= WRITE_INTERVAL_TICKS) return;
            Save();
            m_readyTicks = 0;
        }

        public override void Save()
        {
            base.Save();
            if (MyAPIGateway.Utilities != null)
                MyAPIGateway.Parallel.StartBackground(() =>
                {
                    try
                    {

                        if (m_writer == null)
                        {
                            try
                            {
                                m_writeLock.AcquireExclusive();
                                if (m_writer == null)
                                {
                                    m_writer = MyAPIGateway.Utilities.WriteFileInWorldStorage(m_file, typeof(MyCustomLogger));
                                    MyLog.Default.WriteLine("Opened log for ProceduralWorld");
                                }
                            }
                            finally
                            {
                                m_writeLock.ReleaseExclusive();
                            }
                        }
                        if (m_writer == null || m_cache.Length <= 0) return;
                        string cache = null;
                        try
                        {
                            m_lock.AcquireExclusive();
                            if (m_writer != null && m_cache.Length > 0)
                            {
                                cache = m_cache.ToString();
                                m_cache.Clear();
                                m_lastWriteTime = DateTime.UtcNow;
                            }
                        }
                        finally
                        {
                            m_lock.ReleaseExclusive();
                        }
                        if (cache == null || m_writer == null) return;
                        try
                        {
                            m_writeLock.AcquireExclusive();
                            m_writer.Write(cache);
                            m_writer.Flush();
                        }
                        finally
                        {
                            m_writeLock.ReleaseExclusive();
                        }
                    }
                    catch (Exception e)
                    {
                        MyLog.Default.WriteLine("Procedural LogDump: \r\n" + e.ToString());
                    }
                });
        }

        public override void Detach()
        {
            base.Detach();
            if (m_lock == null) return;
            string remains = null;
            if (m_cache != null)
                try
                {
                    m_lock.AcquireExclusive();
                    if (m_cache.Length > 0)
                    {
                        remains = m_cache.ToString();
                        m_cache.Clear();
                    }
                }
                finally
                {
                    m_lock.ReleaseExclusive();
                }
            if (m_writer == null) return;
            try
            {
                m_writeLock.AcquireExclusive();
                if (remains != null)
                    m_writer.Write(remains);
                m_writer.Close();
                m_writer = null;
            }
            finally
            {
                m_writeLock.ReleaseExclusive();
            }
        }

        private void WriteLineHeader()
        {
            var now = DateTime.Now;
            m_cache.AppendFormat("[{0,2:D2}:{1,2:D2}:{2,2:D2}] ", now.Hour, now.Minute, now.Second);
        }

        protected override void Write(MyLogSeverity severity, StringBuilder message)
        {
            var shouldFlush = false;
            try
            {
                m_lock.AcquireExclusive();
                WriteLineHeader();
                m_cache.Append(message);
                m_cache.Append("\r\n");
                shouldFlush = DateTime.UtcNow - m_lastWriteTime > WriteIntervalTime;
            }
            finally
            {
                m_lock.ReleaseExclusive();
            }
            if (shouldFlush)
                Save();
        }
    }
}