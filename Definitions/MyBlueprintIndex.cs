using System;
using System.Collections.Generic;
using System.Linq;
using Sandbox.Definitions;
using VRage;
using VRage.Game;

namespace Equinox.Utils
{
    public class MyBlueprintIndex
    {
        private static MyBlueprintIndex m_instance;

        public static MyBlueprintIndex Instance
        {
            get
            {
                if (m_instance != null) return m_instance;
                m_instance = new MyBlueprintIndex();
                m_instance.Load();
                return m_instance;
            }
        }

        public enum BlueprintType
        {
            OriginalProducer = 0,
            AllProducers,
            TopLevelProducer,
            Consumer,
            BlueprintTypeCount
        }

        private readonly Dictionary<MyDefinitionId, List<MyIndexedBlueprint>>[] m_index = new Dictionary<MyDefinitionId, List<MyIndexedBlueprint>>[(int)BlueprintType.BlueprintTypeCount];

        public MyBlueprintIndex()
        {
            for (var i = 0; i < m_index.Length; i++)
                m_index[i] = new Dictionary<MyDefinitionId, List<MyIndexedBlueprint>>(MyDefinitionId.Comparer);
        }

        public class MyIndexedBlueprint
        {
            public readonly MyDefinitionId Result;
            public readonly Dictionary<MyDefinitionId, MyFixedPoint> Ingredients;
            // Requires (double) iterations of (Definition) to produce/consume this.
            public readonly Dictionary<MyBlueprintDefinitionBase, double> Blueprints = new Dictionary<MyBlueprintDefinitionBase, double>();

            public readonly bool ConsumptionRecipe;

            internal MyIndexedBlueprint(MyBlueprintDefinitionBase def, MyDefinitionId result, MyFixedPoint divider, bool reverse)
            {
                Result = result;
                Ingredients = new Dictionary<MyDefinitionId, MyFixedPoint>(MyDefinitionId.Comparer);
                ConsumptionRecipe = reverse;
                if (!reverse)
                    foreach (var req in def.Prerequisites)
                        Ingredients[req.Id] = (MyFixedPoint)((double)req.Amount / (double)divider);
                else
                    foreach (var req in def.Results)
                        Ingredients[req.Id] = (MyFixedPoint)((double)req.Amount / (double)divider);
            }

            internal MyIndexedBlueprint(MyDefinitionId result, Dictionary<MyDefinitionId, MyFixedPoint> recipe)
            {
                Result = result;
                Ingredients = recipe;
            }
        }

        private readonly Dictionary<MyDefinitionId, double> m_rawResourcesFor = new Dictionary<MyDefinitionId, double>(MyDefinitionId.Comparer);

        /// <summary>
        /// Gets the amount of raw resources (ores) required to produce the given blueprint.
        /// </summary>
        /// <param name="id">definition ID</param>
        /// <returns></returns>
        public double GetRawResourcesFor(MyDefinitionId id)
        {
            double required;
            if (m_rawResourcesFor.TryGetValue(id, out required))
                return required;

            required = 0;
            m_rawResourcesFor[id] = required;
            MyCubeBlockDefinition cubeDef = MyDefinitionManager.Static.GetCubeBlockDefinition(id);
            if (cubeDef != null)
            {
                foreach (var kv in cubeDef.Components)
                    required += kv.Count * GetRawResourcesFor(kv.Definition.Id);
                m_rawResourcesFor[id] = required;
                return required;
            }
            MyIndexedBlueprint index = GetTopLevelProducing(id);
            if (index != null)
            {
                foreach (var kv in index.Ingredients)
                    if (kv.Key.TypeId == typeof(MyObjectBuilder_Ore) || kv.Key.SubtypeName.IndexOf("ore", StringComparison.OrdinalIgnoreCase) >= 0)
                        required += (double) kv.Value;
                m_rawResourcesFor[id] = required;
                return required;
            }
            return 0;
        }


        private List<MyIndexedBlueprint> GetSafe(MyDefinitionId id, BlueprintType type)
        {
            var itype = (int)type;
            List<MyIndexedBlueprint> vals;
            if (!m_index[itype].TryGetValue(id, out vals))
                m_index[itype][id] = vals = new List<MyIndexedBlueprint>();
            return vals;
        }

        public MyIndexedBlueprint GetTopLevelProducing(MyDefinitionId key)
        {
            List<MyIndexedBlueprint> res;
            return m_index[(int)BlueprintType.TopLevelProducer].TryGetValue(key, out res) && res != null && res.Count > 0 ? res[0] : null;
        }

        public IEnumerable<MyIndexedBlueprint> GetAllConsuming(MyDefinitionId result)
        {
            List<MyIndexedBlueprint> res;
            return m_index[(int)BlueprintType.Consumer].TryGetValue(result, out res) ? res : Enumerable.Empty<MyIndexedBlueprint>();
        }

        public IEnumerable<MyIndexedBlueprint> GetAllProducing(MyDefinitionId result, bool original)
        {
            List<MyIndexedBlueprint> res;
            return m_index[original ? (int)BlueprintType.OriginalProducer : (int)BlueprintType.AllProducers].TryGetValue(result, out res) ? res : Enumerable.Empty<MyIndexedBlueprint>();
        }

        private void Load()
        {
            foreach (var t in m_index)
                t.Clear();

            var needsElaboration = new Queue<MyIndexedBlueprint>();
            // Load base blueprints.
            foreach (var blueprint in MyDefinitionManager.Static.GetBlueprintDefinitions())
            {
                if (blueprint is MyCompositeBlueprintDefinition) continue;
                foreach (var result in blueprint.Results)
                    if (result.Amount > 0)
                    {
                        var bp = new MyIndexedBlueprint(blueprint, result.Id, result.Amount, false);
                        bp.Blueprints[blueprint] = 1.0 / (double)result.Amount;
                        needsElaboration.Enqueue(bp);
                        GetSafe(result.Id, BlueprintType.OriginalProducer).Add(bp);
                        GetSafe(result.Id, BlueprintType.AllProducers).Add(bp);
                    }
                foreach (var src in blueprint.Prerequisites)
                    if (src.Amount > 0)
                    {
                        var bp = new MyIndexedBlueprint(blueprint, src.Id, src.Amount, true);
                        bp.Blueprints[blueprint] = 1.0 / (double)src.Amount;
                        GetSafe(src.Id, BlueprintType.Consumer).Add(bp);
                    }
            }

            while (needsElaboration.Count > 0)
            {
                var bp = needsElaboration.Dequeue();
                var elaborate = bp.Ingredients.Keys.Any(m_index[(int)BlueprintType.AllProducers].ContainsKey);
                if (!elaborate)
                {
                    GetSafe(bp.Result, BlueprintType.TopLevelProducer).Add(bp);
                    continue;
                }
                var recipe = new Dictionary<MyDefinitionId, MyFixedPoint>(MyDefinitionId.Comparer);
                var bps = new Dictionary<MyBlueprintDefinitionBase, double>(bp.Blueprints);
                foreach (var kv in bp.Ingredients)
                {
                    List<MyIndexedBlueprint> bpChild;
                    if (!m_index[(int)BlueprintType.AllProducers].TryGetValue(kv.Key, out bpChild))
                    {
                        recipe.AddValue(kv.Key, kv.Value);
                        continue;
                    }
                    if (bpChild == null || bpChild.Count <= 0) continue;
                    // Something better than the last one?
                    var bpSrc = bpChild[bpChild.Count - 1];
                    foreach (var x in bpSrc.Blueprints)
                        bps.AddValue(x.Key, x.Value * (double)kv.Value);
                    foreach (var kvC in bpSrc.Ingredients)
                    {
                        recipe.AddValue(kvC.Key, kvC.Value * kv.Value);
                    }
                }
                var bpOut = new MyIndexedBlueprint(bp.Result, recipe);
                foreach (var kv in bps)
                    bpOut.Blueprints[kv.Key] = kv.Value;
                needsElaboration.Enqueue(bpOut);
                GetSafe(bp.Result, BlueprintType.AllProducers).Add(bpOut);
            }
        }
    }
}
