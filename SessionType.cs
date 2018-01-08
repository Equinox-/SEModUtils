using System;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.ModAPI;

namespace Equinox.Utils
{
    [Flags]
    public enum SessionType
    {
        PlayerController = 1,
        ServerDecider = 2
    }

    public static class SessionTypeExt
    {
        public static bool HasFlag(this SessionType type, int ot)
        {
            return ((int)type & ot) != 0;
        }

        public static bool HasFlag(this SessionType type, SessionType ot)
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
                session.OnlineMode == MyOnlineModeEnum.OFFLINE;
        }

        public static SessionType SessionType(this IMySession session)
        {
            var flags = 0;
            if (session.IsController())
                flags |= (int)Utils.SessionType.PlayerController;
            if (session.IsDecider())
                flags |= (int)Utils.SessionType.ServerDecider;
            return (SessionType) flags;
        }

        public static bool Flagged(this SessionType a, SessionType b)
        {
            return ((int) a & (int) b) != 0;
        }

        public static bool Flagged(this SessionType a, int b)
        {
            return ((int)a & (int)b) != 0;
        }
    }
}
