using System;
using System.Collections.Generic;
using Sandbox.Game.Entities;
using Sandbox.ModAPI;
using VRage;
using VRage.Collections;
using VRage.Game.ModAPI;
using VRage.ModAPI;
using VRageMath;

namespace Equinox.Utils
{
    public static class MyUtilities
    {
        // We average integral sin(pi*x) from 0 to 1.
        public const float SunMovementMultiplier = (float)(2 / Math.PI);

        // Can draw/control stuff
        public static bool IsController => MyAPIGateway.Session.Player != null;
        // Has the final say.
        public static bool IsDecisionMaker => MyAPIGateway.Multiplayer == null || !MyAPIGateway.Multiplayer.MultiplayerActive || MyAPIGateway.Multiplayer.IsServer || MyAPIGateway.Utilities.IsDedicated;

        private static readonly MyConcurrentPool<List<IMyPlayer>> PlayerListPool = new MyConcurrentPool<List<IMyPlayer>>(2);
        public static IMyPlayer GetPlayerById(this IMyPlayerCollection collection, long id)
        {
            var list = PlayerListPool.Get();
            try
            {
                list.Clear();
                collection.GetPlayers(list, (x) => x.IdentityId == id);
                return list.Count > 0 ? list[0] : null;
            }
            finally
            {
                list.Clear();
                PlayerListPool.Return(list);
            }
        }
        public static IMyPlayer GetPlayerBySteamId(this IMyPlayerCollection collection, ulong steamId)
        {
            var list = PlayerListPool.Get();
            try
            {
                list.Clear();
                collection.GetPlayers(list, (x) => x.SteamUserId == steamId);
                return list.Count > 0 ? list[0] : null;
            }
            finally
            {
                list.Clear();
                PlayerListPool.Return(list);
            }
        }

        public static string ToStringEquinox(this SerializableVector3D v)
        {
            return $"{{X:{v.X} Y:{v.Y} Z:{v.Z}}}";
        }

        public static void AddOrApply<TK, TV>(this Dictionary<TK, TV> dict, TK key, TV val, Func<TV, TV, TV> biFunc)
        {
            TV valCurrent;
            if (!dict.TryGetValue(key, out valCurrent))
            {
                dict[key] = val;
                return;
            }
            dict[key] = biFunc.Invoke(valCurrent, val);
        }

        public static void AddValue<TK>(this Dictionary<TK, MyFixedPoint> dict, TK key, MyFixedPoint val)
        {
            dict[key] = dict.GetValueOrDefault(key, 0) + val;
        }
        public static void AddValue<TK>(this Dictionary<TK, int> dict, TK key, int val)
        {
            dict[key] = dict.GetValueOrDefault(key, 0) + val;
        }
        public static void AddValue<TK>(this Dictionary<TK, float> dict, TK key, float val)
        {
            dict[key] = dict.GetValueOrDefault(key, 0) + val;
        }
        public static void AddValue<TK>(this Dictionary<TK, double> dict, TK key, double val)
        {
            dict[key] = dict.GetValueOrDefault(key, 0) + val;
        }

        public static void AddIfNotNull<T>(this List<T> list, T value) where T : class
        {
            if (value != null)
                list.Add(value);
        }

        public static void SetNeedsWorldMatrix(this IMyEntity e, bool needs)
        {
            var old = e.NeedsWorldMatrix;
            if (needs == old) return;
            e.NeedsWorldMatrix = needs;
            if (!needs) return;
            e.Hierarchy?.Parent?.Entity?.SetNeedsWorldMatrix(true);
        }

        public static bool Equals(this MatrixI mat, MatrixI other)
        {
            return mat.Translation == other.Translation && mat.Backward == other.Backward && mat.Right == other.Right && mat.Up == other.Up;
        }

        public static Vector3D Slerp(this Vector3D start, Vector3D end, float percent)
        {
            var dot = start.Dot(end);
            // clamp [-1,1]
            dot = dot < -1 ? -1 : (dot > 1 ? 1 : dot);
            var theta = Math.Acos(dot) * percent;
            var tmp = end - start * dot;
            tmp.Normalize();
            return (start * Math.Cos(theta)) + (tmp * Math.Sin(theta));
        }

        public static Vector3D NLerp(this Vector3D start, Vector3D end, float percent)
        {
            var res = Vector3D.Lerp(start, end, percent);
            res.Normalize();
            return res;
        }

        public delegate void LoggingCallback(string format, params object[] args);

        public static LoggingCallback LogToList(List<string> dest)
        {
            return (x, y) => dest.Add(string.Format(x, y));
        }

        public static bool StartsWithICase(this string s, string arg)
        {
            return s.StartsWith(arg, true, null);
        }

        public static MatrixD AsMatrixD(this MyPositionAndOrientation posAndOrient)
        {
            var matrix = MatrixD.CreateWorld(posAndOrient.Position, posAndOrient.Forward, posAndOrient.Up);
            const float offset = 100.0f;
            if (MyAPIGatewayShortcuts.GetWorldBoundaries == null) return matrix;
            var bound = MyAPIGatewayShortcuts.GetWorldBoundaries();
            matrix.Translation = Vector3D.Min(Vector3D.Max(matrix.Translation, bound.Min + offset), bound.Max - offset);
            return matrix;
        }

        #region AABB Transforms
        public static BoundingBoxI TransformBoundingBox(BoundingBoxI box, MatrixI matrix)
        {
            Vector3I a, b;
            Vector3I.Transform(ref box.Min, ref matrix, out a);
            Vector3I.Transform(ref box.Max, ref matrix, out b);
            return new BoundingBoxI(Vector3I.Min(a, b), Vector3I.Max(a, b));
        }
        public static BoundingBoxI TransformBoundingBox(BoundingBoxI box, ref MatrixI matrix)
        {
            Vector3I a, b;
            Vector3I.Transform(ref box.Min, ref matrix, out a);
            Vector3I.Transform(ref box.Max, ref matrix, out b);
            return new BoundingBoxI(Vector3I.Min(a, b), Vector3I.Max(a, b));
        }

        public static BoundingBox TransformBoundingBox(BoundingBox box, Matrix matrix)
        {
            Vector3 a, b;
            Vector3.Transform(ref box.Min, ref matrix, out a);
            Vector3.Transform(ref box.Max, ref matrix, out b);
            return new BoundingBox(Vector3.Min(a, b), Vector3.Max(a, b));
        }
        public static BoundingBox TransformBoundingBox(BoundingBox box, ref Matrix matrix)
        {
            Vector3 a, b;
            Vector3.Transform(ref box.Min, ref matrix, out a);
            Vector3.Transform(ref box.Max, ref matrix, out b);
            return new BoundingBox(Vector3.Min(a, b), Vector3.Max(a, b));
        }

        public static BoundingBox TransformBoundingBox(BoundingBox box, MatrixI matrix)
        {
            Vector3 a, b;
            Vector3.Transform(ref box.Min, ref matrix, out a);
            Vector3.Transform(ref box.Max, ref matrix, out b);
            return new BoundingBox(Vector3.Min(a, b), Vector3.Max(a, b));
        }

        public static BoundingBoxD TransformBoundingBox(BoundingBoxD box, MatrixD matrix)
        {
            Vector3D a, b;
            Vector3D.Transform(ref box.Min, ref matrix, out a);
            Vector3D.Transform(ref box.Max, ref matrix, out b);
            return new BoundingBoxD(Vector3D.Min(a, b), Vector3D.Max(a, b));
        }

        public static BoundingBox TransformBoundingBox(BoundingBox box, ref MatrixI matrix)
        {
            Vector3 a, b;
            Vector3.Transform(ref box.Min, ref matrix, out a);
            Vector3.Transform(ref box.Max, ref matrix, out b);
            return new BoundingBox(Vector3.Min(a, b), Vector3.Max(a, b));
        }
        #endregion

        internal static MatrixI Multiply(MatrixI right, MatrixI left)
        {
            MatrixI result;
            MatrixI.Multiply(ref left, ref right, out result);
            return result;
        }

        public static void ScorePlanetFlatness(Vector3D startPos, double radius, out double score, out Vector3D groundPos)
        {
            var planet = MyGamePruningStructure.GetClosestPlanet(startPos);
            groundPos = planet.GetClosestSurfacePointGlobal(ref startPos);
            var center = planet.PositionComp.WorldVolume.Center;

            var normal = groundPos - center;
            normal.Normalize();
            // Sample two points _radius_ away perp. to normal
            var otherDir = Vector3D.CalculatePerpendicularVector(normal);
            otherDir.Normalize();
            var otherWorld = groundPos + otherDir * radius;
            var other1Surf = planet.GetClosestSurfacePointGlobal(ref otherWorld);

            Vector3D.Cross(ref normal, ref otherDir, out otherDir);
            otherDir.Normalize();
            otherWorld = groundPos + otherDir * radius;
            var other2Surf = planet.GetClosestSurfacePointGlobal(ref otherWorld);

            var surfNorm = Vector3D.Cross(other2Surf - groundPos, other1Surf - groundPos);
            surfNorm.Normalize();
            score = Math.Abs(Vector3D.Dot(surfNorm, normal));
        }

        public static ulong Hash64(this string str)
        {
            ulong hash = 5381;
            // djb2 (http://www.cse.yorku.ca/~oz/hash.html)
            // ReSharper disable once LoopCanBeConvertedToQuery
            foreach (var t in str)
                hash = ((hash << 5) + hash) + t;
            return hash;
        }

        public static Color NextColor => colors[colorID = (colorID + 1) % colors.Length];

        private static int colorID = 0;
        private static readonly Color[] colors = new[]
        {
            Color.MediumSeaGreen, Color.Lavender, Color.DarkViolet, Color.Salmon, Color.SlateBlue, Color.AntiqueWhite, Color.DarkGoldenrod, Color.DarkKhaki, Color.MediumBlue, Color.Magenta, Color.PapayaWhip, Color.Orange, Color.SandyBrown,
            Color.Pink, Color.LightCyan, Color.LightGray, Color.LemonChiffon, Color.LightSkyBlue, Color.Snow, Color.Gold, Color.OldLace, Color.PeachPuff, Color.Brown, Color.Linen, Color.Tomato, Color.MidnightBlue, Color.LightSalmon, Color.LimeGreen,
            Color.PowderBlue, Color.DarkSlateBlue, Color.LightYellow, Color.MediumVioletRed, Color.DarkOliveGreen, Color.Goldenrod, Color.Indigo, Color.DarkCyan, Color.LavenderBlush, Color.Cornsilk, Color.Ivory, Color.Coral, Color.DarkSeaGreen,
            Color.MediumSpringGreen, Color.Azure, Color.Transparent, Color.Orchid, Color.Chartreuse, Color.FloralWhite, Color.Gainsboro, Color.RoyalBlue, Color.CadetBlue, Color.DarkSalmon, Color.DarkMagenta, Color.Beige, Color.Bisque, Color.Plum,
            Color.OrangeRed, Color.Olive, Color.Firebrick, Color.SkyBlue, Color.IndianRed, Color.Fuchsia, Color.CornflowerBlue, Color.DarkOrange, Color.BurlyWood, Color.Moccasin, Color.PaleTurquoise, Color.DeepPink, Color.Yellow, Color.SaddleBrown,
            Color.Tan, Color.MediumSlateBlue, Color.Teal, Color.YellowGreen, Color.Peru, Color.MintCream, Color.Blue, Color.DarkRed, Color.ForestGreen, Color.RosyBrown, Color.SteelBlue, Color.White, Color.DarkOrchid, Color.Gray, Color.Violet,
            Color.Maroon, Color.WhiteSmoke, Color.BlueViolet, Color.DarkSlateGray, Color.MistyRose, Color.SeaGreen, Color.DodgerBlue, Color.OliveDrab, Color.BlanchedAlmond, Color.DarkBlue, Color.HotPink, Color.DarkTurquoise, Color.PaleGreen,
            Color.Khaki, Color.Lime, Color.Honeydew, Color.Aqua, Color.Aquamarine, Color.DimGray, Color.Navy, Color.PaleGoldenrod, Color.Cyan, Color.Purple, Color.LightSeaGreen, Color.GreenYellow, Color.AliceBlue, Color.LightBlue, Color.Red,
            Color.LightPink, Color.Crimson, Color.SpringGreen, Color.Black, Color.Thistle, Color.PaleVioletRed, Color.MediumTurquoise, Color.MediumPurple, Color.Sienna, Color.Chocolate, Color.DeepSkyBlue, Color.SlateGray, Color.LawnGreen,
            Color.SeaShell, Color.MediumOrchid, Color.Wheat, Color.DarkGreen, Color.LightSlateGray, Color.MediumAquamarine, Color.LightSteelBlue, Color.DarkGray, Color.Turquoise, Color.NavajoWhite, Color.LightCoral, Color.LightGoldenrodYellow,
            Color.GhostWhite, Color.LightGreen, Color.Green, Color.Silver
        };
    }
}
