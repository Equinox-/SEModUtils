 
 

using System.Xml.Serialization;
using System.Collections.Generic;
namespace Equinox.Utils.Session
{
    public class MyModSessionComponentRegistryGen
    {
        public static void Register() {
            MyModSessionComponentRegistry.Register<Equinox.Utils.Session.MySessionBootstrapper, Equinox.Utils.Session.MyObjectBuilder_SessionBootstrapper>();
            MyModSessionComponentRegistry.Register<Equinox.Utils.Command.MyCommandDispatchComponent, Equinox.Utils.Command.MyObjectBuilder_CommandDispatch>();
            MyModSessionComponentRegistry.Register<Equinox.Utils.Network.MyNetworkComponent, Equinox.Utils.Network.MyObjectBuilder_Network>();
            MyModSessionComponentRegistry.Register<Equinox.Utils.Network.MyRPCComponent, Equinox.Utils.Network.MyObjectBuilder_RPC>();
            MyModSessionComponentRegistry.Register<Equinox.Utils.Network.MySyncComponent, Equinox.Utils.Network.MyObjectBuilder_Sync>();
            MyModSessionComponentRegistry.Register<Equinox.Utils.Logging.MyVRageLogger, Equinox.Utils.Logging.MyObjectBuilder_VRageLogger>();
            MyModSessionComponentRegistry.Register<Equinox.Utils.Logging.MyCustomLogger, Equinox.Utils.Logging.MyObjectBuilder_CustomLogger>();
            MyModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Buildings.MyBuildingControlCommands, Equinox.ProceduralWorld.Buildings.MyObjectBuilder_BuildingControlCommands>();
            MyModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Manager.MyProceduralWorldManager, Equinox.ProceduralWorld.Manager.MyObjectBuilder_ProceduralWorldManager>();
            MyModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Buildings.Library.MyPartManager, Equinox.ProceduralWorld.Buildings.Library.MyObjectBuilder_PartManager>();
            MyModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Buildings.Game.MyProceduralStationModule, Equinox.ProceduralWorld.Buildings.Game.MyObjectBuilder_ProceduralStation>();
            MyModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Voxels.Asteroids.MyAutomaticAsteroidFieldsComponent, Equinox.ProceduralWorld.Voxels.Asteroids.MyObjectBuilder_AutomaticAsteroidFields>();
            MyModSessionComponentRegistry.Register<Equinox.ProceduralWorld.Voxels.Asteroids.MyAsteroidFieldModule, Equinox.ProceduralWorld.Voxels.Asteroids.MyObjectBuilder_AsteroidField>();
        }
    }

    public partial class MyObjectBuilder_SessionManager
    {
        [XmlElement("SessionBootstrapper", typeof(Equinox.Utils.Session.MyObjectBuilder_SessionBootstrapper))]
        [XmlElement("CommandDispatch", typeof(Equinox.Utils.Command.MyObjectBuilder_CommandDispatch))]
        [XmlElement("Network", typeof(Equinox.Utils.Network.MyObjectBuilder_Network))]
        [XmlElement("RPC", typeof(Equinox.Utils.Network.MyObjectBuilder_RPC))]
        [XmlElement("Sync", typeof(Equinox.Utils.Network.MyObjectBuilder_Sync))]
        [XmlElement("VRageLogger", typeof(Equinox.Utils.Logging.MyObjectBuilder_VRageLogger))]
        [XmlElement("CustomLogger", typeof(Equinox.Utils.Logging.MyObjectBuilder_CustomLogger))]
        [XmlElement("BuildingControlCommands", typeof(Equinox.ProceduralWorld.Buildings.MyObjectBuilder_BuildingControlCommands))]
        [XmlElement("ProceduralWorldManager", typeof(Equinox.ProceduralWorld.Manager.MyObjectBuilder_ProceduralWorldManager))]
        [XmlElement("PartManager", typeof(Equinox.ProceduralWorld.Buildings.Library.MyObjectBuilder_PartManager))]
        [XmlElement("ProceduralStation", typeof(Equinox.ProceduralWorld.Buildings.Game.MyObjectBuilder_ProceduralStation))]
        [XmlElement("AutomaticAsteroidFields", typeof(Equinox.ProceduralWorld.Voxels.Asteroids.MyObjectBuilder_AutomaticAsteroidFields))]
        [XmlElement("AsteroidField", typeof(Equinox.ProceduralWorld.Voxels.Asteroids.MyObjectBuilder_AsteroidField))]
        public List<MyObjectBuilder_ModSessionComponent> SessionComponents = new List<MyObjectBuilder_ModSessionComponent>();
    }
}