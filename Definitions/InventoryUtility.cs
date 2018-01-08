using System.Collections.Generic;
using Sandbox.Definitions;
using Sandbox.Game.Entities;
using Sandbox.Game.EntityComponents;
using VRage.Game;

namespace Equinox.Utils.Definitions
{
    public static class InventoryUtility
    {
        public static double GetInventoryVolume(MyDefinitionId id)
        {
            double result;
            if (CacheInvVolume.TryGetValue(id, out result)) return result;
            return CacheInvVolume[id] = GetInventoryVolumeInternal(id);
        }

        private static readonly Dictionary<MyDefinitionId, double> CacheInvVolume = new Dictionary<MyDefinitionId, double>(128, MyDefinitionId.Comparer);

        private static double GetInventoryVolumeInternal(MyDefinitionId id)
        {
            MyContainerDefinition container;
            if (MyComponentContainerExtension.TryGetContainerDefinition(id.TypeId, id.SubtypeId, out container) && container.DefaultComponents != null)
                foreach (var component in container.DefaultComponents)
                {
                    MyComponentDefinitionBase componentDefinition = null;
                    if (!MyComponentContainerExtension.TryGetComponentDefinition(component.BuilderType,
                        component.SubtypeId ?? id.SubtypeId, out componentDefinition)) continue;
                    var invDef = componentDefinition as MyInventoryComponentDefinition;
                    if (invDef != null)
                        return invDef.Volume * 1000;
                }

            var def = MyDefinitionManager.Static.GetCubeBlockDefinition(id);
            if (def is MyCargoContainerDefinition)
                return (def as MyCargoContainerDefinition).InventorySize.Volume * 1000;
            return 0.0;
        }
    }
}
