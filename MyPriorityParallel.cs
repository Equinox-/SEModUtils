using System;
using Sandbox.ModAPI;
using VRage;

namespace Equinox.ProceduralWorld.Utils
{
    public class MyPriorityParallel
    {
        public static void StartBackground(Action action, Action callback = null)
        {
            // Better way in ModAPI?
            Exception trace = null;
            try
            {
                int.Parse("fail");
            }
            catch (Exception e)
            {
                trace = e;
            }
            MyAPIGateway.Parallel.StartBackground(() =>
            {
                try
                {
                    action();
                    callback?.Invoke();
                }
                catch (Exception e)
                {
                    SessionCore.Log("Background task failed. Reason:\n{0}\nInvoked From:\n{1}", e, trace);
#if DEBUG
                    throw;
#endif
                }
            });
        }

        public static void InvokeOnGameThreadBlocking(Action action)
        {
            // Better way in ModAPI?
            Exception trace = null;
            try
            {
                int.Parse("fail");
            }
            catch (Exception e)
            {
                trace = e;
            }
            var mutex = new FastResourceLock();
            mutex.AcquireExclusive();
            MyAPIGateway.Utilities.InvokeOnGameThread(() =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    SessionCore.Log("Background task failed. Reason:\n{0}\nInvoked From:\n{1}", e, trace);
#if DEBUG
                    throw;
#endif
                }
                finally
                {
                    mutex.ReleaseExclusive();
                }
            });
            mutex.AcquireExclusive();
            mutex.ReleaseExclusive();
        }

        public static void InvokeOnGameThread(Action action, Action callback = null)
        {
            // Better way in ModAPI?
            Exception trace = null;
            try
            {
                int.Parse("fail");
            }
            catch (Exception e)
            {
                trace = e;
            }
            MyAPIGateway.Utilities.InvokeOnGameThread(() =>
            {
                try
                {
                    action();
                    callback?.Invoke();
                }
                catch (Exception e)
                {
                    SessionCore.Log("Background task failed. Reason:\n{0}\nInvoked From:\n{1}", e, trace);
#if DEBUG
                    throw;
#endif
                }
            });
        }
    }
}
