﻿using System;
using System.Collections.Generic;
using Equinox.Utils.Noise.Keen;
using VRage;
using VRage.Library.Utils;
using VRageMath;

namespace Equinox.Utils.Noise
{
    public class OctreeNoise
    {
        private readonly IMyModule m_densityNoise;
        private readonly IMyModule m_probabilityModule;
        private readonly IMyModule[] m_placementNoise;
        private readonly IMyModule[] m_warpNoise;
        private readonly int m_depth;
        private readonly double m_cubeSideMax;

        public OctreeNoise(long seed, double cubeSideMax, double cubeSideMin, IMyModule densityNoise)
        {
            // cubeSideMin * (2^depth) == cubeSideMax
            m_depth = (int)Math.Ceiling(Math.Log(cubeSideMax / cubeSideMin) / Math.Log(2)) + 1;
            var seedLow = (int)(seed >> 0);
            var seedHigh = (int)(seed >> 32);
            m_densityNoise = densityNoise ?? new MyCompositeNoise(m_depth, (float)(1 / cubeSideMax), seedLow);
            var rng = new MyRandom(seedHigh);
            m_placementNoise = new IMyModule[3];
            m_warpNoise = new IMyModule[3];
            for (var i = 0; i < 3; i++)
            {
                m_placementNoise[i] = new MySimplex(rng.Next(), (float)(10 / cubeSideMin));
                m_warpNoise[i] = new MySimplex(rng.Next(), (float)(1 / (cubeSideMax * 4)));
            }
            m_probabilityModule = new MySimplex(rng.Next(), 1);
            m_cubeSideMax = cubeSideMax;
        }

        public Vector4I GetOctreeNodeAt(Vector3D pos)
        {
            var rootCell = new Vector4I(Vector3I.Floor(pos / m_cubeSideMax), 0);

            var cell = rootCell;
            while (true)
            {
                var aabb = GetNodeAABB(cell);
                var densityNoise = m_densityNoise.GetValue(aabb.Center) * m_depth;
                var depthDensity = (int)MyMath.Clamp((float)Math.Floor(densityNoise), 0, m_depth - 1);
                if (depthDensity <= cell.W)
                    return cell;
                else
                {
                    var nx = (cell.X << 1) + (aabb.Center.X > pos.X ? 1 : 0);
                    var ny = (cell.Y << 1) + (aabb.Center.Y > pos.Y ? 1 : 0);
                    var nz = (cell.Z << 1) + (aabb.Center.Z > pos.Z ? 1 : 0);
                    cell = new Vector4I(nx, ny, nz, cell.W + 1);
                }
            }
        }

        public IEnumerable<MyTuple<Vector4I, Vector4D>> TryGetSpawnIn(BoundingBoxD aabb, Func<BoundingBoxD, bool> test = null)
        {
            var rootCellsMin = Vector3I.Floor(aabb.Min / m_cubeSideMax);
            var rootCellsMax = Vector3I.Floor(aabb.Max / m_cubeSideMax);

            for (var rootCells = new Vector3I_RangeIterator(ref rootCellsMin, ref rootCellsMax); rootCells.IsValid(); rootCells.MoveNext())
                foreach (var k in GetSpawnsIn(new Vector4I(rootCells.Current, 0), aabb, test))
                    yield return k;
        }

        private static float SampleNoiseNorm(IMyModule src, Vector3D pos)
        {
            var nn = (float)src.GetValue(pos);
            return MyMath.Clamp(nn * Math.Abs(nn), -1, 1);
        }

        public BoundingBoxD GetNodeAABB(Vector4I cell)
        {
            var cellPos = new Vector3I(cell.X, cell.Y, cell.Z);
            var cellSize = m_cubeSideMax / (1 << cell.W);
            return new BoundingBoxD(cellPos * cellSize, (cellPos + 1) * cellSize);
        }

        private IEnumerable<MyTuple<Vector4I, Vector4D>> GetSpawnsIn(Vector4I cell, BoundingBoxD aabbGlobal, Func<BoundingBoxD, bool> test)
        {
            var aabb = GetNodeAABB(cell);
            if (!aabbGlobal.Intersects(aabb))
                yield break;
            if (test != null && !test.Invoke(aabb))
                yield break;

            var densityNoise = m_densityNoise.GetValue(aabb.Center) * m_depth;
            var depthDensity = (int)MyMath.Clamp((float)Math.Floor(densityNoise), 0, m_depth - 1);
            if (depthDensity <= cell.W)
            {
                var localDensity = densityNoise - depthDensity;
                // When local density is zero we just came from a subdivision which means there is a (1/8) chance.  When it is one we want 100% chance.
                var probability = (1 + localDensity * 7) / 8.0;
                var placementRoll = MyMath.Clamp((float)(m_probabilityModule.GetValue(aabb.Center) + 1) / 2, 0, 1);
                if (placementRoll <= probability)
                    yield break;

                // Try to spawn in this cell.
                var placement = new Vector3(SampleNoiseNorm(m_placementNoise[0], aabb.Center),
                                            SampleNoiseNorm(m_placementNoise[1], aabb.Center),
                                            SampleNoiseNorm(m_placementNoise[2], aabb.Center));

                var warp = new Vector3(SampleNoiseNorm(m_warpNoise[0], aabb.Center),
                    SampleNoiseNorm(m_warpNoise[1], aabb.Center),
                    SampleNoiseNorm(m_warpNoise[2], aabb.Center));

                var localPos = Vector3.Clamp((warp * 0.25f) + (placement * 0.75f), Vector3.MinusOne, Vector3.One);

                var worldPos = aabb.Center + aabb.HalfExtents * localPos;
                yield return MyTuple.Create(cell, new Vector4D(worldPos, densityNoise / m_depth));
            }
            else
            {
                // Subdivide.
                for (var i = 0; i < 8; i++)
                {
                    var x = (cell.X << 1) | (i & 1);
                    var y = (cell.Y << 1) | ((i >> 1) & 1);
                    var z = (cell.Z << 1) | ((i >> 2) & 1);
                    foreach (var k in GetSpawnsIn(new Vector4I(x, y, z, cell.W + 1), aabbGlobal, test))
                        yield return k;
                }
            }
        }
    }
}
