using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Equinox.ProceduralWorld.Utils.Session;
using Equinox.Utils;
using Equinox.Utils.DotNet;
using Equinox.Utils.Pool;
using Sandbox.ModAPI;
using VRage.Utils;
using VRage;
using VRage.Collections;

namespace Equinox.ProceduralWorld.Utils.Network
{
    public class MyNetworkComponent : MyLoggingSessionComponent
    {
        private readonly MyExactBufferPool m_bufferPool = new MyExactBufferPool(4 * 1024, 1024 * 1024);
        private readonly MyTypedObjectPool m_packetPool = new MyTypedObjectPool(4, 1024 * 32);
        public const ushort MessageChannel = 57654;

        private static readonly Type[] SuppliedDeps = new[] { typeof(MyNetworkComponent) };
        public override IEnumerable<Type> SuppliesComponents => SuppliedDeps;

        private class MyPacketInfo
        {
            public ulong PacketID { get; }
            public Func<MyPacket> Activator { get; }
            public Action<MyPacket> Handler { get; }
            public Type Type { get; }

            public MyPacketInfo(ulong id, Type type, Func<MyPacket> activator, Action<MyPacket> handler)
            {
                PacketID = id;
                Type = type;
                Activator = activator;
                Handler = handler;
            }
        }
        private readonly FastResourceLock m_packetDbLock = new FastResourceLock();
        private readonly Dictionary<ulong, MyPacketInfo> m_registeredPacketsByID = new Dictionary<ulong, MyPacketInfo>();
        private readonly Dictionary<Type, MyPacketInfo> m_registeredPacketsByType = new Dictionary<Type, MyPacketInfo>();

        public override void Attach()
        {
            base.Attach();
            MyAPIGateway.Multiplayer.RegisterMessageHandler(MessageChannel, MessageHandler);
        }

        public override void Detach()
        {
            base.Detach();
            MyAPIGateway.Multiplayer.UnregisterMessageHandler(MessageChannel, MessageHandler);
        }

        public void RegisterPacketType<T>(Action<T> handler) where T : MyPacket, new()
        {
            var info = new MyPacketInfo(typeof(T).FullName.Hash64(), typeof(T), AllocatePacket<T>, (x) => handler(x as T));
            using (m_packetDbLock.AcquireExclusiveUsing())
            {
                MyPacketInfo oldInfo;
                if (m_registeredPacketsByID.TryGetValue(info.PacketID, out oldInfo))
                {
                    Log(MyLogSeverity.Critical, "Registering packet {0} failed.  The ID collides with {1}", typeof(T), oldInfo.Type);
                    throw new ArgumentException($"Registering packet {typeof(T)} failed.  The ID collides with {oldInfo.Type}");
                }
                m_registeredPacketsByID.Add(info.PacketID, info);
                m_registeredPacketsByType.Add(info.Type, info);
            }
        }

        public void UnregisterPacketType<T>(Action<T> handler) where T : MyPacket, new()
        {
            using (m_packetDbLock.AcquireExclusiveUsing())
            {
                if (!m_registeredPacketsByID.Remove(typeof(T).FullName.Hash64()))
                    Log(MyLogSeverity.Warning, "Unregistered packet type {0} when it wasn't registered", typeof(T));
                m_registeredPacketsByType.Remove(typeof(T));
            }
        }


        public bool SendMessageToServer(MyPacket message, bool reliable = true)
        {
            var data = SerializePacket(message);
            var result = MyAPIGateway.Multiplayer.SendMessageToServer(MessageChannel, data, reliable);
            m_bufferPool.Return(data);
            return result;
        }

        public bool SendMessageToOthers(MyPacket message, bool reliable = true)
        {
            var data = SerializePacket(message);
            var result = MyAPIGateway.Multiplayer.SendMessageToOthers(MessageChannel, data, reliable);
            m_bufferPool.Return(data);
            return result;
        }

        public bool SendMessageTo(MyPacket message, ulong recipient, bool reliable = true)
        {
            var data = SerializePacket(message);
            var result = MyAPIGateway.Multiplayer.SendMessageTo(MessageChannel, data, recipient, reliable);
            m_bufferPool.Return(data);
            return result;
        }

        public T AllocatePacket<T>() where T : MyPacket, new()
        {
            return m_packetPool.GetOrCreate<T>();
        }

        public void ReturnPacket<T>(T packet) where T : MyPacket
        {
            m_packetPool.Return(packet);
        }

        private byte[] SerializePacket(MyPacket packet)
        {
            MyPacketInfo info;
            using (m_packetDbLock.AcquireSharedUsing())
                if (!m_registeredPacketsByType.TryGetValue(packet.GetType(), out info))
                    throw new ArgumentException($"Packet type {packet.GetType()} wasn't registered");
            byte[] serialized;
            using (var stream = MyMemoryStream.CreateEmptyStream(8192))
            {
                stream.Write(info.PacketID);
                packet.WriteTo(stream);
                serialized = m_bufferPool.GetOrCreate(stream.WriteHead);
                Array.Copy(stream.Backing, 0, serialized, 0, stream.WriteHead);
            }
            return serialized;
        }

        private void MessageHandler(byte[] data)
        {
            if (data.Length < 8) return;
            var stream = MyMemoryStream.CreateReaderFor(data);
            var packetID = stream.ReadUInt64();
            MyPacketInfo info;
            using (m_packetDbLock.AcquireSharedUsing())
                if (!m_registeredPacketsByID.TryGetValue(packetID, out info))
                {
                    Log(MyLogSeverity.Warning, "Unknown packet ID {0}", packetID);
                    return;
                }
            try
            {
                var packet = info.Activator.Invoke();
                packet.ReadFrom(stream);
                info.Handler.Invoke(packet);
                ReturnPacket(packet);
            }
            catch (Exception e)
            {
                Log(MyLogSeverity.Critical, "Failed to parse packet of type {0}. Error:\n{1}", info.Type, e);
            }
        }
    }
}
