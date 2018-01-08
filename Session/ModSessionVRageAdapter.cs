using System;
using Equinox.Utils.Session;
using Sandbox.ModAPI;
using VRage.Game.Components;
using VRage.Utils;

namespace Equinox.Utils.Session
{
    public class ModSessionVRageAdapter : MySessionComponentBase
    {
        public ModSessionManager Manager { get; }

        public ModSessionVRageAdapter()
        {
            Manager = new ModSessionManager();
        }

        private bool m_attached = false;
        private bool m_failed = false;

        public override void UpdateBeforeSimulation()
        {
            if (MyAPIGateway.Session == null) return;
            if (m_failed) return;
            if (!m_attached) Attach();
            if (m_failed) return;
            try
            {
                Manager.UpdateBeforeSimulation();
            }
            catch (Exception e)
            {
                Manager.FallbackLogger.Log(MyLogSeverity.Critical, "Failed to update-before-simulation session manager:\n{0}", e);
                m_failed = true;
            }
        }

        public override void UpdateAfterSimulation()
        {
            if (MyAPIGateway.Session == null) return;
            if (m_failed) return;
            if (!m_attached) Attach();
            if (m_failed) return;
            try
            {
                Manager.UpdateAfterSimulation();
            }
            catch (Exception e)
            {
                Manager.FallbackLogger.Log(MyLogSeverity.Critical, "Failed to update-after-simulation session manager:\n{0}", e);
                m_failed = true;
            }
        }

        public override void LoadData()
        {
        }

        public override void SaveData()
        {
            try
            {
                Manager.Save();
            }
            catch (Exception e)
            {
                Manager.FallbackLogger.Log(MyLogSeverity.Critical, "Failed to save session manager:\n{0}", e);
                m_failed = true;
            }
        }

        protected override void UnloadData()
        {
            Detach();
        }

        private void Attach()
        {
            m_attached = true;
            try
            {
                Manager.Attach();
            }
            catch (Exception e)
            {
                Manager.FallbackLogger.Log(MyLogSeverity.Critical, "Failed to attach session manager:\n{0}", e);
                m_failed = true;
            }
        }

        private void Detach()
        {
            m_attached = false;
            try
            {
                Manager.Detach();
            }
            catch (Exception e)
            {
                Manager.FallbackLogger.Log(MyLogSeverity.Critical, "Failed to detach session manager:\n{0}", e);
                m_failed = true;
            }
        }
    }
}
