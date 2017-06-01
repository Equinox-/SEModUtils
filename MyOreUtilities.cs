using System;
using System.Collections.Generic;
using System.Linq;
using Equinox.Utils;
using Sandbox.Definitions;
using VRage.Game;

namespace Equinox.ProceduralWorld.Utils
{
    public static class MyOreUtilities
    {
        private class MyRarityInfo
        {
            public MyDefinitionId Id;
            public double OutputRatio;
            public double Rarity;
        }

        private static List<MyRarityInfo> m_rarityList = null;
        private static Dictionary<MyDefinitionId, MyRarityInfo> m_oreRarity = null;

        private static readonly Dictionary<MyDefinitionId, double> m_avgOutputRatio = new Dictionary<MyDefinitionId, double>();
        public static double GetOutputRatio(MyDefinitionId oreOrIngot)
        {
            double result;
            if (m_avgOutputRatio.TryGetValue(oreOrIngot, out result))
                return result;
            if (oreOrIngot.TypeId == typeof(MyObjectBuilder_Ore))
                return m_avgOutputRatio[oreOrIngot] = MyBlueprintIndex.Instance.GetAllConsuming(oreOrIngot).Select(x => x.Ingredients.Values.Sum(y => (double)y)).DefaultIfEmpty(1).Average();
            if (oreOrIngot.TypeId == typeof(MyObjectBuilder_Ingot))
                return m_avgOutputRatio[oreOrIngot] = MyBlueprintIndex.Instance.GetAllProducing(oreOrIngot).Select(x => x.Ingredients.Values.Sum(y => (double) y)).DefaultIfEmpty(1).Average();
            throw new ArgumentException("Can only evaluate the output ratio of ores and ingots");
        }

        /// <summary>
        /// Caching evaluation of the "rarity" of an ore based on the blueprint consumers of it.
        /// High rarity ores have low output amounts.
        /// </summary>
        /// <param name="ore">Ore ID to lookup</param>
        /// <returns>Rarity [0, 1]</returns>
        public static double GetRarity(MyDefinitionId ore)
        {
            if (ore.TypeId != typeof(MyObjectBuilder_Ore)) throw new ArgumentException("Can only evaluate rarity on ores");
            if (m_oreRarity == null)
            {
                var lst = MyDefinitionManager.Static.GetPhysicalItemDefinitions();
                var temp = new List<MyRarityInfo>();
                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var pobj in lst)
                {
                    if (pobj.Id.TypeId != typeof(MyObjectBuilder_Ore)) continue;
                    var avgOutputRatio = GetOutputRatio(pobj.Id);
                    temp.Add(new MyRarityInfo() { Id = pobj.Id, OutputRatio = avgOutputRatio });
                }
                // Sort in descending.
                temp.Sort((a, b) => Math.Sign(b.OutputRatio - a.OutputRatio));
                var dout = new Dictionary<MyDefinitionId, MyRarityInfo>();
                for (var i = 0; i < temp.Count; i++)
                {
                    temp[i].Rarity = i / (float)(temp.Count - 1);
                    dout[temp[i].Id] = temp[i];
                }
                m_rarityList = temp;
                m_oreRarity = dout;
            }
            MyRarityInfo info;
            if (m_oreRarity.TryGetValue(ore, out info))
                return info.Rarity;
            // Guess rarity using other info.
            var outRatio = GetOutputRatio(ore);
            var left = 0;
            var right = m_rarityList.Count - 1;
            // bsearch m_rarityList for this ratio.
            while (left != right)
            {
                var center = (left + right) / 2;
                var value = m_rarityList[center];
                if (value.OutputRatio > outRatio)
                    right = center;
                else
                    left = center + 1;
            }
            var entry = m_rarityList[left];
            var rarity = entry.Rarity;
            if (left + 1 < m_rarityList.Count)
            {
                var next = m_rarityList[left + 1];
                var dOut = next.OutputRatio - entry.OutputRatio;
                if (Math.Abs(dOut) > 0.01)
                {
                    var dRarity = next.Rarity - entry.Rarity;
                    rarity += (dRarity / dOut) * (outRatio - entry.OutputRatio);
                }
            }
            var result = m_oreRarity[ore] = new MyRarityInfo() { Id = ore, OutputRatio = outRatio, Rarity = rarity };
            return result.Rarity;
        }
    }
}
