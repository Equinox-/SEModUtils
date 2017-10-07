using System;
using System.Diagnostics;
using Equinox.Utils.Logging;
using Sandbox.ModAPI;
using VRage;

namespace Equinox.ProceduralWorld.Utils
{
    public class MyParallelUtilities
    {
        public static Action WrapAction(Action action, IMyLogging logger = null)
        {
            if (!Settings.ParallelCatchErrors)
                return action;
            string initTrace = "";
            if (Settings.ParallelTracing)
                try
                {
                    throw new Exception();
                }
                catch (Exception e)
                {
                    initTrace = e.StackTrace;
                }
            return () =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    logger?.Error("Invoke error: {0}\nScheduled from: {1}", e, initTrace);
                }
            };
        }

        public static void InvokeOnGameThreadBlocking(Action action, IMyLogging logger = null)
        {
            var mutex = new FastResourceLock();
            mutex.AcquireExclusive();
            MyAPIGateway.Utilities.InvokeOnGameThread(WrapAction(() =>
            {
                try
                {
                    action();
                }
                finally
                {
                    mutex.ReleaseExclusive();
                }
            }, logger));
            mutex.AcquireExclusive();
            mutex.ReleaseExclusive();
        }
    }
}
