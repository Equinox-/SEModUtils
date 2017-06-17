using VRageMath;

namespace Equinox.Utils
{
    public struct Vector3I_SurfaceIterator
    {
        private Vector3I Min;
        private Vector3I Max;
        public Vector3I Current { get; private set; }

        private Vector3I m_first, m_second, m_faceVec;
        private Base6Directions.Direction m_faceInternal;

        private Base6Directions.Direction m_face
        {
            get { return m_faceInternal; }
            set
            {
                m_faceInternal = value;
                if ((int)value >= 6) return;
                m_faceVec = Base6Directions.GetIntVector(value);
                var ax = Base6Directions.GetPerpendicular(value);
                var ax2 = Base6Directions.GetLeft(value, ax);
                m_first = Vector3I.Abs(Base6Directions.GetIntVector(ax));
                m_second = Vector3I.Abs(Base6Directions.GetIntVector(ax2));
            }
        }

        /// <summary>
        /// Note.  Both min and max are inclusive.
        /// </summary>
        public Vector3I_SurfaceIterator(Vector3I min, Vector3I max)
        {
            Min = min;
            Max = max;
            Current = min;
            // Init struct completely
            m_faceVec = m_first = m_second = Vector3I.Zero;
            m_faceInternal = 0;
            m_face = 0;
        }

        public bool IsValid()
        {
            return (int)m_face < 6 && Max.X >= Min.X && Max.Y >= Min.Y && Max.Z >= Min.Z;
        }

        public void MoveNext()
        {
            Current += m_first;
            if (Current.Dot(ref m_first) <= Max.Dot(ref m_first)) return;
            Current = Current * (Vector3I.One - m_first) + (Min) * m_first + m_second;
            if (Current.Dot(ref m_second) <= Max.Dot(ref m_second)) return;
            // Modify min/max to remove the face we just processed
            if (m_faceVec.Dot(ref Vector3I.One) > 0)
                Max -= m_faceVec;
            else
                Min -= m_faceVec; // sign is negated, so subtract=add
            m_face++;
            var faceSizeMult = (m_faceVec + 1) / 2;
            Current = Min + faceSizeMult * (Max - Min);
        }
    }
}
