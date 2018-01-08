using System.Linq;

namespace Equinox.Utils.Collections
{
    public abstract class MovingAverageBase<T>
    {
        protected int m_head;
        protected T[] m_ring;
        public T Average { get; protected set; }
        public T Minimum => m_ring.Min();
        public T Maximum => m_ring.Max();

        protected MovingAverageBase(int length)
        {
            m_head = 0;
            m_ring = new T[length];
        }

        public void Resize(int length)
        {
            m_ring = new T[length];
        }

        public virtual void Insert(T value)
        {
            m_ring[m_head] = value;
            m_head = (m_head + 1) % m_ring.Length;
        }
    }


    public class MovingAverageInt64 : MovingAverageBase<long>
    {
        public MovingAverageInt64(int length) : base(length)
        {
        }

        public override void Insert(long value)
        {
            // ReSharper disable once PossibleLossOfFraction
            Average += (value - m_ring[m_head]) / m_ring.Length;
            base.Insert(value);
        }
    }

    public class MovingAverageDouble : MovingAverageBase<double>
    {
        public MovingAverageDouble(int length) : base(length)
        {
        }

        public override void Insert(double value)
        {
            Average += (value - m_ring[m_head]) / m_ring.Length;
            base.Insert(value);
        }
    }
}