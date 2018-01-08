using Equinox.Utils.Cache;
using Sandbox.Definitions;
using Sandbox.Game;

namespace Equinox.Utils.Definitions
{
    public struct PowerSourceSink
    {
        public string ResourceGroup;
        /// <summary>
        /// Positive numbers are consumption, negative are generation.
        /// </summary>
        public float Consumption;

        public PowerSourceSink(string group, float consumption)
        {
            ResourceGroup = group;
            Consumption = consumption;
        }
    }
    
    public class PowerUtilities
    {
        // ReSharper disable CanBeReplacedWithTryCastAndCheckForNull
        private static PowerSourceSink MaxPowerConsumptionInternal(MyCubeBlockDefinition def)
        {
            // refinery, blast furnace, assembler, oxygen tanks, hydrogen tanks, oxygen generator
            if (def is MyProductionBlockDefinition)
            {
                var v = (MyProductionBlockDefinition)def;
                return new PowerSourceSink(v.ResourceSinkGroup.String, v.OperationalPowerConsumption);
            }
            // oxygen farm
            if (def is MyOxygenFarmDefinition)
            {
                var v = (MyOxygenFarmDefinition)def;
                return new PowerSourceSink(v.ResourceSinkGroup.String, v.OperationalPowerConsumption);
            }
            // thrusters
            if (def is MyThrustDefinition)
            {
                var v = (MyThrustDefinition)def;
                return new PowerSourceSink(v.ResourceSinkGroup.String, v.FuelConverter == null ? v.MaxPowerConsumption : 0);
            }
            // gyros
            if (def is MyGyroDefinition)
            {
                var v = (MyGyroDefinition)def;
                return new PowerSourceSink(v.ResourceSinkGroup, v.RequiredPowerInput);
            }
            // reactors
            if (def is MyReactorDefinition)
            {
                var v = (MyReactorDefinition)def;
                return new PowerSourceSink(v.ResourceSourceGroup.String, -v.MaxPowerOutput);
            }
            // solar panels
            if (def is MySolarPanelDefinition)
            {
                var v = (MySolarPanelDefinition)def;
                return new PowerSourceSink(v.ResourceSourceGroup.String, -v.MaxPowerOutput * Utilities.SunMovementMultiplier * (v.IsTwoSided ? 1 : 0.5f));
            }
            // lights
            if (def is MyLightingBlockDefinition)
            {
                var v = (MyLightingBlockDefinition)def;
                return new PowerSourceSink(v.ResourceSinkGroup.String, v.RequiredPowerInput);
            }
            if (def is MyAirVentDefinition)
            {
                var v = (MyAirVentDefinition)def;
                return new PowerSourceSink(v.ResourceSinkGroup.String, v.OperationalPowerConsumption);
            }
            if (def is MyLaserAntennaDefinition)
            {
                var v = (MyLaserAntennaDefinition)def;
                return new PowerSourceSink(v.ResourceSinkGroup.String, v.PowerInputLasing);
            }
            if (def is MyRadioAntennaDefinition)
            {
                var v = (MyRadioAntennaDefinition)def;
                return new PowerSourceSink(v.ResourceSinkGroup.String, MyEnergyConstants.MAX_REQUIRED_POWER_ANTENNA);
            }
            if (def is MyLargeTurretBaseDefinition)
            {
                var v = (MyLargeTurretBaseDefinition)def;
                return new PowerSourceSink(v.ResourceSinkGroup.String, MyEnergyConstants.MAX_REQUIRED_POWER_TURRET);
            }
            if (def is MyOreDetectorDefinition)
            {
                var v = (MyOreDetectorDefinition)def;
                return new PowerSourceSink(v.ResourceSinkGroup.String, MyEnergyConstants.MAX_REQUIRED_POWER_ORE_DETECTOR);
            }
            if (def is MyBeaconDefinition)
            {
                var v = (MyBeaconDefinition)def;
                return new PowerSourceSink(v.ResourceSinkGroup, MyEnergyConstants.MAX_REQUIRED_POWER_BEACON);
            }
            if (def is MyDoorDefinition)
            {
                var v = (MyDoorDefinition)def;
                return new PowerSourceSink(v.ResourceSinkGroup, MyEnergyConstants.MAX_REQUIRED_POWER_DOOR);
            }
            if (def is MyMedicalRoomDefinition)
            {
                var v = (MyMedicalRoomDefinition)def;
                return new PowerSourceSink(v.ResourceSinkGroup, MyEnergyConstants.MAX_REQUIRED_POWER_MEDICAL_ROOM);
            }
            if (def is MySoundBlockDefinition)
            {
                var v = (MySoundBlockDefinition)def;
                return new PowerSourceSink(v.ResourceSinkGroup.String, MyEnergyConstants.MAX_REQUIRED_POWER_SOUNDBLOCK);
            }
            return new PowerSourceSink("null", 0.0f);
            // internal MyCryoChamberDefinition
            // ignore MyShipDrillDefinition
            // ignore MyShipGrinderDefinition
            // ignore MyShipWelderDefinition
        }

        private static readonly LruCache<MyCubeBlockDefinition, PowerSourceSink> maxPowerCache = new LruCache<MyCubeBlockDefinition, PowerSourceSink>(512, null);
        public static PowerSourceSink MaxPowerConsumption(MyCubeBlockDefinition def)
        {
            return maxPowerCache.GetOrCreate(def, MaxPowerConsumptionInternal);
        }
    }
}
