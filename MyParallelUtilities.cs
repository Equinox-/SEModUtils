using System;
using Sandbox.ModAPI;
using VRage;

namespace Equinox.ProceduralWorld.Utils
{
    public class MyParallelUtilities
    {
        public static void InvokeOnGameThreadBlocking(Action action)
        {
            var mutex = new FastResourceLock();
            mutex.AcquireExclusive();
            MyAPIGateway.Utilities.InvokeOnGameThread(() =>
            {
                try
                {
                    action();
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
