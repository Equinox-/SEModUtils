using System;
using Sandbox.ModAPI;
using VRage;

namespace Equinox.ProceduralWorld.Utils
{
    public class MyPriorityParallel
    {
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
                }
                finally
                {
                    mutex.ReleaseExclusive();
                }
            });
            mutex.AcquireExclusive();
            mutex.ReleaseExclusive();
        }
    }
}
