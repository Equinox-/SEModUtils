using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VRage;
using VRage.Utils;

namespace Equinox.ProceduralWorld.Utils.Logging
{
    public class MyVRageLogger : MyLoggerBase
    {
        private readonly string m_filename;
        private readonly StringBuilder m_appVersion;
        private readonly FastResourceLock m_lock;
        private MyLog m_log;

        public MyVRageLogger(string filename, string appVersion)
        {
            m_filename = filename;
            m_appVersion = new StringBuilder(appVersion);
            m_lock = new FastResourceLock();
        }
        
        protected override void Write(MyLogSeverity severity, StringBuilder message)
        {
            using (m_lock.AcquireExclusiveUsing())
                m_log.Log(severity, message);
        }

        public override void Attach()
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

        public override void Detach()
        {
            base.Detach();
            using (m_lock.AcquireExclusiveUsing())
            {
                m_log.Close();
                m_log = null;
            }
        }
    }
}
