using System;
using System.Collections.Generic;
using Equinox.Utils.Command;
using Equinox.Utils.Logging;
using Equinox.Utils.Network;
using Sandbox.ModAPI;

namespace Equinox.Utils.Session
{
    public class ModSessionComponentRegistry
    {
        static ModSessionComponentRegistry()
        {
            ModSessionComponentRegistryGen.Register();
        }

        public delegate bool ObjectBuilderTypeTester(Ob_ModSessionComponent component);

        public delegate Ob_ModSessionComponent SerializeFromXml(string data);

        public delegate Ob_ModSessionComponent SerializeFromBinary(byte[] data);

        public delegate ModSessionComponent CreateComponent();

        public class ModSessionComponentDescriptor
        {
            public readonly ulong TypeKey;
            public readonly Type ComponentType;
            public readonly Type ObjectBuilderType;
            public readonly ObjectBuilderTypeTester Tester;
            public readonly CreateComponent Activator;

            public readonly SerializeFromXml SerializeFromXml;
            public readonly SerializeFromBinary SerializeFromBinary;

            public ModSessionComponentDescriptor(Type componentType, Type objectBuilderType, ObjectBuilderTypeTester typeTester, CreateComponent activator, SerializeFromXml serializeFromXml, SerializeFromBinary serializeFromBinary)
            {
                TypeKey = componentType.FullName.Hash64();
                ComponentType = componentType;
                ObjectBuilderType = objectBuilderType;
                Tester = typeTester;
                Activator = activator;
                SerializeFromXml = serializeFromXml;
                SerializeFromBinary = serializeFromBinary;
            }
        }

        private static readonly Dictionary<Type, ModSessionComponentDescriptor> m_descByType = new Dictionary<Type, ModSessionComponentDescriptor>();
        private static readonly Dictionary<ulong, ModSessionComponentDescriptor> m_descByKey = new Dictionary<ulong, ModSessionComponentDescriptor>();

        public static void Register<TComponent, TObject>() where TComponent : ModSessionComponent, new()
            where TObject : Ob_ModSessionComponent
        {
            var desc = new ModSessionComponentDescriptor(typeof(TComponent), typeof(TObject), (x) => x is TObject, () => new TComponent(),
                (x)=>MyAPIGateway.Utilities.SerializeFromXML<TObject>(x),
                (x)=>MyAPIGateway.Utilities.SerializeFromBinary<TObject>(x));
            m_descByType.Add(desc.ComponentType, desc);
            m_descByType.Add(desc.ObjectBuilderType, desc);
            m_descByKey.Add(desc.TypeKey, desc);
        }

        public static ModSessionComponentDescriptor Get(ulong key)
        {
            return m_descByKey[key];
        }

        public static ModSessionComponentDescriptor Get(Type t)
        {
            return m_descByType[t];
        }

        public static ModSessionComponentDescriptor Get(Ob_ModSessionComponent t)
        {
            ModSessionComponentDescriptor res;
            if (m_descByType.TryGetValue(t.GetType(), out res))
                return res;
            res = null;
            foreach (var v in m_descByKey.Values)
                if (v.Tester.Invoke(t))
                {
                    res = v;
                    break;
                }
            if (res == null) throw new ArgumentException("Couldn't find session component for object builder of type " + t.GetType());
            return res;
        }
    }
}
