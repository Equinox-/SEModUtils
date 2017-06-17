using System;
using System.Collections.Generic;
using Equinox.Utils.Command;
using Equinox.Utils.Logging;
using Equinox.Utils.Network;
using Sandbox.ModAPI;

namespace Equinox.Utils.Session
{
    public class MyModSessionComponentRegistry
    {
        static MyModSessionComponentRegistry()
        {
            MyModSessionComponentRegistryGen.Register();
        }

        public delegate bool ObjectBuilderTypeTester(MyObjectBuilder_ModSessionComponent component);

        public delegate MyObjectBuilder_ModSessionComponent SerializeFromXml(string data);

        public delegate MyObjectBuilder_ModSessionComponent SerializeFromBinary(byte[] data);

        public delegate MyModSessionComponent CreateComponent();

        public class MyModSessionComponentDescriptor
        {
            public readonly ulong TypeKey;
            public readonly Type ComponentType;
            public readonly Type ObjectBuilderType;
            public readonly ObjectBuilderTypeTester Tester;
            public readonly CreateComponent Activator;

            public readonly SerializeFromXml SerializeFromXml;
            public readonly SerializeFromBinary SerializeFromBinary;

            public MyModSessionComponentDescriptor(Type componentType, Type objectBuilderType, ObjectBuilderTypeTester typeTester, CreateComponent activator, SerializeFromXml serializeFromXml, SerializeFromBinary serializeFromBinary)
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

        private static readonly Dictionary<Type, MyModSessionComponentDescriptor> m_descByType = new Dictionary<Type, MyModSessionComponentDescriptor>();
        private static readonly Dictionary<ulong, MyModSessionComponentDescriptor> m_descByKey = new Dictionary<ulong, MyModSessionComponentDescriptor>();

        public static void Register<TComponent, TObject>() where TComponent : MyModSessionComponent, new()
            where TObject : MyObjectBuilder_ModSessionComponent
        {
            var desc = new MyModSessionComponentDescriptor(typeof(TComponent), typeof(TObject), (x) => x is TObject, () => new TComponent(),
                (x)=>MyAPIGateway.Utilities.SerializeFromXML<TObject>(x),
                (x)=>MyAPIGateway.Utilities.SerializeFromBinary<TObject>(x));
            m_descByType.Add(desc.ComponentType, desc);
            m_descByType.Add(desc.ObjectBuilderType, desc);
            m_descByKey.Add(desc.TypeKey, desc);
        }

        public static MyModSessionComponentDescriptor Get(ulong key)
        {
            return m_descByKey[key];
        }

        public static MyModSessionComponentDescriptor Get(Type t)
        {
            return m_descByType[t];
        }

        public static MyModSessionComponentDescriptor Get(MyObjectBuilder_ModSessionComponent t)
        {
            MyModSessionComponentDescriptor res;
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
