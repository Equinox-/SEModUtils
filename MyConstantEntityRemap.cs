using System.Collections.Generic;
using VRage.ModAPI;

namespace Equinox.Utils
{
    public class MyConstantEntityRemap : IMyRemapHelper
    {
        private readonly IReadOnlyDictionary<long, long> m_map;

        public MyConstantEntityRemap(IReadOnlyDictionary<long, long> map)
        {
            m_map = map;
        }

        public long RemapEntityId(long oldEntityId)
        {
            long result;
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (m_map.TryGetValue(oldEntityId, out result))
                return result;
            return oldEntityId;
        }

        public int RemapGroupId(string @group, int oldValue)
        {
            return oldValue;
        }

        public void Clear()
        {
        }
    }
}
