using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equinox.Utils.Session;
using Sandbox.ModAPI;
using VRage.Game.Components;
using VRage.Utils;

namespace Equinox.ProceduralWorld.Utils.Session
{
    public class MyModSessionVRageAdapter : MySessionComponentBase
    {
        public MyModSessionManager Manager { get; }

        public MyModSessionVRageAdapter()
        {
            Manager = new MyModSessionManager();
        }

        private bool m_attached = false;
        private bool m_failed = false;

        public override void UpdateBeforeSimulation()
        {
            if (m_failed) return;
            if (MyAPIGateway.Session == null) return;
            if (!m_attached) Attach();
            if (m_failed) return;
            try
            {
                Manager.UpdateBeforeSimulation();
            }
            catch (Exception e)
            {
                MyLog.Default.Log(MyLogSeverity.Critical, "Failed to update-before-simulation session manager:\n{0}", e);
                m_failed = true;
            }
        }

        public override void UpdateAfterSimulation()
        {
            if (m_failed) return;
            if (MyAPIGateway.Session == null) return;
            if (!m_attached) Attach();
            if (m_failed) return;
            try
            {
                Manager.UpdateAfterSimulation();
            }
            catch (Exception e)
            {
                MyLog.Default.Log(MyLogSeverity.Critical, "Failed to update-after-simulation session manager:\n{0}", e);
                m_failed = true;
            }
        }

        public override void LoadData()
        {
            if (m_failed) return;
        }

        public override void SaveData()
        {
            if (m_failed) return;
            try
            {
                Manager.Save();
            }
            catch (Exception e)
            {
                MyLog.Default.Log(MyLogSeverity.Critical, "Failed to save session manager:\n{0}", e);
                m_failed = true;
            }
        }

        protected override void UnloadData()
        {
            if (m_failed) return;

            Detach();
        }

        private void Attach()
        {
            if (m_failed) return;

            m_attached = true;
            try
            {
                Manager.Attach();
            }
            catch (Exception e)
            {
                MyLog.Default.Log(MyLogSeverity.Critical, "Failed to attach session manager:\n{0}", e);
                m_failed = true;
            }
        }

        private void Detach()
        {
            if (m_failed) return;

            m_attached = false;
            try
            {
                Manager.Detach();
            }
            catch (Exception e)
            {
                MyLog.Default.Log(MyLogSeverity.Critical, "Failed to detach session manager:\n{0}", e);
                m_failed = true;
            }
        }
    }
}
