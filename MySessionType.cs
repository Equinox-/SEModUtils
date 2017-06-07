using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.ModAPI;

namespace Equinox.Utils
{
    [Flags]
    public enum MySessionType
    {
        PlayerController = 1,
        ServerDecider = 2
    }

    public static class MySessionTypeExt
    {
        public static bool HasFlag(this MySessionType type, int ot)
        {
            return ((int)type & ot) != 0;
        }

        public static bool HasFlag(this MySessionType type, MySessionType ot)
        {
            return ((int)type & (int)ot) != 0;
        }

        public static bool IsController(this IMySession session)
        {
            return session.Player != null;
        }

        public static bool IsDecider(this IMySession session)
        {
            return MyAPIGateway.Utilities.IsDedicated || (MyAPIGateway.Multiplayer != null && MyAPIGateway.Multiplayer.IsServer) || 
                session.SessionSettings.OnlineMode == MyOnlineModeEnum.OFFLINE;
        }

        public static MySessionType SessionType(this IMySession session)
        {
            var flags = 0;
            if (session.IsController())
                flags |= (int) MySessionType.PlayerController;
            if (session.IsDecider())
                flags |= (int) MySessionType.ServerDecider;
            return (MySessionType) flags;
        }

        public static bool Flagged(this MySessionType a, MySessionType b)
        {
            return ((int) a & (int) b) != 0;
        }

        public static bool Flagged(this MySessionType a, int b)
        {
            return ((int)a & (int)b) != 0;
        }
    }
}
