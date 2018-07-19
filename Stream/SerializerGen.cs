using Equinox.Utils.Stream;
namespace Equinox.Utils.Stream {
    public static class MemoryStreamExtensions {
        public static void Write(this MemoryStream stream, ref VRageMath.ContainmentType val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.ContainmentType val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.ContainmentType) tmp;
        }
        public static void Write(this MemoryStream stream, ref VRageMath.CubeFace val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.CubeFace val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.CubeFace) tmp;
        }
        public static void Write(this MemoryStream stream, ref VRageMath.CurveContinuity val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.CurveContinuity val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.CurveContinuity) tmp;
        }
        public static void Write(this MemoryStream stream, ref VRageMath.CurveLoopType val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.CurveLoopType val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.CurveLoopType) tmp;
        }
        public static void Write(this MemoryStream stream, ref VRageMath.CurveTangent val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.CurveTangent val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.CurveTangent) tmp;
        }
        public static void Write(this MemoryStream stream, ref VRageMath.PlaneIntersectionType val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.PlaneIntersectionType val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.PlaneIntersectionType) tmp;
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Base27Directions.Direction val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Base27Directions.Direction val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.Base27Directions.Direction) tmp;
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Base6Directions.Direction val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Base6Directions.Direction val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.Base6Directions.Direction) tmp;
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Base6Directions.DirectionFlags val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Base6Directions.DirectionFlags val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.Base6Directions.DirectionFlags) tmp;
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Base6Directions.Axis val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Base6Directions.Axis val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.Base6Directions.Axis) tmp;
        }
        public static void Write(this MemoryStream stream, ref VRageMath.BoundingBox2I val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.BoundingBox2I val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.BoundingBox2D val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.BoundingBox2D val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.BoundingBox2 val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.BoundingBox2 val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.BoundingBoxI val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.BoundingBoxI val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.BoundingBox val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.BoundingBox val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.BoundingSphere val)
		{
			stream.Write(ref val.Center);
			stream.Write(ref val.Radius);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.BoundingSphere val)
		{
			stream.Read(ref val.Center);
			stream.Read(ref val.Radius);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.BoundingBoxD val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.BoundingBoxD val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.BoundingSphereD val)
		{
			stream.Write(ref val.Center);
			stream.Write(ref val.Radius);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.BoundingSphereD val)
		{
			stream.Read(ref val.Center);
			stream.Read(ref val.Radius);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.CapsuleD val)
		{
			stream.Write(ref val.P0);
			stream.Write(ref val.P1);
			stream.Write(ref val.Radius);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.CapsuleD val)
		{
			stream.Read(ref val.P0);
			stream.Read(ref val.P1);
			stream.Read(ref val.Radius);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.MyTransformD val)
		{
			stream.Write(ref val.Rotation);
			stream.Write(ref val.Position);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.MyTransformD val)
		{
			stream.Read(ref val.Rotation);
			stream.Read(ref val.Position);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.QuaternionD val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.QuaternionD val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.SerializableRange val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.SerializableRange val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.SymmetricSerializableRange val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.SymmetricSerializableRange val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector2D val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector2D val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector4D val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector4D val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.MyOrientedBoundingBoxD val)
		{
			stream.Write(ref val.Center);
			stream.Write(ref val.HalfExtent);
			stream.Write(ref val.Orientation);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.MyOrientedBoundingBoxD val)
		{
			stream.Read(ref val.Center);
			stream.Read(ref val.HalfExtent);
			stream.Read(ref val.Orientation);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.PlaneD val)
		{
			stream.Write(ref val.Normal);
			stream.Write(ref val.D);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.PlaneD val)
		{
			stream.Read(ref val.Normal);
			stream.Read(ref val.D);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.LineD val)
		{
			stream.Write(ref val.From);
			stream.Write(ref val.To);
			stream.Write(ref val.Direction);
			stream.Write(ref val.Length);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.LineD val)
		{
			stream.Read(ref val.From);
			stream.Read(ref val.To);
			stream.Read(ref val.Direction);
			stream.Read(ref val.Length);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.RayD val)
		{
			stream.Write(ref val.Position);
			stream.Write(ref val.Direction);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.RayD val)
		{
			stream.Read(ref val.Position);
			stream.Read(ref val.Direction);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.MatrixD val)
		{
			stream.Write(ref val.M11);
			stream.Write(ref val.M12);
			stream.Write(ref val.M13);
			stream.Write(ref val.M14);
			stream.Write(ref val.M21);
			stream.Write(ref val.M22);
			stream.Write(ref val.M23);
			stream.Write(ref val.M24);
			stream.Write(ref val.M31);
			stream.Write(ref val.M32);
			stream.Write(ref val.M33);
			stream.Write(ref val.M34);
			stream.Write(ref val.M41);
			stream.Write(ref val.M42);
			stream.Write(ref val.M43);
			stream.Write(ref val.M44);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.MatrixD val)
		{
			stream.Read(ref val.M11);
			stream.Read(ref val.M12);
			stream.Read(ref val.M13);
			stream.Read(ref val.M14);
			stream.Read(ref val.M21);
			stream.Read(ref val.M22);
			stream.Read(ref val.M23);
			stream.Read(ref val.M24);
			stream.Read(ref val.M31);
			stream.Read(ref val.M32);
			stream.Read(ref val.M33);
			stream.Read(ref val.M34);
			stream.Read(ref val.M41);
			stream.Read(ref val.M42);
			stream.Read(ref val.M43);
			stream.Read(ref val.M44);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector3D val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector3D val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.MatrixI val)
		{
			stream.Write(ref val.Right);
			stream.Write(ref val.Up);
			stream.Write(ref val.Backward);
			stream.Write(ref val.Translation);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.MatrixI val)
		{
			stream.Read(ref val.Right);
			stream.Read(ref val.Up);
			stream.Read(ref val.Backward);
			stream.Read(ref val.Translation);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.MyQuad val)
		{
			stream.Write(ref val.Point0);
			stream.Write(ref val.Point1);
			stream.Write(ref val.Point2);
			stream.Write(ref val.Point3);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.MyQuad val)
		{
			stream.Read(ref val.Point0);
			stream.Read(ref val.Point1);
			stream.Read(ref val.Point2);
			stream.Read(ref val.Point3);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.MyQuadD val)
		{
			stream.Write(ref val.Point0);
			stream.Write(ref val.Point1);
			stream.Write(ref val.Point2);
			stream.Write(ref val.Point3);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.MyQuadD val)
		{
			stream.Read(ref val.Point0);
			stream.Read(ref val.Point1);
			stream.Read(ref val.Point2);
			stream.Read(ref val.Point3);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.MyShort4 val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.MyShort4 val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Color val)
		{
			stream.Write(ref val.PackedValue);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Color val)
		{
			stream.Read(ref val.PackedValue);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Capsule val)
		{
			stream.Write(ref val.P0);
			stream.Write(ref val.P1);
			stream.Write(ref val.Radius);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Capsule val)
		{
			stream.Read(ref val.P0);
			stream.Read(ref val.P1);
			stream.Read(ref val.Radius);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.MyBlockOrientation val)
		{
			stream.Write(ref val.Forward);
			stream.Write(ref val.Up);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.MyBlockOrientation val)
		{
			stream.Read(ref val.Forward);
			stream.Read(ref val.Up);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.MyBounds val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
			stream.Write(ref val.Default);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.MyBounds val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
			stream.Read(ref val.Default);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.MyOrientedBoundingBox val)
		{
			stream.Write(ref val.Center);
			stream.Write(ref val.HalfExtent);
			stream.Write(ref val.Orientation);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.MyOrientedBoundingBox val)
		{
			stream.Read(ref val.Center);
			stream.Read(ref val.HalfExtent);
			stream.Read(ref val.Orientation);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.MyTransform val)
		{
			stream.Write(ref val.Rotation);
			stream.Write(ref val.Position);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.MyTransform val)
		{
			stream.Read(ref val.Rotation);
			stream.Read(ref val.Position);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.MyUShort4 val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.MyUShort4 val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector3B val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector3B val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector3S val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector3S val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector2I val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector2I val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector3UByte val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector3UByte val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector3Ushort val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector3Ushort val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector4I val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector4I val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector3I_RangeIterator val)
		{
			stream.Write(ref val.Current);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector3I_RangeIterator val)
		{
			stream.Read(ref val.Current);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector3I val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector3I val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Line val)
		{
			stream.Write(ref val.From);
			stream.Write(ref val.To);
			stream.Write(ref val.Direction);
			stream.Write(ref val.Length);
			stream.Write(ref val.BoundingBox);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Line val)
		{
			stream.Read(ref val.From);
			stream.Read(ref val.To);
			stream.Read(ref val.Direction);
			stream.Read(ref val.Length);
			stream.Read(ref val.BoundingBox);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Matrix val)
		{
			stream.Write(ref val.M11);
			stream.Write(ref val.M12);
			stream.Write(ref val.M13);
			stream.Write(ref val.M14);
			stream.Write(ref val.M21);
			stream.Write(ref val.M22);
			stream.Write(ref val.M23);
			stream.Write(ref val.M24);
			stream.Write(ref val.M31);
			stream.Write(ref val.M32);
			stream.Write(ref val.M33);
			stream.Write(ref val.M34);
			stream.Write(ref val.M41);
			stream.Write(ref val.M42);
			stream.Write(ref val.M43);
			stream.Write(ref val.M44);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Matrix val)
		{
			stream.Read(ref val.M11);
			stream.Read(ref val.M12);
			stream.Read(ref val.M13);
			stream.Read(ref val.M14);
			stream.Read(ref val.M21);
			stream.Read(ref val.M22);
			stream.Read(ref val.M23);
			stream.Read(ref val.M24);
			stream.Read(ref val.M31);
			stream.Read(ref val.M32);
			stream.Read(ref val.M33);
			stream.Read(ref val.M34);
			stream.Read(ref val.M41);
			stream.Read(ref val.M42);
			stream.Read(ref val.M43);
			stream.Read(ref val.M44);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Plane val)
		{
			stream.Write(ref val.Normal);
			stream.Write(ref val.D);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Plane val)
		{
			stream.Read(ref val.Normal);
			stream.Read(ref val.D);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Point val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Point val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Quaternion val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Quaternion val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Ray val)
		{
			stream.Write(ref val.Position);
			stream.Write(ref val.Direction);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Ray val)
		{
			stream.Read(ref val.Position);
			stream.Read(ref val.Direction);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Rectangle val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Width);
			stream.Write(ref val.Height);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Rectangle val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Width);
			stream.Read(ref val.Height);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.RectangleF val)
		{
			stream.Write(ref val.Position);
			stream.Write(ref val.Size);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.RectangleF val)
		{
			stream.Read(ref val.Position);
			stream.Read(ref val.Size);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector2 val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector2 val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector3 val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector3 val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector4 val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector4 val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MemoryStream stream, ref VRageMath.Vector4UByte val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MemoryStream stream, ref VRageMath.Vector4UByte val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MemoryStream stream, byte val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, sbyte val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, short val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, ushort val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, int val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, uint val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, long val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, ulong val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, float val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, double val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.ContainmentType val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.CubeFace val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.CurveContinuity val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.CurveLoopType val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.CurveTangent val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.PlaneIntersectionType val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Base27Directions.Direction val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Base6Directions.Direction val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Base6Directions.DirectionFlags val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Base6Directions.Axis val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.BoundingBox2I val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.BoundingBox2D val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.BoundingBox2 val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.BoundingBoxI val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.BoundingBox val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.BoundingSphere val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.BoundingBoxD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.BoundingSphereD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.CapsuleD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.MyTransformD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.QuaternionD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.SerializableRange val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.SymmetricSerializableRange val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector2D val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector4D val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.MyOrientedBoundingBoxD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.PlaneD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.LineD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.RayD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.MatrixD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector3D val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.MatrixI val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.MyQuad val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.MyQuadD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.MyShort4 val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Color val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Capsule val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.MyBlockOrientation val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.MyBounds val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.MyOrientedBoundingBox val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.MyTransform val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.MyUShort4 val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector3B val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector3S val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector2I val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector3UByte val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector3Ushort val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector4I val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector3I_RangeIterator val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector3I val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Line val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Matrix val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Plane val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Point val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Quaternion val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Ray val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Rectangle val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.RectangleF val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector2 val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector3 val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector4 val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MemoryStream stream, VRageMath.Vector4UByte val)
		{
            stream.Write(ref val);
        }
        public static byte ReadByte(this MemoryStream stream)
		{
            var val = default(byte);
            stream.Read(ref val);
            return val;
        }
        public static sbyte ReadSByte(this MemoryStream stream)
		{
            var val = default(sbyte);
            stream.Read(ref val);
            return val;
        }
        public static short ReadInt16(this MemoryStream stream)
		{
            var val = default(short);
            stream.Read(ref val);
            return val;
        }
        public static ushort ReadUInt16(this MemoryStream stream)
		{
            var val = default(ushort);
            stream.Read(ref val);
            return val;
        }
        public static int ReadInt32(this MemoryStream stream)
		{
            var val = default(int);
            stream.Read(ref val);
            return val;
        }
        public static uint ReadUInt32(this MemoryStream stream)
		{
            var val = default(uint);
            stream.Read(ref val);
            return val;
        }
        public static long ReadInt64(this MemoryStream stream)
		{
            var val = default(long);
            stream.Read(ref val);
            return val;
        }
        public static ulong ReadUInt64(this MemoryStream stream)
		{
            var val = default(ulong);
            stream.Read(ref val);
            return val;
        }
        public static float ReadSingle(this MemoryStream stream)
		{
            var val = default(float);
            stream.Read(ref val);
            return val;
        }
        public static double ReadDouble(this MemoryStream stream)
		{
            var val = default(double);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.ContainmentType ReadContainmentType(this MemoryStream stream)
		{
            var val = default(VRageMath.ContainmentType);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.CubeFace ReadCubeFace(this MemoryStream stream)
		{
            var val = default(VRageMath.CubeFace);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.CurveContinuity ReadCurveContinuity(this MemoryStream stream)
		{
            var val = default(VRageMath.CurveContinuity);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.CurveLoopType ReadCurveLoopType(this MemoryStream stream)
		{
            var val = default(VRageMath.CurveLoopType);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.CurveTangent ReadCurveTangent(this MemoryStream stream)
		{
            var val = default(VRageMath.CurveTangent);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.PlaneIntersectionType ReadPlaneIntersectionType(this MemoryStream stream)
		{
            var val = default(VRageMath.PlaneIntersectionType);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Base27Directions.Direction ReadBase27Direction(this MemoryStream stream)
		{
            var val = default(VRageMath.Base27Directions.Direction);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Base6Directions.Direction ReadBase6Direction(this MemoryStream stream)
		{
            var val = default(VRageMath.Base6Directions.Direction);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Base6Directions.DirectionFlags ReadBase6DirectionFlags(this MemoryStream stream)
		{
            var val = default(VRageMath.Base6Directions.DirectionFlags);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Base6Directions.Axis ReadBase6Axis(this MemoryStream stream)
		{
            var val = default(VRageMath.Base6Directions.Axis);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingBox2I ReadBoundingBox2I(this MemoryStream stream)
		{
            var val = default(VRageMath.BoundingBox2I);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingBox2D ReadBoundingBox2D(this MemoryStream stream)
		{
            var val = default(VRageMath.BoundingBox2D);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingBox2 ReadBoundingBox2(this MemoryStream stream)
		{
            var val = default(VRageMath.BoundingBox2);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingBoxI ReadBoundingBoxI(this MemoryStream stream)
		{
            var val = default(VRageMath.BoundingBoxI);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingBox ReadBoundingBox(this MemoryStream stream)
		{
            var val = default(VRageMath.BoundingBox);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingSphere ReadBoundingSphere(this MemoryStream stream)
		{
            var val = default(VRageMath.BoundingSphere);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingBoxD ReadBoundingBoxD(this MemoryStream stream)
		{
            var val = default(VRageMath.BoundingBoxD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingSphereD ReadBoundingSphereD(this MemoryStream stream)
		{
            var val = default(VRageMath.BoundingSphereD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.CapsuleD ReadCapsuleD(this MemoryStream stream)
		{
            var val = default(VRageMath.CapsuleD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyTransformD ReadMyTransformD(this MemoryStream stream)
		{
            var val = default(VRageMath.MyTransformD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.QuaternionD ReadQuaternionD(this MemoryStream stream)
		{
            var val = default(VRageMath.QuaternionD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.SerializableRange ReadSerializableRange(this MemoryStream stream)
		{
            var val = default(VRageMath.SerializableRange);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.SymmetricSerializableRange ReadSymmetricSerializableRange(this MemoryStream stream)
		{
            var val = default(VRageMath.SymmetricSerializableRange);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector2D ReadVector2D(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector2D);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector4D ReadVector4D(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector4D);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyOrientedBoundingBoxD ReadMyOrientedBoundingBoxD(this MemoryStream stream)
		{
            var val = default(VRageMath.MyOrientedBoundingBoxD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.PlaneD ReadPlaneD(this MemoryStream stream)
		{
            var val = default(VRageMath.PlaneD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.LineD ReadLineD(this MemoryStream stream)
		{
            var val = default(VRageMath.LineD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.RayD ReadRayD(this MemoryStream stream)
		{
            var val = default(VRageMath.RayD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MatrixD ReadMatrixD(this MemoryStream stream)
		{
            var val = default(VRageMath.MatrixD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3D ReadVector3D(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector3D);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MatrixI ReadMatrixI(this MemoryStream stream)
		{
            var val = default(VRageMath.MatrixI);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyQuad ReadMyQuad(this MemoryStream stream)
		{
            var val = default(VRageMath.MyQuad);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyQuadD ReadMyQuadD(this MemoryStream stream)
		{
            var val = default(VRageMath.MyQuadD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyShort4 ReadMyShort4(this MemoryStream stream)
		{
            var val = default(VRageMath.MyShort4);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Color ReadColor(this MemoryStream stream)
		{
            var val = default(VRageMath.Color);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Capsule ReadCapsule(this MemoryStream stream)
		{
            var val = default(VRageMath.Capsule);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyBlockOrientation ReadMyBlockOrientation(this MemoryStream stream)
		{
            var val = default(VRageMath.MyBlockOrientation);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyBounds ReadMyBounds(this MemoryStream stream)
		{
            var val = default(VRageMath.MyBounds);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyOrientedBoundingBox ReadMyOrientedBoundingBox(this MemoryStream stream)
		{
            var val = default(VRageMath.MyOrientedBoundingBox);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyTransform ReadMyTransform(this MemoryStream stream)
		{
            var val = default(VRageMath.MyTransform);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyUShort4 ReadMyUShort4(this MemoryStream stream)
		{
            var val = default(VRageMath.MyUShort4);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3B ReadVector3B(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector3B);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3S ReadVector3S(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector3S);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector2I ReadVector2I(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector2I);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3UByte ReadVector3UByte(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector3UByte);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3Ushort ReadVector3Ushort(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector3Ushort);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector4I ReadVector4I(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector4I);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3I_RangeIterator ReadVector3I_RangeIterator(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector3I_RangeIterator);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3I ReadVector3I(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector3I);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Line ReadLine(this MemoryStream stream)
		{
            var val = default(VRageMath.Line);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Matrix ReadMatrix(this MemoryStream stream)
		{
            var val = default(VRageMath.Matrix);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Plane ReadPlane(this MemoryStream stream)
		{
            var val = default(VRageMath.Plane);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Point ReadPoint(this MemoryStream stream)
		{
            var val = default(VRageMath.Point);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Quaternion ReadQuaternion(this MemoryStream stream)
		{
            var val = default(VRageMath.Quaternion);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Ray ReadRay(this MemoryStream stream)
		{
            var val = default(VRageMath.Ray);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Rectangle ReadRectangle(this MemoryStream stream)
		{
            var val = default(VRageMath.Rectangle);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.RectangleF ReadRectangleF(this MemoryStream stream)
		{
            var val = default(VRageMath.RectangleF);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector2 ReadVector2(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector2);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3 ReadVector3(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector3);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector4 ReadVector4(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector4);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector4UByte ReadVector4UByte(this MemoryStream stream)
		{
            var val = default(VRageMath.Vector4UByte);
            stream.Read(ref val);
            return val;
        }
    }
}

public static class SerializerExtensions {
    public class ByteSerializer : Serializer<byte> {
        public static readonly ByteSerializer Instance = new ByteSerializer();
        public override void Read(ref byte val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref byte val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class SbyteSerializer : Serializer<sbyte> {
        public static readonly SbyteSerializer Instance = new SbyteSerializer();
        public override void Read(ref sbyte val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref sbyte val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class ShortSerializer : Serializer<short> {
        public static readonly ShortSerializer Instance = new ShortSerializer();
        public override void Read(ref short val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref short val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class UshortSerializer : Serializer<ushort> {
        public static readonly UshortSerializer Instance = new UshortSerializer();
        public override void Read(ref ushort val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref ushort val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class IntSerializer : Serializer<int> {
        public static readonly IntSerializer Instance = new IntSerializer();
        public override void Read(ref int val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref int val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class UintSerializer : Serializer<uint> {
        public static readonly UintSerializer Instance = new UintSerializer();
        public override void Read(ref uint val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref uint val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class LongSerializer : Serializer<long> {
        public static readonly LongSerializer Instance = new LongSerializer();
        public override void Read(ref long val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref long val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class UlongSerializer : Serializer<ulong> {
        public static readonly UlongSerializer Instance = new UlongSerializer();
        public override void Read(ref ulong val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref ulong val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class FloatSerializer : Serializer<float> {
        public static readonly FloatSerializer Instance = new FloatSerializer();
        public override void Read(ref float val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref float val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class DoubleSerializer : Serializer<double> {
        public static readonly DoubleSerializer Instance = new DoubleSerializer();
        public override void Read(ref double val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref double val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class ContainmentTypeSerializer : Serializer<VRageMath.ContainmentType> {
        public static readonly ContainmentTypeSerializer Instance = new ContainmentTypeSerializer();
        public override void Read(ref VRageMath.ContainmentType val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.ContainmentType val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class CubeFaceSerializer : Serializer<VRageMath.CubeFace> {
        public static readonly CubeFaceSerializer Instance = new CubeFaceSerializer();
        public override void Read(ref VRageMath.CubeFace val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.CubeFace val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class CurveContinuitySerializer : Serializer<VRageMath.CurveContinuity> {
        public static readonly CurveContinuitySerializer Instance = new CurveContinuitySerializer();
        public override void Read(ref VRageMath.CurveContinuity val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.CurveContinuity val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class CurveLoopTypeSerializer : Serializer<VRageMath.CurveLoopType> {
        public static readonly CurveLoopTypeSerializer Instance = new CurveLoopTypeSerializer();
        public override void Read(ref VRageMath.CurveLoopType val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.CurveLoopType val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class CurveTangentSerializer : Serializer<VRageMath.CurveTangent> {
        public static readonly CurveTangentSerializer Instance = new CurveTangentSerializer();
        public override void Read(ref VRageMath.CurveTangent val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.CurveTangent val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class PlaneIntersectionTypeSerializer : Serializer<VRageMath.PlaneIntersectionType> {
        public static readonly PlaneIntersectionTypeSerializer Instance = new PlaneIntersectionTypeSerializer();
        public override void Read(ref VRageMath.PlaneIntersectionType val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.PlaneIntersectionType val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Base27DirectionSerializer : Serializer<VRageMath.Base27Directions.Direction> {
        public static readonly Base27DirectionSerializer Instance = new Base27DirectionSerializer();
        public override void Read(ref VRageMath.Base27Directions.Direction val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Base27Directions.Direction val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Base6DirectionSerializer : Serializer<VRageMath.Base6Directions.Direction> {
        public static readonly Base6DirectionSerializer Instance = new Base6DirectionSerializer();
        public override void Read(ref VRageMath.Base6Directions.Direction val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Base6Directions.Direction val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Base6DirectionFlagsSerializer : Serializer<VRageMath.Base6Directions.DirectionFlags> {
        public static readonly Base6DirectionFlagsSerializer Instance = new Base6DirectionFlagsSerializer();
        public override void Read(ref VRageMath.Base6Directions.DirectionFlags val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Base6Directions.DirectionFlags val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Base6AxisSerializer : Serializer<VRageMath.Base6Directions.Axis> {
        public static readonly Base6AxisSerializer Instance = new Base6AxisSerializer();
        public override void Read(ref VRageMath.Base6Directions.Axis val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Base6Directions.Axis val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class BoundingBox2ISerializer : Serializer<VRageMath.BoundingBox2I> {
        public static readonly BoundingBox2ISerializer Instance = new BoundingBox2ISerializer();
        public override void Read(ref VRageMath.BoundingBox2I val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingBox2I val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class BoundingBox2DSerializer : Serializer<VRageMath.BoundingBox2D> {
        public static readonly BoundingBox2DSerializer Instance = new BoundingBox2DSerializer();
        public override void Read(ref VRageMath.BoundingBox2D val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingBox2D val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class BoundingBox2Serializer : Serializer<VRageMath.BoundingBox2> {
        public static readonly BoundingBox2Serializer Instance = new BoundingBox2Serializer();
        public override void Read(ref VRageMath.BoundingBox2 val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingBox2 val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class BoundingBoxISerializer : Serializer<VRageMath.BoundingBoxI> {
        public static readonly BoundingBoxISerializer Instance = new BoundingBoxISerializer();
        public override void Read(ref VRageMath.BoundingBoxI val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingBoxI val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class BoundingBoxSerializer : Serializer<VRageMath.BoundingBox> {
        public static readonly BoundingBoxSerializer Instance = new BoundingBoxSerializer();
        public override void Read(ref VRageMath.BoundingBox val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingBox val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class BoundingSphereSerializer : Serializer<VRageMath.BoundingSphere> {
        public static readonly BoundingSphereSerializer Instance = new BoundingSphereSerializer();
        public override void Read(ref VRageMath.BoundingSphere val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingSphere val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class BoundingBoxDSerializer : Serializer<VRageMath.BoundingBoxD> {
        public static readonly BoundingBoxDSerializer Instance = new BoundingBoxDSerializer();
        public override void Read(ref VRageMath.BoundingBoxD val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingBoxD val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class BoundingSphereDSerializer : Serializer<VRageMath.BoundingSphereD> {
        public static readonly BoundingSphereDSerializer Instance = new BoundingSphereDSerializer();
        public override void Read(ref VRageMath.BoundingSphereD val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingSphereD val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class CapsuleDSerializer : Serializer<VRageMath.CapsuleD> {
        public static readonly CapsuleDSerializer Instance = new CapsuleDSerializer();
        public override void Read(ref VRageMath.CapsuleD val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.CapsuleD val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyTransformDSerializer : Serializer<VRageMath.MyTransformD> {
        public static readonly MyTransformDSerializer Instance = new MyTransformDSerializer();
        public override void Read(ref VRageMath.MyTransformD val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyTransformD val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class QuaternionDSerializer : Serializer<VRageMath.QuaternionD> {
        public static readonly QuaternionDSerializer Instance = new QuaternionDSerializer();
        public override void Read(ref VRageMath.QuaternionD val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.QuaternionD val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class SerializableRangeSerializer : Serializer<VRageMath.SerializableRange> {
        public static readonly SerializableRangeSerializer Instance = new SerializableRangeSerializer();
        public override void Read(ref VRageMath.SerializableRange val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.SerializableRange val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class SymmetricSerializableRangeSerializer : Serializer<VRageMath.SymmetricSerializableRange> {
        public static readonly SymmetricSerializableRangeSerializer Instance = new SymmetricSerializableRangeSerializer();
        public override void Read(ref VRageMath.SymmetricSerializableRange val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.SymmetricSerializableRange val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector2DSerializer : Serializer<VRageMath.Vector2D> {
        public static readonly Vector2DSerializer Instance = new Vector2DSerializer();
        public override void Read(ref VRageMath.Vector2D val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector2D val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector4DSerializer : Serializer<VRageMath.Vector4D> {
        public static readonly Vector4DSerializer Instance = new Vector4DSerializer();
        public override void Read(ref VRageMath.Vector4D val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector4D val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyOrientedBoundingBoxDSerializer : Serializer<VRageMath.MyOrientedBoundingBoxD> {
        public static readonly MyOrientedBoundingBoxDSerializer Instance = new MyOrientedBoundingBoxDSerializer();
        public override void Read(ref VRageMath.MyOrientedBoundingBoxD val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyOrientedBoundingBoxD val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class PlaneDSerializer : Serializer<VRageMath.PlaneD> {
        public static readonly PlaneDSerializer Instance = new PlaneDSerializer();
        public override void Read(ref VRageMath.PlaneD val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.PlaneD val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class LineDSerializer : Serializer<VRageMath.LineD> {
        public static readonly LineDSerializer Instance = new LineDSerializer();
        public override void Read(ref VRageMath.LineD val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.LineD val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class RayDSerializer : Serializer<VRageMath.RayD> {
        public static readonly RayDSerializer Instance = new RayDSerializer();
        public override void Read(ref VRageMath.RayD val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.RayD val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MatrixDSerializer : Serializer<VRageMath.MatrixD> {
        public static readonly MatrixDSerializer Instance = new MatrixDSerializer();
        public override void Read(ref VRageMath.MatrixD val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MatrixD val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector3DSerializer : Serializer<VRageMath.Vector3D> {
        public static readonly Vector3DSerializer Instance = new Vector3DSerializer();
        public override void Read(ref VRageMath.Vector3D val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3D val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MatrixISerializer : Serializer<VRageMath.MatrixI> {
        public static readonly MatrixISerializer Instance = new MatrixISerializer();
        public override void Read(ref VRageMath.MatrixI val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MatrixI val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyQuadSerializer : Serializer<VRageMath.MyQuad> {
        public static readonly MyQuadSerializer Instance = new MyQuadSerializer();
        public override void Read(ref VRageMath.MyQuad val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyQuad val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyQuadDSerializer : Serializer<VRageMath.MyQuadD> {
        public static readonly MyQuadDSerializer Instance = new MyQuadDSerializer();
        public override void Read(ref VRageMath.MyQuadD val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyQuadD val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyShort4Serializer : Serializer<VRageMath.MyShort4> {
        public static readonly MyShort4Serializer Instance = new MyShort4Serializer();
        public override void Read(ref VRageMath.MyShort4 val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyShort4 val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class ColorSerializer : Serializer<VRageMath.Color> {
        public static readonly ColorSerializer Instance = new ColorSerializer();
        public override void Read(ref VRageMath.Color val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Color val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class CapsuleSerializer : Serializer<VRageMath.Capsule> {
        public static readonly CapsuleSerializer Instance = new CapsuleSerializer();
        public override void Read(ref VRageMath.Capsule val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Capsule val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBlockOrientationSerializer : Serializer<VRageMath.MyBlockOrientation> {
        public static readonly MyBlockOrientationSerializer Instance = new MyBlockOrientationSerializer();
        public override void Read(ref VRageMath.MyBlockOrientation val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyBlockOrientation val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBoundsSerializer : Serializer<VRageMath.MyBounds> {
        public static readonly MyBoundsSerializer Instance = new MyBoundsSerializer();
        public override void Read(ref VRageMath.MyBounds val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyBounds val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyOrientedBoundingBoxSerializer : Serializer<VRageMath.MyOrientedBoundingBox> {
        public static readonly MyOrientedBoundingBoxSerializer Instance = new MyOrientedBoundingBoxSerializer();
        public override void Read(ref VRageMath.MyOrientedBoundingBox val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyOrientedBoundingBox val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyTransformSerializer : Serializer<VRageMath.MyTransform> {
        public static readonly MyTransformSerializer Instance = new MyTransformSerializer();
        public override void Read(ref VRageMath.MyTransform val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyTransform val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyUShort4Serializer : Serializer<VRageMath.MyUShort4> {
        public static readonly MyUShort4Serializer Instance = new MyUShort4Serializer();
        public override void Read(ref VRageMath.MyUShort4 val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyUShort4 val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector3BSerializer : Serializer<VRageMath.Vector3B> {
        public static readonly Vector3BSerializer Instance = new Vector3BSerializer();
        public override void Read(ref VRageMath.Vector3B val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3B val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector3SSerializer : Serializer<VRageMath.Vector3S> {
        public static readonly Vector3SSerializer Instance = new Vector3SSerializer();
        public override void Read(ref VRageMath.Vector3S val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3S val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector2ISerializer : Serializer<VRageMath.Vector2I> {
        public static readonly Vector2ISerializer Instance = new Vector2ISerializer();
        public override void Read(ref VRageMath.Vector2I val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector2I val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector3UByteSerializer : Serializer<VRageMath.Vector3UByte> {
        public static readonly Vector3UByteSerializer Instance = new Vector3UByteSerializer();
        public override void Read(ref VRageMath.Vector3UByte val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3UByte val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector3UshortSerializer : Serializer<VRageMath.Vector3Ushort> {
        public static readonly Vector3UshortSerializer Instance = new Vector3UshortSerializer();
        public override void Read(ref VRageMath.Vector3Ushort val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3Ushort val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector4ISerializer : Serializer<VRageMath.Vector4I> {
        public static readonly Vector4ISerializer Instance = new Vector4ISerializer();
        public override void Read(ref VRageMath.Vector4I val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector4I val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector3I_RangeIteratorSerializer : Serializer<VRageMath.Vector3I_RangeIterator> {
        public static readonly Vector3I_RangeIteratorSerializer Instance = new Vector3I_RangeIteratorSerializer();
        public override void Read(ref VRageMath.Vector3I_RangeIterator val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3I_RangeIterator val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector3ISerializer : Serializer<VRageMath.Vector3I> {
        public static readonly Vector3ISerializer Instance = new Vector3ISerializer();
        public override void Read(ref VRageMath.Vector3I val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3I val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class LineSerializer : Serializer<VRageMath.Line> {
        public static readonly LineSerializer Instance = new LineSerializer();
        public override void Read(ref VRageMath.Line val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Line val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MatrixSerializer : Serializer<VRageMath.Matrix> {
        public static readonly MatrixSerializer Instance = new MatrixSerializer();
        public override void Read(ref VRageMath.Matrix val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Matrix val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class PlaneSerializer : Serializer<VRageMath.Plane> {
        public static readonly PlaneSerializer Instance = new PlaneSerializer();
        public override void Read(ref VRageMath.Plane val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Plane val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class PointSerializer : Serializer<VRageMath.Point> {
        public static readonly PointSerializer Instance = new PointSerializer();
        public override void Read(ref VRageMath.Point val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Point val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class QuaternionSerializer : Serializer<VRageMath.Quaternion> {
        public static readonly QuaternionSerializer Instance = new QuaternionSerializer();
        public override void Read(ref VRageMath.Quaternion val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Quaternion val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class RaySerializer : Serializer<VRageMath.Ray> {
        public static readonly RaySerializer Instance = new RaySerializer();
        public override void Read(ref VRageMath.Ray val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Ray val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class RectangleSerializer : Serializer<VRageMath.Rectangle> {
        public static readonly RectangleSerializer Instance = new RectangleSerializer();
        public override void Read(ref VRageMath.Rectangle val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Rectangle val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class RectangleFSerializer : Serializer<VRageMath.RectangleF> {
        public static readonly RectangleFSerializer Instance = new RectangleFSerializer();
        public override void Read(ref VRageMath.RectangleF val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.RectangleF val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector2Serializer : Serializer<VRageMath.Vector2> {
        public static readonly Vector2Serializer Instance = new Vector2Serializer();
        public override void Read(ref VRageMath.Vector2 val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector2 val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector3Serializer : Serializer<VRageMath.Vector3> {
        public static readonly Vector3Serializer Instance = new Vector3Serializer();
        public override void Read(ref VRageMath.Vector3 val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3 val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector4Serializer : Serializer<VRageMath.Vector4> {
        public static readonly Vector4Serializer Instance = new Vector4Serializer();
        public override void Read(ref VRageMath.Vector4 val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector4 val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class Vector4UByteSerializer : Serializer<VRageMath.Vector4UByte> {
        public static readonly Vector4UByteSerializer Instance = new Vector4UByteSerializer();
        public override void Read(ref VRageMath.Vector4UByte val, MemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector4UByte val, MemoryStream stream)
		{
            stream.Write(ref val);
        }
    }

    public static void RegisterBuiltinTypes()
	{
            SerializerRegistry.RegisterSerializer(ByteSerializer.Instance);
            SerializerRegistry.RegisterSerializer(SbyteSerializer.Instance);
            SerializerRegistry.RegisterSerializer(ShortSerializer.Instance);
            SerializerRegistry.RegisterSerializer(UshortSerializer.Instance);
            SerializerRegistry.RegisterSerializer(IntSerializer.Instance);
            SerializerRegistry.RegisterSerializer(UintSerializer.Instance);
            SerializerRegistry.RegisterSerializer(LongSerializer.Instance);
            SerializerRegistry.RegisterSerializer(UlongSerializer.Instance);
            SerializerRegistry.RegisterSerializer(FloatSerializer.Instance);
            SerializerRegistry.RegisterSerializer(DoubleSerializer.Instance);
            SerializerRegistry.RegisterSerializer(ContainmentTypeSerializer.Instance);
            SerializerRegistry.RegisterSerializer(CubeFaceSerializer.Instance);
            SerializerRegistry.RegisterSerializer(CurveContinuitySerializer.Instance);
            SerializerRegistry.RegisterSerializer(CurveLoopTypeSerializer.Instance);
            SerializerRegistry.RegisterSerializer(CurveTangentSerializer.Instance);
            SerializerRegistry.RegisterSerializer(PlaneIntersectionTypeSerializer.Instance);
            SerializerRegistry.RegisterSerializer(Base27DirectionSerializer.Instance);
            SerializerRegistry.RegisterSerializer(Base6DirectionSerializer.Instance);
            SerializerRegistry.RegisterSerializer(Base6DirectionFlagsSerializer.Instance);
            SerializerRegistry.RegisterSerializer(Base6AxisSerializer.Instance);
            SerializerRegistry.RegisterSerializer(BoundingBox2ISerializer.Instance);
            SerializerRegistry.RegisterSerializer(BoundingBox2DSerializer.Instance);
            SerializerRegistry.RegisterSerializer(BoundingBox2Serializer.Instance);
            SerializerRegistry.RegisterSerializer(BoundingBoxISerializer.Instance);
            SerializerRegistry.RegisterSerializer(BoundingBoxSerializer.Instance);
            SerializerRegistry.RegisterSerializer(BoundingSphereSerializer.Instance);
            SerializerRegistry.RegisterSerializer(BoundingBoxDSerializer.Instance);
            SerializerRegistry.RegisterSerializer(BoundingSphereDSerializer.Instance);
            SerializerRegistry.RegisterSerializer(CapsuleDSerializer.Instance);
            SerializerRegistry.RegisterSerializer(MyTransformDSerializer.Instance);
            SerializerRegistry.RegisterSerializer(QuaternionDSerializer.Instance);
            SerializerRegistry.RegisterSerializer(SerializableRangeSerializer.Instance);
            SerializerRegistry.RegisterSerializer(SymmetricSerializableRangeSerializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector2DSerializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector4DSerializer.Instance);
            SerializerRegistry.RegisterSerializer(MyOrientedBoundingBoxDSerializer.Instance);
            SerializerRegistry.RegisterSerializer(PlaneDSerializer.Instance);
            SerializerRegistry.RegisterSerializer(LineDSerializer.Instance);
            SerializerRegistry.RegisterSerializer(RayDSerializer.Instance);
            SerializerRegistry.RegisterSerializer(MatrixDSerializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector3DSerializer.Instance);
            SerializerRegistry.RegisterSerializer(MatrixISerializer.Instance);
            SerializerRegistry.RegisterSerializer(MyQuadSerializer.Instance);
            SerializerRegistry.RegisterSerializer(MyQuadDSerializer.Instance);
            SerializerRegistry.RegisterSerializer(MyShort4Serializer.Instance);
            SerializerRegistry.RegisterSerializer(ColorSerializer.Instance);
            SerializerRegistry.RegisterSerializer(CapsuleSerializer.Instance);
            SerializerRegistry.RegisterSerializer(MyBlockOrientationSerializer.Instance);
            SerializerRegistry.RegisterSerializer(MyBoundsSerializer.Instance);
            SerializerRegistry.RegisterSerializer(MyOrientedBoundingBoxSerializer.Instance);
            SerializerRegistry.RegisterSerializer(MyTransformSerializer.Instance);
            SerializerRegistry.RegisterSerializer(MyUShort4Serializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector3BSerializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector3SSerializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector2ISerializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector3UByteSerializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector3UshortSerializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector4ISerializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector3I_RangeIteratorSerializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector3ISerializer.Instance);
            SerializerRegistry.RegisterSerializer(LineSerializer.Instance);
            SerializerRegistry.RegisterSerializer(MatrixSerializer.Instance);
            SerializerRegistry.RegisterSerializer(PlaneSerializer.Instance);
            SerializerRegistry.RegisterSerializer(PointSerializer.Instance);
            SerializerRegistry.RegisterSerializer(QuaternionSerializer.Instance);
            SerializerRegistry.RegisterSerializer(RaySerializer.Instance);
            SerializerRegistry.RegisterSerializer(RectangleSerializer.Instance);
            SerializerRegistry.RegisterSerializer(RectangleFSerializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector2Serializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector3Serializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector4Serializer.Instance);
            SerializerRegistry.RegisterSerializer(Vector4UByteSerializer.Instance);
    }
}