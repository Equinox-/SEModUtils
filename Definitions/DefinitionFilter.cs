using System.Collections.Generic;
using System.Linq;
using VRage.Game;
using VRage.ObjectBuilders;

namespace Equinox.Utils.Definitions
{
    public delegate bool DefinitionTester(MyDefinitionBase def);

    public class DefinitionFilter
    {
        private readonly HashSet<MyObjectBuilderType> m_types = new HashSet<MyObjectBuilderType>(MyObjectBuilderType.Comparer);
        private readonly Dictionary<MyObjectBuilderType, HashSet<string>> m_typeAndSubtype = new Dictionary<MyObjectBuilderType, HashSet<string>>(MyObjectBuilderType.Comparer);
        private readonly Dictionary<MyObjectBuilderType, List<DefinitionTester>> m_typeAndTester = new Dictionary<MyObjectBuilderType, List<DefinitionTester>>(MyObjectBuilderType.Comparer);

        public DefinitionFilter Append(DefinitionFilter other)
        {
            foreach (var k in other.m_types)
                OrType(k);
            foreach (var kv in other.m_typeAndSubtype)
                foreach (var s in kv.Value)
                    OrTypeSubtype(kv.Key, s);
            foreach (var kv in other.m_typeAndTester)
                foreach (var s in kv.Value)
                    OrTypeTester(kv.Key, s);
            return this;
        }

        public DefinitionFilter OrType(params MyObjectBuilderType[] types)
        {
            foreach (var type in types)
            {
                m_types.Add(type);
                m_typeAndSubtype.Remove(type);
                m_typeAndTester.Remove(type);
            }
            return this;
        }

        public DefinitionFilter OrTypeSubtype(MyObjectBuilderType type, params string[] subtypes)
        {
            if (m_types.Contains(type)) return this;
            HashSet<string> set;
            if (!m_typeAndSubtype.TryGetValue(type, out set))
                m_typeAndSubtype[type] = set = new HashSet<string>();
            foreach (var s in subtypes)
                set.Add(s);
            return this;
        }

        public DefinitionFilter OrTypeTester(MyObjectBuilderType type, DefinitionTester tester)
        {
            if (m_types.Contains(type)) return this;
            List<DefinitionTester> set;
            if (!m_typeAndTester.TryGetValue(type, out set))
                m_typeAndTester[type] = set = new List<DefinitionTester>(1);
            set.Add(tester);
            return this;
        }

        public bool Test(MyDefinitionBase b)
        {
            if (m_types.Contains(b.Id.TypeId)) return true;
            HashSet<string> subtypes;
            if (m_typeAndSubtype.TryGetValue(b.Id.TypeId, out subtypes))
                if (subtypes.Contains(b.Id.SubtypeName))
                    return true;
            List<DefinitionTester> testers;
            return m_typeAndTester.TryGetValue(b.Id.TypeId, out testers) && testers.Any(test => test(b));
        }

        public static implicit operator DefinitionTester(DefinitionFilter filter)
        {
            return filter.Test;
        }
    }
}
