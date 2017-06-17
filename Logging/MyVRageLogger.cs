using System.Text;
using Equinox.Utils.Session;
using VRage;
using VRage.Utils;

namespace Equinox.Utils.Logging
{
    public class MyVRageLogger : MyLoggerBase
    {
        private string m_filename;
        private readonly StringBuilder m_appVersion;
        private readonly FastResourceLock m_lock;
        private MyLog m_log;

        public MyVRageLogger()
        {
            m_appVersion = new StringBuilder("1.0.0");
            m_lock = new FastResourceLock();
        }
        
        protected override void Write(StringBuilder message)
        {
            using (m_lock.AcquireExclusiveUsing())
                m_log.WriteLine(message.ToString());
        }

        protected override void Attach()
        {
            base.Attach();
            using (m_lock.AcquireExclusiveUsing())
            {
                m_log = new MyLog();
                m_log.Init("Storage/" + m_filename, m_appVersion);
            }
        }

        public override void Save()
        {
            base.Save();
            using (m_lock.AcquireExclusiveUsing())
                m_log.Flush();
        }

        protected override void Detach()
        {
            base.Detach();
            using (m_lock.AcquireExclusiveUsing())
            {
                m_log.Close();
                m_log = null;
            }
        }

        public override void LoadConfiguration(MyObjectBuilder_ModSessionComponent config)
        {
            base.LoadConfiguration(config);
            var up = config as MyObjectBuilder_VRageLogger;
            if (up == null)
            {
                Log(MyLogSeverity.Critical, "Configuration type {0} doesn't match component type {1}", config.GetType(), GetType());
                return;
            }
            m_filename = up.Filename;
        }

        public override MyObjectBuilder_ModSessionComponent SaveConfiguration()
        {
            var config = new MyObjectBuilder_VRageLogger();
            config.LogLevel = LogLevel;
            config.Filename = m_filename;
            return config;
        }
    }

    public class MyObjectBuilder_VRageLogger : MyObjectBuilder_LoggerBase
    {
        public string Filename;
    }
}
