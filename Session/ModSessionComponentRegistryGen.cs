 
 

using System.Xml.Serialization;
using System.Collections.Generic;
using Equinox.ProceduralWorld.Names;

namespace Equinox.Utils.Session
{
    public class ModSessionComponentRegistryGen
    {
        public static void Register() {
            ModSessionComponentRegistry.Register<Equinox.Utils.Logging.VRageLogger, Equinox.Utils.Logging.Ob_VRageLogger>();
            ModSessionComponentRegistry.Register<Equinox.Utils.Logging.CustomLogger, Equinox.Utils.Logging.Ob_CustomLogger>();
            ModSessionComponentRegistry.Register<Equinox.Utils.Network.NetworkComponent, Equinox.Utils.Network.Ob_Network>();
            ModSessionComponentRegistry.Register<Equinox.Utils.Network.RPCComponent, Equinox.Utils.Network.Ob_RPC>();
            ModSessionComponentRegistry.Register<Equinox.Utils.Network.SyncComponent, Equinox.Utils.Network.Ob_Sync>();
            ModSessionComponentRegistry.Register<Equinox.Utils.Session.SessionBootstrapper, Equinox.Utils.Session.Ob_SessionBootstrapper>();
            ModSessionComponentRegistry.Register<Equinox.Utils.Command.CommandDispatchComponent, Equinox.Utils.Command.Ob_CommandDispatch>();
            ModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Names.CompositeNameGenerator, Equinox.ProceduralWorld.Names.Ob_CompositeNameGenerator>();
            ModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Buildings.BuildingControlCommands, Equinox.ProceduralWorld.Buildings.Ob_BuildingControlCommands>();
            ModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Buildings.BuildingDatabase, Equinox.ProceduralWorld.Buildings.Ob_BuildingDatabase>();
            ModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Manager.ProceduralWorldManager, Equinox.ProceduralWorld.Manager.Ob_ProceduralWorldManager>();
            ModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Voxels.Planets.InfinitePlanetsModule, Equinox.ProceduralWorld.Voxels.Planets.Ob_InfinitePlanets>();
            ModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Voxels.Asteroids.AsteroidFieldModule, Equinox.ProceduralWorld.Voxels.Asteroids.Ob_AsteroidField>();
            ModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Voxels.Asteroids.AutomaticAsteroidFieldsComponent, Equinox.ProceduralWorld.Voxels.Asteroids.Ob_AutomaticAsteroidFields>();
            ModSessionComponentRegistry.Register<ExoticNameGenerator, Ob_ExoticNameGenerator>();
            ModSessionComponentRegistry.Register<StatisticalNameGenerator, Ob_StatisticalNameGenerator>();
            ModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Buildings.Generation.StationGeneratorManager, Equinox.ProceduralWorld.Buildings.Generation.Ob_StationGeneratorManager>();
            ModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Buildings.Exporter.DesignTools, Equinox.ProceduralWorld.Buildings.Exporter.Ob_DesignTools>();
            ModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Buildings.Game.ProceduralStationModule, Equinox.ProceduralWorld.Buildings.Game.Ob_ProceduralStation>();
            ModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Buildings.Seeds.ProceduralFactions, Equinox.ProceduralWorld.Buildings.Seeds.Ob_ProceduralFactions>();
            ModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Buildings.Library.PartManager, Equinox.ProceduralWorld.Buildings.Library.Ob_PartManager>();
        }
    }

    public partial class Ob_SessionManager
    {
        [XmlElement("VRageLogger", typeof(Equinox.Utils.Logging.Ob_VRageLogger))]
        [XmlElement("CustomLogger", typeof(Equinox.Utils.Logging.Ob_CustomLogger))]
        [XmlElement("Network", typeof(Equinox.Utils.Network.Ob_Network))]
        [XmlElement("RPC", typeof(Equinox.Utils.Network.Ob_RPC))]
        [XmlElement("Sync", typeof(Equinox.Utils.Network.Ob_Sync))]
        [XmlElement("SessionBootstrapper", typeof(Equinox.Utils.Session.Ob_SessionBootstrapper))]
        [XmlElement("CommandDispatch", typeof(Equinox.Utils.Command.Ob_CommandDispatch))]
        [XmlElement("CompositeNameGenerator", typeof(Equinox.ProceduralWorld.Names.Ob_CompositeNameGenerator))]
        [XmlElement("BuildingControlCommands", typeof(Equinox.ProceduralWorld.Buildings.Ob_BuildingControlCommands))]
        [XmlElement("BuildingDatabase", typeof(Equinox.ProceduralWorld.Buildings.Ob_BuildingDatabase))]
        [XmlElement("ProceduralWorldManager", typeof(Equinox.ProceduralWorld.Manager.Ob_ProceduralWorldManager))]
        [XmlElement("InfinitePlanets", typeof(Equinox.ProceduralWorld.Voxels.Planets.Ob_InfinitePlanets))]
        [XmlElement("AsteroidField", typeof(Equinox.ProceduralWorld.Voxels.Asteroids.Ob_AsteroidField))]
        [XmlElement("AutomaticAsteroidFields", typeof(Equinox.ProceduralWorld.Voxels.Asteroids.Ob_AutomaticAsteroidFields))]
        [XmlElement("MyExoticNameGenerator", typeof(Ob_ExoticNameGenerator))]
        [XmlElement("MyStatisticalNameGenerator", typeof(Ob_StatisticalNameGenerator))]
        [XmlElement("StationGeneratorManager", typeof(Equinox.ProceduralWorld.Buildings.Generation.Ob_StationGeneratorManager))]
        [XmlElement("DesignTools", typeof(Equinox.ProceduralWorld.Buildings.Exporter.Ob_DesignTools))]
        [XmlElement("ProceduralStation", typeof(Equinox.ProceduralWorld.Buildings.Game.Ob_ProceduralStation))]
        [XmlElement("ProceduralFactions", typeof(Equinox.ProceduralWorld.Buildings.Seeds.Ob_ProceduralFactions))]
        [XmlElement("PartManager", typeof(Equinox.ProceduralWorld.Buildings.Library.Ob_PartManager))]
        public List<Ob_ModSessionComponent> SessionComponents = new List<Ob_ModSessionComponent>();
    }
}