using System.Collections.Generic;
using VRage;
using VRageMath;

namespace Equinox.Utils
{
    public class TupleEqualityComparer<TK1, TK2> : IEqualityComparer<MyTuple<TK1, TK2>>
    {
        private readonly IEqualityComparer<TK1> m_a;
        private readonly IEqualityComparer<TK2> m_b;

        public TupleEqualityComparer(IEqualityComparer<TK1> a, IEqualityComparer<TK2> b)
        {
            m_a = a ?? EqualityComparer<TK1>.Default;
            m_b = b ?? EqualityComparer<TK2>.Default;
        }

        public bool Equals(MyTuple<TK1, TK2> x, MyTuple<TK1, TK2> y)
        {
            return m_a.Equals(x.Item1, y.Item1) && m_b.Equals(x.Item2, y.Item2);
        }

        public int GetHashCode(MyTuple<TK1, TK2> obj)
        {
            return obj.GetHashCode();
        }
    }

    public class MatrixIEqualityComparer : IEqualityComparer<MatrixI>
    {
        public static readonly MatrixIEqualityComparer Instance = new MatrixIEqualityComparer();

        public bool Equals(MatrixI x, MatrixI y)
        {
            return x.Backward == y.Backward && x.Right == y.Right && x.Up == y.Up && x.Translation == y.Translation;
        }

        public int GetHashCode(MatrixI obj)
        {
            return obj.GetHashCode();
        }
    }
}
