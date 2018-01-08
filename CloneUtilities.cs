using System;
using System.Collections.Generic;
using System.Linq;
using Equinox.Utils.Cache;
using Sandbox.ModAPI;
using VRage.Game;
using VRage.Game.ObjectBuilders;
using VRage.Game.ObjectBuilders.ComponentSystem;
using VRage.ObjectBuilders;
using VRage.Serialization;
using VRageMath;

namespace Equinox.Utils
{
#pragma warning disable CS0618
    public static class CloneUtilities
    {
        private static readonly LruCache<MyObjectBuilder_Base, string> SerializedObjectBuilderCache = new LruCache<MyObjectBuilder_Base, string>(100, null);

        public static T Clone<T>(T src) where T : class
        {
            return src == null ? null : MyAPIGateway.Utilities.SerializeFromXML<T>(MyAPIGateway.Utilities.SerializeToXML(src));
        }

        public static T CloneCached<T>(T src) where T : MyObjectBuilder_Base
        {
            return MyAPIGateway.Utilities.SerializeFromXML<T>(SerializedObjectBuilderCache.GetOrCreate(src, MyAPIGateway.Utilities.SerializeToXML));
        }

        public static MyObjectBuilder_CubeGrid CloneFast(MyObjectBuilder_CubeGrid src)
        {
//            if (true) return (MyObjectBuilder_CubeGrid)src.Clone();
            if (src == null) return null;
            // TODO check version numbers for slightly better code?
            if (src.GetType() != typeof(MyObjectBuilder_CubeGrid)) return (MyObjectBuilder_CubeGrid)src.Clone();
            var res = new MyObjectBuilder_CubeGrid();
            res.GridSizeEnum = src.GridSizeEnum;
            res.CubeBlocks = src.CubeBlocks == null ? null : new List<MyObjectBuilder_CubeBlock>(src.CubeBlocks.Select(CloneFast));
            res.IsStatic = src.IsStatic;
            res.IsUnsupportedStation = src.IsUnsupportedStation;
            res.Skeleton = src.Skeleton == null ? null : new List<BoneInfo>(src.Skeleton);
            res.LinearVelocity = src.LinearVelocity;
            res.AngularVelocity = src.AngularVelocity;
            res.XMirroxPlane = src.XMirroxPlane;
            res.YMirroxPlane = src.YMirroxPlane;
            res.ZMirroxPlane = src.ZMirroxPlane;
            res.XMirroxOdd = src.XMirroxOdd;
            res.YMirroxOdd = src.YMirroxOdd;
            res.ZMirroxOdd = src.ZMirroxOdd;
            res.DampenersEnabled = src.DampenersEnabled;
            res.UsePositionForSpawn = src.UsePositionForSpawn;
            res.PlanetSpawnHeightRatio = src.PlanetSpawnHeightRatio;
            res.SpawnRangeMin = src.SpawnRangeMin;
            res.SpawnRangeMax = src.SpawnRangeMax;
            res.ConveyorLines = src.ConveyorLines == null ? null : new List<MyObjectBuilder_ConveyorLine>(src.ConveyorLines.Select(CloneFast));
            res.BlockGroups = src.BlockGroups == null ? null : new List<MyObjectBuilder_BlockGroup>(src.BlockGroups.Select(CloneFast));
            res.Handbrake = src.Handbrake;
            res.DisplayName = src.DisplayName;
            res.OxygenAmount = (float[])src.OxygenAmount?.Clone();
            res.DestructibleBlocks = src.DestructibleBlocks;
            res.JumpDriveDirection = src.JumpDriveDirection;
            res.JumpRemainingTime = src.JumpRemainingTime;
            res.CreatePhysics = src.CreatePhysics;
            res.EnableSmallToLargeConnections = src.EnableSmallToLargeConnections;
            res.IsRespawnGrid = src.IsRespawnGrid;
            res.playedTime = src.playedTime;
            res.GridGeneralDamageModifier = src.GridGeneralDamageModifier;
            res.LocalCoordSys = src.LocalCoordSys;
            res.Editable = src.Editable;
            res.TargetingTargets = src.TargetingTargets == null ? null : new List<long>(src.TargetingTargets);
            res.TargetingWhitelist = src.TargetingWhitelist;
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_CubeBlock CloneFast(MyObjectBuilder_CubeBlock src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_FracturedBlock)) return CloneFast((MyObjectBuilder_FracturedBlock)src);
            if (src.GetType() == typeof(MyObjectBuilder_TerminalBlock)) return CloneFast((MyObjectBuilder_TerminalBlock)src);
            if (src.GetType() == typeof(MyObjectBuilder_FunctionalBlock)) return CloneFast((MyObjectBuilder_FunctionalBlock)src);
            if (src.GetType() == typeof(MyObjectBuilder_ProjectorBase)) return CloneFast((MyObjectBuilder_ProjectorBase)src);
            if (src.GetType() == typeof(MyObjectBuilder_CargoContainer)) return CloneFast((MyObjectBuilder_CargoContainer)src);
            if (src.GetType() == typeof(MyObjectBuilder_CompoundCubeBlock)) return CloneFast((MyObjectBuilder_CompoundCubeBlock)src);
            if (src.GetType() == typeof(MyObjectBuilder_DebugSphere3)) return CloneFast((MyObjectBuilder_DebugSphere3)src);
            if (src.GetType() == typeof(MyObjectBuilder_DebugSphere2)) return CloneFast((MyObjectBuilder_DebugSphere2)src);
            if (src.GetType() == typeof(MyObjectBuilder_DebugSphere1)) return CloneFast((MyObjectBuilder_DebugSphere1)src);
            if (src.GetType() != typeof(MyObjectBuilder_CubeBlock)) return (MyObjectBuilder_CubeBlock)src.Clone();
            var res = new MyObjectBuilder_CubeBlock();
            res.EntityId = src.EntityId;
            res.Name = src.Name;
            res.Min = src.Min;
            res.IntegrityPercent = src.IntegrityPercent;
            res.BuildPercent = src.BuildPercent;
            res.BlockOrientation = src.BlockOrientation;
            res.ConstructionStockpile = CloneFast(src.ConstructionStockpile);
            res.Owner = src.Owner;
            res.BuiltBy = src.BuiltBy;
            res.ShareMode = src.ShareMode;
            res.SubBlocks = (MyObjectBuilder_CubeBlock.MySubBlockId[])src.SubBlocks?.Clone();
            res.MultiBlockId = src.MultiBlockId;
            res.MultiBlockDefinition = src.MultiBlockDefinition;
            res.MultiBlockIndex = src.MultiBlockIndex;
            res.BlockGeneralDamageModifier = src.BlockGeneralDamageModifier;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_ConveyorLine CloneFast(MyObjectBuilder_ConveyorLine src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_ConveyorLine)) return (MyObjectBuilder_ConveyorLine)src.Clone();
            var res = new MyObjectBuilder_ConveyorLine();
            res.StartPosition = src.StartPosition;
            res.StartDirection = src.StartDirection;
            res.EndPosition = src.EndPosition;
            res.EndDirection = src.EndDirection;
            res.PacketsForward = src.PacketsForward == null ? null : new List<MyObjectBuilder_ConveyorPacket>(src.PacketsForward.Select(CloneFast));
            res.PacketsBackward = src.PacketsBackward == null ? null : new List<MyObjectBuilder_ConveyorPacket>(src.PacketsBackward.Select(CloneFast));
            res.Sections = src.Sections == null ? null : new List<SerializableLineSectionInformation>(src.Sections);
            res.ConveyorLineType = src.ConveyorLineType;
            res.ConveyorLineConductivity = src.ConveyorLineConductivity;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_BlockGroup CloneFast(MyObjectBuilder_BlockGroup src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_BlockGroup)) return (MyObjectBuilder_BlockGroup)src.Clone();
            var res = new MyObjectBuilder_BlockGroup();
            res.Name = src.Name;
            res.Blocks = src.Blocks == null ? null : new List<Vector3I>(src.Blocks);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_ComponentContainer CloneFast(MyObjectBuilder_ComponentContainer src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_ComponentContainer)) return (MyObjectBuilder_ComponentContainer)src.Clone();
            var res = new MyObjectBuilder_ComponentContainer();
            res.Components = src.Components == null ? null : new List<MyObjectBuilder_ComponentContainer.ComponentData>(src.Components.Select(CloneFast));
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_FracturedBlock CloneFast(MyObjectBuilder_FracturedBlock src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_FracturedBlock)) return (MyObjectBuilder_FracturedBlock)src.Clone();
            var res = new MyObjectBuilder_FracturedBlock();
            res.BlockDefinitions = src.BlockDefinitions == null ? null : new List<SerializableDefinitionId>(src.BlockDefinitions);
            res.Shapes = src.Shapes == null ? null : new List<MyObjectBuilder_FracturedBlock.ShapeB>(src.Shapes);
            res.BlockOrientations = src.BlockOrientations == null ? null : new List<SerializableBlockOrientation>(src.BlockOrientations);
            res.CreatingFracturedBlock = src.CreatingFracturedBlock;
            res.MultiBlocks = src.MultiBlocks == null ? null : new List<MyObjectBuilder_FracturedBlock.MyMultiBlockPart>(src.MultiBlocks.Select(CloneFast));
            res.EntityId = src.EntityId;
            res.Name = src.Name;
            res.Min = src.Min;
            res.IntegrityPercent = src.IntegrityPercent;
            res.BuildPercent = src.BuildPercent;
            res.BlockOrientation = src.BlockOrientation;
            res.ConstructionStockpile = CloneFast(src.ConstructionStockpile);
            res.Owner = src.Owner;
            res.BuiltBy = src.BuiltBy;
            res.ShareMode = src.ShareMode;
            res.SubBlocks = (MyObjectBuilder_CubeBlock.MySubBlockId[])src.SubBlocks?.Clone();
            res.MultiBlockId = src.MultiBlockId;
            res.MultiBlockDefinition = src.MultiBlockDefinition;
            res.MultiBlockIndex = src.MultiBlockIndex;
            res.BlockGeneralDamageModifier = src.BlockGeneralDamageModifier;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_TerminalBlock CloneFast(MyObjectBuilder_TerminalBlock src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_FunctionalBlock)) return CloneFast((MyObjectBuilder_FunctionalBlock)src);
            if (src.GetType() == typeof(MyObjectBuilder_ProjectorBase)) return CloneFast((MyObjectBuilder_ProjectorBase)src);
            if (src.GetType() == typeof(MyObjectBuilder_CargoContainer)) return CloneFast((MyObjectBuilder_CargoContainer)src);
            if (src.GetType() == typeof(MyObjectBuilder_DebugSphere3)) return CloneFast((MyObjectBuilder_DebugSphere3)src);
            if (src.GetType() == typeof(MyObjectBuilder_DebugSphere2)) return CloneFast((MyObjectBuilder_DebugSphere2)src);
            if (src.GetType() == typeof(MyObjectBuilder_DebugSphere1)) return CloneFast((MyObjectBuilder_DebugSphere1)src);
            if (src.GetType() != typeof(MyObjectBuilder_TerminalBlock)) return (MyObjectBuilder_TerminalBlock)src.Clone();
            var res = new MyObjectBuilder_TerminalBlock();
            res.CustomName = src.CustomName;
            res.ShowOnHUD = src.ShowOnHUD;
            res.ShowInTerminal = src.ShowInTerminal;
            res.ShowInToolbarConfig = src.ShowInToolbarConfig;
            res.ShowInInventory = src.ShowInInventory;
            res.EntityId = src.EntityId;
            res.Name = src.Name;
            res.Min = src.Min;
            res.IntegrityPercent = src.IntegrityPercent;
            res.BuildPercent = src.BuildPercent;
            res.BlockOrientation = src.BlockOrientation;
            res.ConstructionStockpile = CloneFast(src.ConstructionStockpile);
            res.Owner = src.Owner;
            res.BuiltBy = src.BuiltBy;
            res.ShareMode = src.ShareMode;
            res.SubBlocks = (MyObjectBuilder_CubeBlock.MySubBlockId[])src.SubBlocks?.Clone();
            res.MultiBlockId = src.MultiBlockId;
            res.MultiBlockDefinition = src.MultiBlockDefinition;
            res.MultiBlockIndex = src.MultiBlockIndex;
            res.BlockGeneralDamageModifier = src.BlockGeneralDamageModifier;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_FunctionalBlock CloneFast(MyObjectBuilder_FunctionalBlock src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_ProjectorBase)) return CloneFast((MyObjectBuilder_ProjectorBase)src);
            if (src.GetType() == typeof(MyObjectBuilder_DebugSphere3)) return CloneFast((MyObjectBuilder_DebugSphere3)src);
            if (src.GetType() == typeof(MyObjectBuilder_DebugSphere2)) return CloneFast((MyObjectBuilder_DebugSphere2)src);
            if (src.GetType() == typeof(MyObjectBuilder_DebugSphere1)) return CloneFast((MyObjectBuilder_DebugSphere1)src);
            if (src.GetType() != typeof(MyObjectBuilder_FunctionalBlock)) return (MyObjectBuilder_FunctionalBlock)src.Clone();
            var res = new MyObjectBuilder_FunctionalBlock();
            res.Enabled = src.Enabled;
            res.CustomName = src.CustomName;
            res.ShowOnHUD = src.ShowOnHUD;
            res.ShowInTerminal = src.ShowInTerminal;
            res.ShowInToolbarConfig = src.ShowInToolbarConfig;
            res.ShowInInventory = src.ShowInInventory;
            res.EntityId = src.EntityId;
            res.Name = src.Name;
            res.Min = src.Min;
            res.IntegrityPercent = src.IntegrityPercent;
            res.BuildPercent = src.BuildPercent;
            res.BlockOrientation = src.BlockOrientation;
            res.ConstructionStockpile = CloneFast(src.ConstructionStockpile);
            res.Owner = src.Owner;
            res.BuiltBy = src.BuiltBy;
            res.ShareMode = src.ShareMode;
            res.SubBlocks = (MyObjectBuilder_CubeBlock.MySubBlockId[])src.SubBlocks?.Clone();
            res.MultiBlockId = src.MultiBlockId;
            res.MultiBlockDefinition = src.MultiBlockDefinition;
            res.MultiBlockIndex = src.MultiBlockIndex;
            res.BlockGeneralDamageModifier = src.BlockGeneralDamageModifier;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_ProjectorBase CloneFast(MyObjectBuilder_ProjectorBase src)
        {
            if (src == null) return null;
            return (MyObjectBuilder_ProjectorBase)src.Clone();
        }
        private static MyObjectBuilder_CargoContainer CloneFast(MyObjectBuilder_CargoContainer src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_CargoContainer)) return (MyObjectBuilder_CargoContainer)src.Clone();
            var res = new MyObjectBuilder_CargoContainer();
            res.Inventory = CloneFast(src.Inventory);
            res.ContainerType = src.ContainerType;
            res.CustomName = src.CustomName;
            res.ShowOnHUD = src.ShowOnHUD;
            res.ShowInTerminal = src.ShowInTerminal;
            res.ShowInToolbarConfig = src.ShowInToolbarConfig;
            res.ShowInInventory = src.ShowInInventory;
            res.EntityId = src.EntityId;
            res.Name = src.Name;
            res.Min = src.Min;
            res.IntegrityPercent = src.IntegrityPercent;
            res.BuildPercent = src.BuildPercent;
            res.BlockOrientation = src.BlockOrientation;
            res.ConstructionStockpile = CloneFast(src.ConstructionStockpile);
            res.Owner = src.Owner;
            res.BuiltBy = src.BuiltBy;
            res.ShareMode = src.ShareMode;
            res.SubBlocks = (MyObjectBuilder_CubeBlock.MySubBlockId[])src.SubBlocks?.Clone();
            res.MultiBlockId = src.MultiBlockId;
            res.MultiBlockDefinition = src.MultiBlockDefinition;
            res.MultiBlockIndex = src.MultiBlockIndex;
            res.BlockGeneralDamageModifier = src.BlockGeneralDamageModifier;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_CompoundCubeBlock CloneFast(MyObjectBuilder_CompoundCubeBlock src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_CompoundCubeBlock)) return (MyObjectBuilder_CompoundCubeBlock)src.Clone();
            var res = new MyObjectBuilder_CompoundCubeBlock();
            res.Blocks = src.Blocks == null ? null : new MyObjectBuilder_CubeBlock[src.Blocks.Length];
            if (src.Blocks != null)
                for (var i = 0; i < src.Blocks.Length; i++) res.Blocks[i] = CloneFast(src.Blocks[i]);
            res.BlockIds = (ushort[])src.BlockIds?.Clone();
            res.EntityId = src.EntityId;
            res.Name = src.Name;
            res.Min = src.Min;
            res.IntegrityPercent = src.IntegrityPercent;
            res.BuildPercent = src.BuildPercent;
            res.BlockOrientation = src.BlockOrientation;
            res.ConstructionStockpile = CloneFast(src.ConstructionStockpile);
            res.Owner = src.Owner;
            res.BuiltBy = src.BuiltBy;
            res.ShareMode = src.ShareMode;
            res.SubBlocks = (MyObjectBuilder_CubeBlock.MySubBlockId[])src.SubBlocks?.Clone();
            res.MultiBlockId = src.MultiBlockId;
            res.MultiBlockDefinition = src.MultiBlockDefinition;
            res.MultiBlockIndex = src.MultiBlockIndex;
            res.BlockGeneralDamageModifier = src.BlockGeneralDamageModifier;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_DebugSphere3 CloneFast(MyObjectBuilder_DebugSphere3 src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_DebugSphere3)) return (MyObjectBuilder_DebugSphere3)src.Clone();
            var res = new MyObjectBuilder_DebugSphere3();
            res.Enabled = src.Enabled;
            res.CustomName = src.CustomName;
            res.ShowOnHUD = src.ShowOnHUD;
            res.ShowInTerminal = src.ShowInTerminal;
            res.ShowInToolbarConfig = src.ShowInToolbarConfig;
            res.ShowInInventory = src.ShowInInventory;
            res.EntityId = src.EntityId;
            res.Name = src.Name;
            res.Min = src.Min;
            res.IntegrityPercent = src.IntegrityPercent;
            res.BuildPercent = src.BuildPercent;
            res.BlockOrientation = src.BlockOrientation;
            res.ConstructionStockpile = CloneFast(src.ConstructionStockpile);
            res.Owner = src.Owner;
            res.BuiltBy = src.BuiltBy;
            res.ShareMode = src.ShareMode;
            res.SubBlocks = (MyObjectBuilder_CubeBlock.MySubBlockId[])src.SubBlocks?.Clone();
            res.MultiBlockId = src.MultiBlockId;
            res.MultiBlockDefinition = src.MultiBlockDefinition;
            res.MultiBlockIndex = src.MultiBlockIndex;
            res.BlockGeneralDamageModifier = src.BlockGeneralDamageModifier;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_DebugSphere2 CloneFast(MyObjectBuilder_DebugSphere2 src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_DebugSphere2)) return (MyObjectBuilder_DebugSphere2)src.Clone();
            var res = new MyObjectBuilder_DebugSphere2();
            res.Enabled = src.Enabled;
            res.CustomName = src.CustomName;
            res.ShowOnHUD = src.ShowOnHUD;
            res.ShowInTerminal = src.ShowInTerminal;
            res.ShowInToolbarConfig = src.ShowInToolbarConfig;
            res.ShowInInventory = src.ShowInInventory;
            res.EntityId = src.EntityId;
            res.Name = src.Name;
            res.Min = src.Min;
            res.IntegrityPercent = src.IntegrityPercent;
            res.BuildPercent = src.BuildPercent;
            res.BlockOrientation = src.BlockOrientation;
            res.ConstructionStockpile = CloneFast(src.ConstructionStockpile);
            res.Owner = src.Owner;
            res.BuiltBy = src.BuiltBy;
            res.ShareMode = src.ShareMode;
            res.SubBlocks = (MyObjectBuilder_CubeBlock.MySubBlockId[])src.SubBlocks?.Clone();
            res.MultiBlockId = src.MultiBlockId;
            res.MultiBlockDefinition = src.MultiBlockDefinition;
            res.MultiBlockIndex = src.MultiBlockIndex;
            res.BlockGeneralDamageModifier = src.BlockGeneralDamageModifier;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_DebugSphere1 CloneFast(MyObjectBuilder_DebugSphere1 src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_DebugSphere1)) return (MyObjectBuilder_DebugSphere1)src.Clone();
            var res = new MyObjectBuilder_DebugSphere1();
            res.Enabled = src.Enabled;
            res.CustomName = src.CustomName;
            res.ShowOnHUD = src.ShowOnHUD;
            res.ShowInTerminal = src.ShowInTerminal;
            res.ShowInToolbarConfig = src.ShowInToolbarConfig;
            res.ShowInInventory = src.ShowInInventory;
            res.EntityId = src.EntityId;
            res.Name = src.Name;
            res.Min = src.Min;
            res.IntegrityPercent = src.IntegrityPercent;
            res.BuildPercent = src.BuildPercent;
            res.BlockOrientation = src.BlockOrientation;
            res.ConstructionStockpile = CloneFast(src.ConstructionStockpile);
            res.Owner = src.Owner;
            res.BuiltBy = src.BuiltBy;
            res.ShareMode = src.ShareMode;
            res.SubBlocks = (MyObjectBuilder_CubeBlock.MySubBlockId[])src.SubBlocks?.Clone();
            res.MultiBlockId = src.MultiBlockId;
            res.MultiBlockDefinition = src.MultiBlockDefinition;
            res.MultiBlockIndex = src.MultiBlockIndex;
            res.BlockGeneralDamageModifier = src.BlockGeneralDamageModifier;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_ConstructionStockpile CloneFast(MyObjectBuilder_ConstructionStockpile src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_ConstructionStockpile)) return (MyObjectBuilder_ConstructionStockpile)src.Clone();
            var res = new MyObjectBuilder_ConstructionStockpile();
            res.Items = src.Items == null ? null : new MyObjectBuilder_StockpileItem[src.Items.Length];
            if (src.Items != null)
                for (var i = 0; i < src.Items.Length; i++) res.Items[i] = CloneFast(src.Items[i]);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_ConveyorPacket CloneFast(MyObjectBuilder_ConveyorPacket src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_ConveyorPacket)) return (MyObjectBuilder_ConveyorPacket)src.Clone();
            var res = new MyObjectBuilder_ConveyorPacket();
            res.Item = CloneFast(src.Item);
            res.LinePosition = src.LinePosition;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_ComponentContainer.ComponentData CloneFast(MyObjectBuilder_ComponentContainer.ComponentData src)
        {
            if (src == null) return null;
            var res = new MyObjectBuilder_ComponentContainer.ComponentData();
            res.TypeId = src.TypeId;
            res.Component = CloneFast(src.Component);
            return res;
        }
        private static MyObjectBuilder_FracturedBlock.MyMultiBlockPart CloneFast(MyObjectBuilder_FracturedBlock.MyMultiBlockPart src)
        {
            if (src == null) return null;
            var res = new MyObjectBuilder_FracturedBlock.MyMultiBlockPart();
            res.MultiBlockDefinition = src.MultiBlockDefinition;
            res.MultiBlockId = src.MultiBlockId;
            return res;
        }
        private static MyObjectBuilder_Inventory CloneFast(MyObjectBuilder_Inventory src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_Inventory)) return (MyObjectBuilder_Inventory)src.Clone();
            var res = new MyObjectBuilder_Inventory();
            res.Items = src.Items == null ? null : new List<MyObjectBuilder_InventoryItem>(src.Items.Select(CloneFast));
            res.nextItemId = src.nextItemId;
            res.Volume = src.Volume;
            res.Mass = src.Mass;
            res.MaxItemCount = src.MaxItemCount;
            res.Size = src.Size;
            res.InventoryFlags = src.InventoryFlags;
            res.RemoveEntityOnEmpty = src.RemoveEntityOnEmpty;
            res.InventoryId = src.InventoryId;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_StockpileItem CloneFast(MyObjectBuilder_StockpileItem src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_StockpileItem)) return (MyObjectBuilder_StockpileItem)src.Clone();
            var res = new MyObjectBuilder_StockpileItem();
            res.Amount = src.Amount;
            res.PhysicalContent = CloneFast(src.PhysicalContent);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_InventoryItem CloneFast(MyObjectBuilder_InventoryItem src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_InventoryItem)) return (MyObjectBuilder_InventoryItem)src.Clone();
            var res = new MyObjectBuilder_InventoryItem();
            res.Amount = src.Amount;
            res.Scale = src.Scale;
            res.Content = CloneFast(src.Content);
            res.PhysicalContent = CloneFast(src.PhysicalContent);
            res.ItemId = src.ItemId;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_ComponentBase CloneFast(MyObjectBuilder_ComponentBase src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_AtmosphereDetectorComponent)) return CloneFast((MyObjectBuilder_AtmosphereDetectorComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_CharacterPickupComponent)) return CloneFast((MyObjectBuilder_CharacterPickupComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_CraftingComponentBase)) return CloneFast((MyObjectBuilder_CraftingComponentBase)src);
            if (src.GetType() == typeof(MyObjectBuilder_CraftingComponentInteractive)) return CloneFast((MyObjectBuilder_CraftingComponentInteractive)src);
            if (src.GetType() == typeof(MyObjectBuilder_EntityReverbDetectorComponent)) return CloneFast((MyObjectBuilder_EntityReverbDetectorComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_ModStorageComponent)) return CloneFast((MyObjectBuilder_ModStorageComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_TriggerBase)) return CloneFast((MyObjectBuilder_TriggerBase)src);
            if (src.GetType() == typeof(MyObjectBuilder_AreaTrigger)) return CloneFast((MyObjectBuilder_AreaTrigger)src);
            if (src.GetType() == typeof(MyObjectBuilder_ShipSoundComponent)) return CloneFast((MyObjectBuilder_ShipSoundComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_PhysicsComponentBase)) return CloneFast((MyObjectBuilder_PhysicsComponentBase)src);
            if (src.GetType() == typeof(MyObjectBuilder_PhysicsBodyComponent)) return CloneFast((MyObjectBuilder_PhysicsBodyComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_InventoryBase)) return CloneFast((MyObjectBuilder_InventoryBase)src);
            if (src.GetType() == typeof(MyObjectBuilder_AreaInventory)) return CloneFast((MyObjectBuilder_AreaInventory)src);
            if (src.GetType() == typeof(MyObjectBuilder_InventoryAggregate)) return CloneFast((MyObjectBuilder_InventoryAggregate)src);
            if (src.GetType() == typeof(MyObjectBuilder_AreaInventoryAggregate)) return CloneFast((MyObjectBuilder_AreaInventoryAggregate)src);
            if (src.GetType() == typeof(MyObjectBuilder_CraftingComponentCharacter)) return CloneFast((MyObjectBuilder_CraftingComponentCharacter)src);
            if (src.GetType() == typeof(MyObjectBuilder_CraftingComponentBlock)) return CloneFast((MyObjectBuilder_CraftingComponentBlock)src);
            if (src.GetType() == typeof(MyObjectBuilder_CraftingComponentBasic)) return CloneFast((MyObjectBuilder_CraftingComponentBasic)src);
            if (src.GetType() == typeof(MyObjectBuilder_CharacterSoundComponent)) return CloneFast((MyObjectBuilder_CharacterSoundComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_EntityStatComponent)) return CloneFast((MyObjectBuilder_EntityStatComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_CharacterStatComponent)) return CloneFast((MyObjectBuilder_CharacterStatComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_EntityDurabilityComponent)) return CloneFast((MyObjectBuilder_EntityDurabilityComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_FractureComponentBase)) return CloneFast((MyObjectBuilder_FractureComponentBase)src);
            if (src.GetType() == typeof(MyObjectBuilder_FractureComponentCubeBlock)) return CloneFast((MyObjectBuilder_FractureComponentCubeBlock)src);
            if (src.GetType() == typeof(MyObjectBuilder_InventorySpawnComponent)) return CloneFast((MyObjectBuilder_InventorySpawnComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_ModelComponent)) return CloneFast((MyObjectBuilder_ModelComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_RespawnComponent)) return CloneFast((MyObjectBuilder_RespawnComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_UpdateTrigger)) return CloneFast((MyObjectBuilder_UpdateTrigger)src);
            if (src.GetType() == typeof(MyObjectBuilder_UseObjectsComponent)) return CloneFast((MyObjectBuilder_UseObjectsComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_TimerComponent)) return CloneFast((MyObjectBuilder_TimerComponent)src);
            if (src.GetType() == typeof(MyObjectBuilder_Inventory)) return CloneFast((MyObjectBuilder_Inventory)src);
            return (MyObjectBuilder_ComponentBase)src.Clone();
        }
        private static MyObjectBuilder_PhysicalObject CloneFast(MyObjectBuilder_PhysicalObject src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_UsableItem)) return CloneFast((MyObjectBuilder_UsableItem)src);
            if (src.GetType() == typeof(MyObjectBuilder_SchematicItem)) return CloneFast((MyObjectBuilder_SchematicItem)src);
            if (src.GetType() == typeof(MyObjectBuilder_TreeObject)) return CloneFast((MyObjectBuilder_TreeObject)src);
            if (src.GetType() == typeof(MyObjectBuilder_AmmoMagazine)) return CloneFast((MyObjectBuilder_AmmoMagazine)src);
            if (src.GetType() == typeof(MyObjectBuilder_BlockItem)) return CloneFast((MyObjectBuilder_BlockItem)src);
            if (src.GetType() == typeof(MyObjectBuilder_Component)) return CloneFast((MyObjectBuilder_Component)src);
            if (src.GetType() == typeof(MyObjectBuilder_ConsumableItem)) return CloneFast((MyObjectBuilder_ConsumableItem)src);
            if (src.GetType() == typeof(MyObjectBuilder_Ingot)) return CloneFast((MyObjectBuilder_Ingot)src);
            if (src.GetType() == typeof(MyObjectBuilder_Ore)) return CloneFast((MyObjectBuilder_Ore)src);
            if (src.GetType() == typeof(MyObjectBuilder_PhysicalGunObject)) return CloneFast((MyObjectBuilder_PhysicalGunObject)src);
            if (src.GetType() != typeof(MyObjectBuilder_PhysicalObject)) return (MyObjectBuilder_PhysicalObject)src.Clone();
            var res = new MyObjectBuilder_PhysicalObject();
            res.Flags = src.Flags;
            res.DurabilityHP = src.DurabilityHP;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_AtmosphereDetectorComponent CloneFast(MyObjectBuilder_AtmosphereDetectorComponent src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_AtmosphereDetectorComponent)) return (MyObjectBuilder_AtmosphereDetectorComponent)src.Clone();
            var res = new MyObjectBuilder_AtmosphereDetectorComponent();
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_CharacterPickupComponent CloneFast(MyObjectBuilder_CharacterPickupComponent src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_CharacterPickupComponent)) return (MyObjectBuilder_CharacterPickupComponent)src.Clone();
            var res = new MyObjectBuilder_CharacterPickupComponent();
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_CraftingComponentBase CloneFast(MyObjectBuilder_CraftingComponentBase src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_CraftingComponentInteractive)) return CloneFast((MyObjectBuilder_CraftingComponentInteractive)src);
            if (src.GetType() == typeof(MyObjectBuilder_CraftingComponentCharacter)) return CloneFast((MyObjectBuilder_CraftingComponentCharacter)src);
            if (src.GetType() == typeof(MyObjectBuilder_CraftingComponentBlock)) return CloneFast((MyObjectBuilder_CraftingComponentBlock)src);
            if (src.GetType() == typeof(MyObjectBuilder_CraftingComponentBasic)) return CloneFast((MyObjectBuilder_CraftingComponentBasic)src);
            if (src.GetType() != typeof(MyObjectBuilder_CraftingComponentBase)) return (MyObjectBuilder_CraftingComponentBase)src.Clone();
            var res = new MyObjectBuilder_CraftingComponentBase();
            res.LockedByEntityId = src.LockedByEntityId;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_CraftingComponentInteractive CloneFast(MyObjectBuilder_CraftingComponentInteractive src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_CraftingComponentInteractive)) return (MyObjectBuilder_CraftingComponentInteractive)src.Clone();
            var res = new MyObjectBuilder_CraftingComponentInteractive();
            res.LockedByEntityId = src.LockedByEntityId;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_EntityReverbDetectorComponent CloneFast(MyObjectBuilder_EntityReverbDetectorComponent src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_EntityReverbDetectorComponent)) return (MyObjectBuilder_EntityReverbDetectorComponent)src.Clone();
            var res = new MyObjectBuilder_EntityReverbDetectorComponent();
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_ModStorageComponent CloneFast(MyObjectBuilder_ModStorageComponent src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_ModStorageComponent)) return (MyObjectBuilder_ModStorageComponent)src.Clone();
            var res = new MyObjectBuilder_ModStorageComponent();
            res.Storage = CloneFast(src.Storage);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_TriggerBase CloneFast(MyObjectBuilder_TriggerBase src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_AreaTrigger)) return CloneFast((MyObjectBuilder_AreaTrigger)src);
            if (src.GetType() == typeof(MyObjectBuilder_UpdateTrigger)) return CloneFast((MyObjectBuilder_UpdateTrigger)src);
            if (src.GetType() != typeof(MyObjectBuilder_TriggerBase)) return (MyObjectBuilder_TriggerBase)src.Clone();
            var res = new MyObjectBuilder_TriggerBase();
            res.Type = src.Type;
            res.AABB = src.AABB;
            res.BoundingSphere = src.BoundingSphere;
            res.Offset = src.Offset;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_AreaTrigger CloneFast(MyObjectBuilder_AreaTrigger src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_AreaTrigger)) return (MyObjectBuilder_AreaTrigger)src.Clone();
            var res = new MyObjectBuilder_AreaTrigger();
            res.Name = src.Name;
            res.Type = src.Type;
            res.AABB = src.AABB;
            res.BoundingSphere = src.BoundingSphere;
            res.Offset = src.Offset;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_ShipSoundComponent CloneFast(MyObjectBuilder_ShipSoundComponent src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_ShipSoundComponent)) return (MyObjectBuilder_ShipSoundComponent)src.Clone();
            var res = new MyObjectBuilder_ShipSoundComponent();
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_PhysicsComponentBase CloneFast(MyObjectBuilder_PhysicsComponentBase src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_PhysicsBodyComponent)) return CloneFast((MyObjectBuilder_PhysicsBodyComponent)src);
            return (MyObjectBuilder_PhysicsComponentBase)src.Clone();
        }
        private static MyObjectBuilder_PhysicsBodyComponent CloneFast(MyObjectBuilder_PhysicsBodyComponent src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_PhysicsBodyComponent)) return (MyObjectBuilder_PhysicsBodyComponent)src.Clone();
            var res = new MyObjectBuilder_PhysicsBodyComponent();
            res.LinearVelocity = src.LinearVelocity;
            res.AngularVelocity = src.AngularVelocity;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_InventoryBase CloneFast(MyObjectBuilder_InventoryBase src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_AreaInventory)) return CloneFast((MyObjectBuilder_AreaInventory)src);
            if (src.GetType() == typeof(MyObjectBuilder_InventoryAggregate)) return CloneFast((MyObjectBuilder_InventoryAggregate)src);
            if (src.GetType() == typeof(MyObjectBuilder_AreaInventoryAggregate)) return CloneFast((MyObjectBuilder_AreaInventoryAggregate)src);
            if (src.GetType() == typeof(MyObjectBuilder_Inventory)) return CloneFast((MyObjectBuilder_Inventory)src);
            if (src.GetType() != typeof(MyObjectBuilder_InventoryBase)) return (MyObjectBuilder_InventoryBase)src.Clone();
            var res = new MyObjectBuilder_InventoryBase();
            res.InventoryId = src.InventoryId;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_AreaInventory CloneFast(MyObjectBuilder_AreaInventory src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_AreaInventory)) return (MyObjectBuilder_AreaInventory)src.Clone();
            var res = new MyObjectBuilder_AreaInventory();
            res.InventoryId = src.InventoryId;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_InventoryAggregate CloneFast(MyObjectBuilder_InventoryAggregate src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_AreaInventoryAggregate)) return CloneFast((MyObjectBuilder_AreaInventoryAggregate)src);
            if (src.GetType() != typeof(MyObjectBuilder_InventoryAggregate)) return (MyObjectBuilder_InventoryAggregate)src.Clone();
            var res = new MyObjectBuilder_InventoryAggregate();
            res.Inventories = src.Inventories == null ? null : new List<MyObjectBuilder_InventoryBase>(src.Inventories.Select(CloneFast));
            res.InventoryId = src.InventoryId;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_AreaInventoryAggregate CloneFast(MyObjectBuilder_AreaInventoryAggregate src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_AreaInventoryAggregate)) return (MyObjectBuilder_AreaInventoryAggregate)src.Clone();
            var res = new MyObjectBuilder_AreaInventoryAggregate();
            res.Radius = src.Radius;
            res.Inventories = src.Inventories == null ? null : new List<MyObjectBuilder_InventoryBase>(src.Inventories.Select(CloneFast));
            res.InventoryId = src.InventoryId;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_CraftingComponentCharacter CloneFast(MyObjectBuilder_CraftingComponentCharacter src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_CraftingComponentCharacter)) return (MyObjectBuilder_CraftingComponentCharacter)src.Clone();
            var res = new MyObjectBuilder_CraftingComponentCharacter();
            res.LockedByEntityId = src.LockedByEntityId;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_CraftingComponentBlock CloneFast(MyObjectBuilder_CraftingComponentBlock src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_CraftingComponentBlock)) return (MyObjectBuilder_CraftingComponentBlock)src.Clone();
            var res = new MyObjectBuilder_CraftingComponentBlock();
            res.InsertedItems = src.InsertedItems == null ? null : new List<MyObjectBuilder_InventoryItem>(src.InsertedItems.Select(CloneFast));
            res.InsertedItemUseLevel = src.InsertedItemUseLevel;
            res.LockedByEntityId = src.LockedByEntityId;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_CraftingComponentBasic CloneFast(MyObjectBuilder_CraftingComponentBasic src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_CraftingComponentBasic)) return (MyObjectBuilder_CraftingComponentBasic)src.Clone();
            var res = new MyObjectBuilder_CraftingComponentBasic();
            res.LockedByEntityId = src.LockedByEntityId;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_CharacterSoundComponent CloneFast(MyObjectBuilder_CharacterSoundComponent src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_CharacterSoundComponent)) return (MyObjectBuilder_CharacterSoundComponent)src.Clone();
            var res = new MyObjectBuilder_CharacterSoundComponent();
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_EntityStatComponent CloneFast(MyObjectBuilder_EntityStatComponent src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_CharacterStatComponent)) return CloneFast((MyObjectBuilder_CharacterStatComponent)src);
            if (src.GetType() != typeof(MyObjectBuilder_EntityStatComponent)) return (MyObjectBuilder_EntityStatComponent)src.Clone();
            var res = new MyObjectBuilder_EntityStatComponent();
            res.Stats = src.Stats == null ? null : new MyObjectBuilder_EntityStat[src.Stats.Length];
            if (src.Stats != null)
                for (var i = 0; i < src.Stats.Length; i++) res.Stats[i] = CloneFast(src.Stats[i]);
            res.ScriptNames = (string[])src.ScriptNames?.Clone();
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_CharacterStatComponent CloneFast(MyObjectBuilder_CharacterStatComponent src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_CharacterStatComponent)) return (MyObjectBuilder_CharacterStatComponent)src.Clone();
            var res = new MyObjectBuilder_CharacterStatComponent();
            res.Stats = src.Stats == null ? null : new MyObjectBuilder_EntityStat[src.Stats.Length];
            if (src.Stats != null)
                for (var i = 0; i < src.Stats.Length; i++) res.Stats[i] = CloneFast(src.Stats[i]);
            res.ScriptNames = (string[])src.ScriptNames?.Clone();
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_EntityDurabilityComponent CloneFast(MyObjectBuilder_EntityDurabilityComponent src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_EntityDurabilityComponent)) return (MyObjectBuilder_EntityDurabilityComponent)src.Clone();
            var res = new MyObjectBuilder_EntityDurabilityComponent();
            res.EntityHP = src.EntityHP;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_FractureComponentBase CloneFast(MyObjectBuilder_FractureComponentBase src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_FractureComponentCubeBlock)) return CloneFast((MyObjectBuilder_FractureComponentCubeBlock)src);
            if (src.GetType() != typeof(MyObjectBuilder_FractureComponentBase)) return (MyObjectBuilder_FractureComponentBase)src.Clone();
            var res = new MyObjectBuilder_FractureComponentBase();
            res.Shapes = src.Shapes == null ? null : new List<MyObjectBuilder_FractureComponentBase.FracturedShape>(src.Shapes);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_FractureComponentCubeBlock CloneFast(MyObjectBuilder_FractureComponentCubeBlock src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_FractureComponentCubeBlock)) return (MyObjectBuilder_FractureComponentCubeBlock)src.Clone();
            var res = new MyObjectBuilder_FractureComponentCubeBlock();
            res.Shapes = src.Shapes == null ? null : new List<MyObjectBuilder_FractureComponentBase.FracturedShape>(src.Shapes);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_InventorySpawnComponent CloneFast(MyObjectBuilder_InventorySpawnComponent src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_InventorySpawnComponent)) return (MyObjectBuilder_InventorySpawnComponent)src.Clone();
            var res = new MyObjectBuilder_InventorySpawnComponent();
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_ModelComponent CloneFast(MyObjectBuilder_ModelComponent src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_ModelComponent)) return (MyObjectBuilder_ModelComponent)src.Clone();
            var res = new MyObjectBuilder_ModelComponent();
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_RespawnComponent CloneFast(MyObjectBuilder_RespawnComponent src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_RespawnComponent)) return (MyObjectBuilder_RespawnComponent)src.Clone();
            var res = new MyObjectBuilder_RespawnComponent();
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_UpdateTrigger CloneFast(MyObjectBuilder_UpdateTrigger src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_UpdateTrigger)) return (MyObjectBuilder_UpdateTrigger)src.Clone();
            var res = new MyObjectBuilder_UpdateTrigger();
            res.Size = src.Size;
            res.Type = src.Type;
            res.AABB = src.AABB;
            res.BoundingSphere = src.BoundingSphere;
            res.Offset = src.Offset;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_UseObjectsComponent CloneFast(MyObjectBuilder_UseObjectsComponent src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_UseObjectsComponent)) return (MyObjectBuilder_UseObjectsComponent)src.Clone();
            var res = new MyObjectBuilder_UseObjectsComponent();
            res.CustomDetectorsCount = src.CustomDetectorsCount;
            res.CustomDetectorsNames = (string[])src.CustomDetectorsNames?.Clone();
            res.CustomDetectorsMatrices = (Matrix[])src.CustomDetectorsMatrices?.Clone();
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_TimerComponent CloneFast(MyObjectBuilder_TimerComponent src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_TimerComponent)) return (MyObjectBuilder_TimerComponent)src.Clone();
            var res = new MyObjectBuilder_TimerComponent();
            res.Repeat = src.Repeat;
            res.TimeToEvent = src.TimeToEvent;
            res.SetTimeMinutes = src.SetTimeMinutes;
            res.TimerEnabled = src.TimerEnabled;
            res.RemoveEntityOnTimer = src.RemoveEntityOnTimer;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_UsableItem CloneFast(MyObjectBuilder_UsableItem src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_SchematicItem)) return CloneFast((MyObjectBuilder_SchematicItem)src);
            if (src.GetType() == typeof(MyObjectBuilder_ConsumableItem)) return CloneFast((MyObjectBuilder_ConsumableItem)src);
            if (src.GetType() != typeof(MyObjectBuilder_UsableItem)) return (MyObjectBuilder_UsableItem)src.Clone();
            var res = new MyObjectBuilder_UsableItem();
            res.Flags = src.Flags;
            res.DurabilityHP = src.DurabilityHP;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_SchematicItem CloneFast(MyObjectBuilder_SchematicItem src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_SchematicItem)) return (MyObjectBuilder_SchematicItem)src.Clone();
            var res = new MyObjectBuilder_SchematicItem();
            res.Flags = src.Flags;
            res.DurabilityHP = src.DurabilityHP;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_TreeObject CloneFast(MyObjectBuilder_TreeObject src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_TreeObject)) return (MyObjectBuilder_TreeObject)src.Clone();
            var res = new MyObjectBuilder_TreeObject();
            res.Flags = src.Flags;
            res.DurabilityHP = src.DurabilityHP;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_AmmoMagazine CloneFast(MyObjectBuilder_AmmoMagazine src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_AmmoMagazine)) return (MyObjectBuilder_AmmoMagazine)src.Clone();
            var res = new MyObjectBuilder_AmmoMagazine();
            res.ProjectilesCount = src.ProjectilesCount;
            res.Flags = src.Flags;
            res.DurabilityHP = src.DurabilityHP;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_BlockItem CloneFast(MyObjectBuilder_BlockItem src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_BlockItem)) return (MyObjectBuilder_BlockItem)src.Clone();
            var res = new MyObjectBuilder_BlockItem();
            res.BlockDefId = src.BlockDefId;
            res.Flags = src.Flags;
            res.DurabilityHP = src.DurabilityHP;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_Component CloneFast(MyObjectBuilder_Component src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_Component)) return (MyObjectBuilder_Component)src.Clone();
            var res = new MyObjectBuilder_Component();
            res.Flags = src.Flags;
            res.DurabilityHP = src.DurabilityHP;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_ConsumableItem CloneFast(MyObjectBuilder_ConsumableItem src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_ConsumableItem)) return (MyObjectBuilder_ConsumableItem)src.Clone();
            var res = new MyObjectBuilder_ConsumableItem();
            res.Flags = src.Flags;
            res.DurabilityHP = src.DurabilityHP;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_Ingot CloneFast(MyObjectBuilder_Ingot src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_Ingot)) return (MyObjectBuilder_Ingot)src.Clone();
            var res = new MyObjectBuilder_Ingot();
            res.Flags = src.Flags;
            res.DurabilityHP = src.DurabilityHP;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_Ore CloneFast(MyObjectBuilder_Ore src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_Ore)) return (MyObjectBuilder_Ore)src.Clone();
            var res = new MyObjectBuilder_Ore();
            res.Flags = src.Flags;
            res.DurabilityHP = src.DurabilityHP;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_PhysicalGunObject CloneFast(MyObjectBuilder_PhysicalGunObject src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_PhysicalGunObject)) return (MyObjectBuilder_PhysicalGunObject)src.Clone();
            var res = new MyObjectBuilder_PhysicalGunObject();
            res.GunEntity = CloneFast(src.GunEntity);
            res.Flags = src.Flags;
            res.DurabilityHP = src.DurabilityHP;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static SerializableDictionary<Guid, string> CloneFast(SerializableDictionary<Guid, string> src)
        {
            if (src == null) return null;
            var res = new SerializableDictionary<Guid, string>();
            res.Dictionary = new Dictionary<Guid, string>(src.Dictionary);
            return res;
        }
        private static MyObjectBuilder_EntityBase CloneFast(MyObjectBuilder_EntityBase src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_GhostCharacter)) return CloneFast((MyObjectBuilder_GhostCharacter)src);
            if (src.GetType() == typeof(MyObjectBuilder_FracturedPiece)) return CloneFast((MyObjectBuilder_FracturedPiece)src);
            if (src.GetType() == typeof(MyObjectBuilder_InventoryBagEntity)) return CloneFast((MyObjectBuilder_InventoryBagEntity)src);
            if (src.GetType() == typeof(MyObjectBuilder_Rope)) return CloneFast((MyObjectBuilder_Rope)src);
            if (src.GetType() == typeof(MyObjectBuilder_CubeGrid)) return CloneFast((MyObjectBuilder_CubeGrid)src);
            if (src.GetType() == typeof(MyObjectBuilder_EngineerToolBase)) return CloneFast((MyObjectBuilder_EngineerToolBase)src);
            if (src.GetType() == typeof(MyObjectBuilder_AreaMarker)) return CloneFast((MyObjectBuilder_AreaMarker)src);
            if (src.GetType() == typeof(MyObjectBuilder_Character)) return CloneFast((MyObjectBuilder_Character)src);
            if (src.GetType() == typeof(MyObjectBuilder_CubePlacer)) return CloneFast((MyObjectBuilder_CubePlacer)src);
            if (src.GetType() == typeof(MyObjectBuilder_DestroyableItem)) return CloneFast((MyObjectBuilder_DestroyableItem)src);
            if (src.GetType() == typeof(MyObjectBuilder_EnvironmentItems)) return CloneFast((MyObjectBuilder_EnvironmentItems)src);
            if (src.GetType() == typeof(MyObjectBuilder_DestroyableItems)) return CloneFast((MyObjectBuilder_DestroyableItems)src);
            if (src.GetType() == typeof(MyObjectBuilder_FloatingObject)) return CloneFast((MyObjectBuilder_FloatingObject)src);
            if (src.GetType() == typeof(MyObjectBuilder_HandToolBase)) return CloneFast((MyObjectBuilder_HandToolBase)src);
            if (src.GetType() == typeof(MyObjectBuilder_HandTool)) return CloneFast((MyObjectBuilder_HandTool)src);
            if (src.GetType() == typeof(MyObjectBuilder_GoodAIControlHandTool)) return CloneFast((MyObjectBuilder_GoodAIControlHandTool)src);
            if (src.GetType() == typeof(MyObjectBuilder_ManipulationTool)) return CloneFast((MyObjectBuilder_ManipulationTool)src);
            if (src.GetType() == typeof(MyObjectBuilder_Tree)) return CloneFast((MyObjectBuilder_Tree)src);
            if (src.GetType() == typeof(MyObjectBuilder_Trees)) return CloneFast((MyObjectBuilder_Trees)src);
            if (src.GetType() == typeof(MyObjectBuilder_Bushes)) return CloneFast((MyObjectBuilder_Bushes)src);
            if (src.GetType() == typeof(MyObjectBuilder_TreesMedium)) return CloneFast((MyObjectBuilder_TreesMedium)src);
            if (src.GetType() == typeof(MyObjectBuilder_VoxelMap)) return CloneFast((MyObjectBuilder_VoxelMap)src);
            if (src.GetType() == typeof(MyObjectBuilder_Planet)) return CloneFast((MyObjectBuilder_Planet)src);
            if (src.GetType() == typeof(MyObjectBuilder_ReplicableEntity)) return CloneFast((MyObjectBuilder_ReplicableEntity)src);
            if (src.GetType() != typeof(MyObjectBuilder_EntityBase)) return (MyObjectBuilder_EntityBase)src.Clone();
            var res = new MyObjectBuilder_EntityBase();
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_EntityStat CloneFast(MyObjectBuilder_EntityStat src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_EntityStat)) return (MyObjectBuilder_EntityStat)src.Clone();
            var res = new MyObjectBuilder_EntityStat();
            res.Value = src.Value;
            res.MaxValue = src.MaxValue;
            res.StatRegenAmountMultiplier = src.StatRegenAmountMultiplier;
            res.StatRegenAmountMultiplierDuration = src.StatRegenAmountMultiplierDuration;
            res.Effects = src.Effects == null ? null : new MyObjectBuilder_EntityStatRegenEffect[src.Effects.Length];
            if (src.Effects != null)
                for (var i = 0; i < src.Effects.Length; i++) res.Effects[i] = CloneFast(src.Effects[i]);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_GhostCharacter CloneFast(MyObjectBuilder_GhostCharacter src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_GhostCharacter)) return (MyObjectBuilder_GhostCharacter)src.Clone();
            var res = new MyObjectBuilder_GhostCharacter();
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_FracturedPiece CloneFast(MyObjectBuilder_FracturedPiece src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_FracturedPiece)) return (MyObjectBuilder_FracturedPiece)src.Clone();
            var res = new MyObjectBuilder_FracturedPiece();
            res.BlockDefinitions = src.BlockDefinitions == null ? null : new List<SerializableDefinitionId>(src.BlockDefinitions);
            res.Shapes = src.Shapes == null ? null : new List<MyObjectBuilder_FracturedPiece.Shape>(src.Shapes);
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_InventoryBagEntity CloneFast(MyObjectBuilder_InventoryBagEntity src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_InventoryBagEntity)) return (MyObjectBuilder_InventoryBagEntity)src.Clone();
            var res = new MyObjectBuilder_InventoryBagEntity();
            res.LinearVelocity = src.LinearVelocity;
            res.AngularVelocity = src.AngularVelocity;
            res.Mass = src.Mass;
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_Rope CloneFast(MyObjectBuilder_Rope src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_Rope)) return (MyObjectBuilder_Rope)src.Clone();
            var res = new MyObjectBuilder_Rope();
            res.MaxRopeLength = src.MaxRopeLength;
            res.CurrentRopeLength = src.CurrentRopeLength;
            res.EntityIdHookA = src.EntityIdHookA;
            res.EntityIdHookB = src.EntityIdHookB;
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_EngineerToolBase CloneFast(MyObjectBuilder_EngineerToolBase src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_EngineerToolBase)) return (MyObjectBuilder_EngineerToolBase)src.Clone();
            var res = new MyObjectBuilder_EngineerToolBase();
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_AreaMarker CloneFast(MyObjectBuilder_AreaMarker src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_AreaMarker)) return (MyObjectBuilder_AreaMarker)src.Clone();
            var res = new MyObjectBuilder_AreaMarker();
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_Character CloneFast(MyObjectBuilder_Character src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_Character)) return (MyObjectBuilder_Character)src.Clone();
            var res = new MyObjectBuilder_Character();
            res.CharacterModel = src.CharacterModel;
            res.Inventory = CloneFast(src.Inventory);
            res.HandWeapon = CloneFast(src.HandWeapon);
            res.Battery = CloneFast(src.Battery);
            res.LightEnabled = src.LightEnabled;
            res.DampenersEnabled = src.DampenersEnabled;
            res.CharacterGeneralDamageModifier = src.CharacterGeneralDamageModifier;
            res.UsingLadder = src.UsingLadder;
            res.HeadAngle = src.HeadAngle;
            res.LinearVelocity = src.LinearVelocity;
            res.AutoenableJetpackDelay = src.AutoenableJetpackDelay;
            res.JetpackEnabled = src.JetpackEnabled;
            res.AIMode = src.AIMode;
            res.ColorMaskHSV = src.ColorMaskHSV;
            res.LootingCounter = src.LootingCounter;
            res.DisplayName = src.DisplayName;
            res.IsInFirstPersonView = src.IsInFirstPersonView;
            res.EnableBroadcasting = src.EnableBroadcasting;
            res.OxygenLevel = src.OxygenLevel;
            res.EnvironmentOxygenLevel = src.EnvironmentOxygenLevel;
            res.StoredGases = src.StoredGases == null ? null : new List<MyObjectBuilder_Character.StoredGas>(src.StoredGases);
            res.MovementState = src.MovementState;
            res.EnabledComponents = src.EnabledComponents == null ? null : new List<string>(src.EnabledComponents);
            res.PlayerSteamId = src.PlayerSteamId;
            res.PlayerSerialId = src.PlayerSerialId;
            res.NeedsOxygenFromSuit = src.NeedsOxygenFromSuit;
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_CubePlacer CloneFast(MyObjectBuilder_CubePlacer src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_CubePlacer)) return (MyObjectBuilder_CubePlacer)src.Clone();
            var res = new MyObjectBuilder_CubePlacer();
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_DestroyableItem CloneFast(MyObjectBuilder_DestroyableItem src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_DestroyableItem)) return (MyObjectBuilder_DestroyableItem)src.Clone();
            var res = new MyObjectBuilder_DestroyableItem();
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_EnvironmentItems CloneFast(MyObjectBuilder_EnvironmentItems src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_DestroyableItems)) return CloneFast((MyObjectBuilder_DestroyableItems)src);
            if (src.GetType() == typeof(MyObjectBuilder_Trees)) return CloneFast((MyObjectBuilder_Trees)src);
            if (src.GetType() == typeof(MyObjectBuilder_Bushes)) return CloneFast((MyObjectBuilder_Bushes)src);
            if (src.GetType() == typeof(MyObjectBuilder_TreesMedium)) return CloneFast((MyObjectBuilder_TreesMedium)src);
            if (src.GetType() != typeof(MyObjectBuilder_EnvironmentItems)) return (MyObjectBuilder_EnvironmentItems)src.Clone();
            var res = new MyObjectBuilder_EnvironmentItems();
            res.Items = (MyObjectBuilder_EnvironmentItems.MyOBEnvironmentItemData[])src.Items?.Clone();
            res.CellsOffset = src.CellsOffset;
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_DestroyableItems CloneFast(MyObjectBuilder_DestroyableItems src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_DestroyableItems)) return (MyObjectBuilder_DestroyableItems)src.Clone();
            var res = new MyObjectBuilder_DestroyableItems();
            res.Items = (MyObjectBuilder_EnvironmentItems.MyOBEnvironmentItemData[])src.Items?.Clone();
            res.CellsOffset = src.CellsOffset;
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_FloatingObject CloneFast(MyObjectBuilder_FloatingObject src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_FloatingObject)) return (MyObjectBuilder_FloatingObject)src.Clone();
            var res = new MyObjectBuilder_FloatingObject();
            res.Item = CloneFast(src.Item);
            res.ModelVariant = src.ModelVariant;
            res.OreSubtypeId = src.OreSubtypeId;
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_HandToolBase CloneFast(MyObjectBuilder_HandToolBase src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_HandTool)) return CloneFast((MyObjectBuilder_HandTool)src);
            if (src.GetType() == typeof(MyObjectBuilder_GoodAIControlHandTool)) return CloneFast((MyObjectBuilder_GoodAIControlHandTool)src);
            if (src.GetType() != typeof(MyObjectBuilder_HandToolBase)) return (MyObjectBuilder_HandToolBase)src.Clone();
            var res = new MyObjectBuilder_HandToolBase();
            res.DeviceBase = CloneFast(src.DeviceBase);
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_HandTool CloneFast(MyObjectBuilder_HandTool src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_HandTool)) return (MyObjectBuilder_HandTool)src.Clone();
            var res = new MyObjectBuilder_HandTool();
            res.DeviceBase = CloneFast(src.DeviceBase);
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_GoodAIControlHandTool CloneFast(MyObjectBuilder_GoodAIControlHandTool src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_GoodAIControlHandTool)) return (MyObjectBuilder_GoodAIControlHandTool)src.Clone();
            var res = new MyObjectBuilder_GoodAIControlHandTool();
            res.DeviceBase = CloneFast(src.DeviceBase);
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_ManipulationTool CloneFast(MyObjectBuilder_ManipulationTool src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_ManipulationTool)) return (MyObjectBuilder_ManipulationTool)src.Clone();
            var res = new MyObjectBuilder_ManipulationTool();
            res.State = src.State;
            res.OtherEntityId = src.OtherEntityId;
            res.HeadLocalPivotPosition = src.HeadLocalPivotPosition;
            res.HeadLocalPivotOrientation = src.HeadLocalPivotOrientation;
            res.OtherLocalPivotPosition = src.OtherLocalPivotPosition;
            res.OtherLocalPivotOrientation = src.OtherLocalPivotOrientation;
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_Tree CloneFast(MyObjectBuilder_Tree src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_Tree)) return (MyObjectBuilder_Tree)src.Clone();
            var res = new MyObjectBuilder_Tree();
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_Trees CloneFast(MyObjectBuilder_Trees src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_TreesMedium)) return CloneFast((MyObjectBuilder_TreesMedium)src);
            if (src.GetType() != typeof(MyObjectBuilder_Trees)) return (MyObjectBuilder_Trees)src.Clone();
            var res = new MyObjectBuilder_Trees();
            res.Items = (MyObjectBuilder_EnvironmentItems.MyOBEnvironmentItemData[])src.Items?.Clone();
            res.CellsOffset = src.CellsOffset;
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_Bushes CloneFast(MyObjectBuilder_Bushes src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_Bushes)) return (MyObjectBuilder_Bushes)src.Clone();
            var res = new MyObjectBuilder_Bushes();
            res.Items = (MyObjectBuilder_EnvironmentItems.MyOBEnvironmentItemData[])src.Items?.Clone();
            res.CellsOffset = src.CellsOffset;
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_TreesMedium CloneFast(MyObjectBuilder_TreesMedium src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_TreesMedium)) return (MyObjectBuilder_TreesMedium)src.Clone();
            var res = new MyObjectBuilder_TreesMedium();
            res.Items = (MyObjectBuilder_EnvironmentItems.MyOBEnvironmentItemData[])src.Items?.Clone();
            res.CellsOffset = src.CellsOffset;
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_VoxelMap CloneFast(MyObjectBuilder_VoxelMap src)
        {
            if (src == null) return null;
            if (src.GetType() == typeof(MyObjectBuilder_Planet)) return CloneFast((MyObjectBuilder_Planet)src);
            if (src.GetType() != typeof(MyObjectBuilder_VoxelMap)) return (MyObjectBuilder_VoxelMap)src.Clone();
            var res = new MyObjectBuilder_VoxelMap();
            res.MutableStorage = src.MutableStorage;
            res.ContentChanged = src.ContentChanged;
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.StorageName = src.StorageName;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_Planet CloneFast(MyObjectBuilder_Planet src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_Planet)) return (MyObjectBuilder_Planet)src.Clone();
            var res = new MyObjectBuilder_Planet();
            res.Radius = src.Radius;
            res.HasAtmosphere = src.HasAtmosphere;
            res.AtmosphereRadius = src.AtmosphereRadius;
            res.MinimumSurfaceRadius = src.MinimumSurfaceRadius;
            res.MaximumHillRadius = src.MaximumHillRadius;
            res.AtmosphereWavelengths = src.AtmosphereWavelengths;
            res.SavedEnviromentSectors = (MyObjectBuilder_Planet.SavedSector[])src.SavedEnviromentSectors?.Clone();
            res.GravityFalloff = src.GravityFalloff;
            res.MarkAreaEmpty = src.MarkAreaEmpty;
            res.AtmosphereSettings = src.AtmosphereSettings;
            res.SurfaceGravity = src.SurfaceGravity;
            res.SpawnsFlora = src.SpawnsFlora;
            res.ShowGPS = src.ShowGPS;
            res.SpherizeWithDistance = src.SpherizeWithDistance;
            res.PlanetGenerator = src.PlanetGenerator;
            res.MutableStorage = src.MutableStorage;
            res.ContentChanged = src.ContentChanged;
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.StorageName = src.StorageName;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_ReplicableEntity CloneFast(MyObjectBuilder_ReplicableEntity src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_ReplicableEntity)) return (MyObjectBuilder_ReplicableEntity)src.Clone();
            var res = new MyObjectBuilder_ReplicableEntity();
            res.LinearVelocity = src.LinearVelocity;
            res.AngularVelocity = src.AngularVelocity;
            res.Mass = src.Mass;
            res.EntityId = src.EntityId;
            res.PersistentFlags = src.PersistentFlags;
            res.Name = src.Name;
            res.PositionAndOrientation = src.PositionAndOrientation;
            res.ComponentContainer = CloneFast(src.ComponentContainer);
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_EntityStatRegenEffect CloneFast(MyObjectBuilder_EntityStatRegenEffect src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_EntityStatRegenEffect)) return (MyObjectBuilder_EntityStatRegenEffect)src.Clone();
            var res = new MyObjectBuilder_EntityStatRegenEffect();
            res.TickAmount = src.TickAmount;
            res.Interval = src.Interval;
            res.MaxRegenRatio = src.MaxRegenRatio;
            res.MinRegenRatio = src.MinRegenRatio;
            res.AliveTime = src.AliveTime;
            res.Duration = src.Duration;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_Battery CloneFast(MyObjectBuilder_Battery src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_Battery)) return (MyObjectBuilder_Battery)src.Clone();
            var res = new MyObjectBuilder_Battery();
            res.ProducerEnabled = src.ProducerEnabled;
            res.CurrentCapacity = src.CurrentCapacity;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
        private static MyObjectBuilder_ToolBase CloneFast(MyObjectBuilder_ToolBase src)
        {
            if (src == null) return null;
            if (src.GetType() != typeof(MyObjectBuilder_ToolBase)) return (MyObjectBuilder_ToolBase)src.Clone();
            var res = new MyObjectBuilder_ToolBase();
            res.InventoryItemId = src.InventoryItemId;
            res.SubtypeName = src.SubtypeName;
            return res;
        }
    }
#pragma warning restore CS0618
}
