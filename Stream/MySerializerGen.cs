using Equinox.Utils.Stream;
namespace Equinox.Utils.Stream {
    public static class MyMemoryStreamExtensions {
        public static void Write(this MyMemoryStream stream, ref VRageMath.ContainmentType val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.ContainmentType val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.ContainmentType) tmp;
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.CubeFace val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.CubeFace val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.CubeFace) tmp;
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.CurveContinuity val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.CurveContinuity val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.CurveContinuity) tmp;
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.CurveLoopType val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.CurveLoopType val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.CurveLoopType) tmp;
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.CurveTangent val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.CurveTangent val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.CurveTangent) tmp;
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.PlaneIntersectionType val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.PlaneIntersectionType val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.PlaneIntersectionType) tmp;
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Base27Directions.Direction val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Base27Directions.Direction val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.Base27Directions.Direction) tmp;
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Base6Directions.Direction val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Base6Directions.Direction val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.Base6Directions.Direction) tmp;
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Base6Directions.DirectionFlags val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Base6Directions.DirectionFlags val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.Base6Directions.DirectionFlags) tmp;
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Base6Directions.Axis val)
		{
            var tmp = (byte) val;
            stream.Write(ref tmp);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Base6Directions.Axis val)
		{
            var tmp = default(byte);
            stream.Read(ref tmp);
            val = (VRageMath.Base6Directions.Axis) tmp;
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.BoundingBox2I val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.BoundingBox2I val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.BoundingBox2D val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.BoundingBox2D val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.BoundingBox2 val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.BoundingBox2 val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.BoundingBoxI val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.BoundingBoxI val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.BoundingBox val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.BoundingBox val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.BoundingSphere val)
		{
			stream.Write(ref val.Center);
			stream.Write(ref val.Radius);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.BoundingSphere val)
		{
			stream.Read(ref val.Center);
			stream.Read(ref val.Radius);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.BoundingBoxD val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.BoundingBoxD val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.BoundingSphereD val)
		{
			stream.Write(ref val.Center);
			stream.Write(ref val.Radius);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.BoundingSphereD val)
		{
			stream.Read(ref val.Center);
			stream.Read(ref val.Radius);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.CapsuleD val)
		{
			stream.Write(ref val.P0);
			stream.Write(ref val.P1);
			stream.Write(ref val.Radius);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.CapsuleD val)
		{
			stream.Read(ref val.P0);
			stream.Read(ref val.P1);
			stream.Read(ref val.Radius);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.MyTransformD val)
		{
			stream.Write(ref val.Rotation);
			stream.Write(ref val.Position);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.MyTransformD val)
		{
			stream.Read(ref val.Rotation);
			stream.Read(ref val.Position);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.QuaternionD val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.QuaternionD val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.SerializableRange val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.SerializableRange val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.SymetricSerializableRange val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.SymetricSerializableRange val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Vector2D val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Vector2D val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Vector4D val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Vector4D val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.MyOrientedBoundingBoxD val)
		{
			stream.Write(ref val.Center);
			stream.Write(ref val.HalfExtent);
			stream.Write(ref val.Orientation);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.MyOrientedBoundingBoxD val)
		{
			stream.Read(ref val.Center);
			stream.Read(ref val.HalfExtent);
			stream.Read(ref val.Orientation);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.PlaneD val)
		{
			stream.Write(ref val.Normal);
			stream.Write(ref val.D);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.PlaneD val)
		{
			stream.Read(ref val.Normal);
			stream.Read(ref val.D);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.LineD val)
		{
			stream.Write(ref val.From);
			stream.Write(ref val.To);
			stream.Write(ref val.Direction);
			stream.Write(ref val.Length);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.LineD val)
		{
			stream.Read(ref val.From);
			stream.Read(ref val.To);
			stream.Read(ref val.Direction);
			stream.Read(ref val.Length);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.RayD val)
		{
			stream.Write(ref val.Position);
			stream.Write(ref val.Direction);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.RayD val)
		{
			stream.Read(ref val.Position);
			stream.Read(ref val.Direction);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.MatrixD val)
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
        public static void Read(this MyMemoryStream stream, ref VRageMath.MatrixD val)
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
        public static void Write(this MyMemoryStream stream, ref VRageMath.Vector3D val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Vector3D val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.MatrixI val)
		{
			stream.Write(ref val.Right);
			stream.Write(ref val.Up);
			stream.Write(ref val.Backward);
			stream.Write(ref val.Translation);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.MatrixI val)
		{
			stream.Read(ref val.Right);
			stream.Read(ref val.Up);
			stream.Read(ref val.Backward);
			stream.Read(ref val.Translation);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.MyQuad val)
		{
			stream.Write(ref val.Point0);
			stream.Write(ref val.Point1);
			stream.Write(ref val.Point2);
			stream.Write(ref val.Point3);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.MyQuad val)
		{
			stream.Read(ref val.Point0);
			stream.Read(ref val.Point1);
			stream.Read(ref val.Point2);
			stream.Read(ref val.Point3);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.MyQuadD val)
		{
			stream.Write(ref val.Point0);
			stream.Write(ref val.Point1);
			stream.Write(ref val.Point2);
			stream.Write(ref val.Point3);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.MyQuadD val)
		{
			stream.Read(ref val.Point0);
			stream.Read(ref val.Point1);
			stream.Read(ref val.Point2);
			stream.Read(ref val.Point3);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.MyShort4 val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.MyShort4 val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Color val)
		{
			stream.Write(ref val.PackedValue);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Color val)
		{
			stream.Read(ref val.PackedValue);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Capsule val)
		{
			stream.Write(ref val.P0);
			stream.Write(ref val.P1);
			stream.Write(ref val.Radius);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Capsule val)
		{
			stream.Read(ref val.P0);
			stream.Read(ref val.P1);
			stream.Read(ref val.Radius);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.MyBlockOrientation val)
		{
			stream.Write(ref val.Forward);
			stream.Write(ref val.Up);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.MyBlockOrientation val)
		{
			stream.Read(ref val.Forward);
			stream.Read(ref val.Up);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.MyBounds val)
		{
			stream.Write(ref val.Min);
			stream.Write(ref val.Max);
			stream.Write(ref val.Default);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.MyBounds val)
		{
			stream.Read(ref val.Min);
			stream.Read(ref val.Max);
			stream.Read(ref val.Default);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.MyOrientedBoundingBox val)
		{
			stream.Write(ref val.Center);
			stream.Write(ref val.HalfExtent);
			stream.Write(ref val.Orientation);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.MyOrientedBoundingBox val)
		{
			stream.Read(ref val.Center);
			stream.Read(ref val.HalfExtent);
			stream.Read(ref val.Orientation);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.MyTransform val)
		{
			stream.Write(ref val.Rotation);
			stream.Write(ref val.Position);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.MyTransform val)
		{
			stream.Read(ref val.Rotation);
			stream.Read(ref val.Position);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.MyUShort4 val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.MyUShort4 val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Vector3B val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Vector3B val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Vector3S val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Vector3S val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Vector2I val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Vector2I val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Vector3UByte val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Vector3UByte val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Vector3Ushort val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Vector3Ushort val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Vector4I val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Vector4I val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Vector3I val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Vector3I val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Line val)
		{
			stream.Write(ref val.From);
			stream.Write(ref val.To);
			stream.Write(ref val.Direction);
			stream.Write(ref val.Length);
			stream.Write(ref val.BoundingBox);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Line val)
		{
			stream.Read(ref val.From);
			stream.Read(ref val.To);
			stream.Read(ref val.Direction);
			stream.Read(ref val.Length);
			stream.Read(ref val.BoundingBox);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Matrix val)
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
        public static void Read(this MyMemoryStream stream, ref VRageMath.Matrix val)
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
        public static void Write(this MyMemoryStream stream, ref VRageMath.Plane val)
		{
			stream.Write(ref val.Normal);
			stream.Write(ref val.D);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Plane val)
		{
			stream.Read(ref val.Normal);
			stream.Read(ref val.D);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Point val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Point val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Quaternion val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Quaternion val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Ray val)
		{
			stream.Write(ref val.Position);
			stream.Write(ref val.Direction);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Ray val)
		{
			stream.Read(ref val.Position);
			stream.Read(ref val.Direction);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Rectangle val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Width);
			stream.Write(ref val.Height);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Rectangle val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Width);
			stream.Read(ref val.Height);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.RectangleF val)
		{
			stream.Write(ref val.Position);
			stream.Write(ref val.Size);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.RectangleF val)
		{
			stream.Read(ref val.Position);
			stream.Read(ref val.Size);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Vector2 val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Vector2 val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Vector3 val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Vector3 val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Vector4 val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Vector4 val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MyMemoryStream stream, ref VRageMath.Vector4UByte val)
		{
			stream.Write(ref val.X);
			stream.Write(ref val.Y);
			stream.Write(ref val.Z);
			stream.Write(ref val.W);
        }
        public static void Read(this MyMemoryStream stream, ref VRageMath.Vector4UByte val)
		{
			stream.Read(ref val.X);
			stream.Read(ref val.Y);
			stream.Read(ref val.Z);
			stream.Read(ref val.W);
        }
        public static void Write(this MyMemoryStream stream, byte val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, sbyte val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, short val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, ushort val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, int val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, uint val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, long val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, ulong val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, float val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, double val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.ContainmentType val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.CubeFace val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.CurveContinuity val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.CurveLoopType val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.CurveTangent val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.PlaneIntersectionType val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Base27Directions.Direction val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Base6Directions.Direction val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Base6Directions.DirectionFlags val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Base6Directions.Axis val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.BoundingBox2I val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.BoundingBox2D val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.BoundingBox2 val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.BoundingBoxI val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.BoundingBox val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.BoundingSphere val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.BoundingBoxD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.BoundingSphereD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.CapsuleD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.MyTransformD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.QuaternionD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.SerializableRange val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.SymetricSerializableRange val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Vector2D val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Vector4D val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.MyOrientedBoundingBoxD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.PlaneD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.LineD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.RayD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.MatrixD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Vector3D val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.MatrixI val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.MyQuad val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.MyQuadD val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.MyShort4 val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Color val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Capsule val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.MyBlockOrientation val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.MyBounds val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.MyOrientedBoundingBox val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.MyTransform val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.MyUShort4 val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Vector3B val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Vector3S val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Vector2I val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Vector3UByte val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Vector3Ushort val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Vector4I val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Vector3I val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Line val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Matrix val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Plane val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Point val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Quaternion val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Ray val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Rectangle val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.RectangleF val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Vector2 val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Vector3 val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Vector4 val)
		{
            stream.Write(ref val);
        }
        public static void Write(this MyMemoryStream stream, VRageMath.Vector4UByte val)
		{
            stream.Write(ref val);
        }
        public static byte ReadByte(this MyMemoryStream stream)
		{
            var val = default(byte);
            stream.Read(ref val);
            return val;
        }
        public static sbyte ReadSByte(this MyMemoryStream stream)
		{
            var val = default(sbyte);
            stream.Read(ref val);
            return val;
        }
        public static short ReadInt16(this MyMemoryStream stream)
		{
            var val = default(short);
            stream.Read(ref val);
            return val;
        }
        public static ushort ReadUInt16(this MyMemoryStream stream)
		{
            var val = default(ushort);
            stream.Read(ref val);
            return val;
        }
        public static int ReadInt32(this MyMemoryStream stream)
		{
            var val = default(int);
            stream.Read(ref val);
            return val;
        }
        public static uint ReadUInt32(this MyMemoryStream stream)
		{
            var val = default(uint);
            stream.Read(ref val);
            return val;
        }
        public static long ReadInt64(this MyMemoryStream stream)
		{
            var val = default(long);
            stream.Read(ref val);
            return val;
        }
        public static ulong ReadUInt64(this MyMemoryStream stream)
		{
            var val = default(ulong);
            stream.Read(ref val);
            return val;
        }
        public static float ReadSingle(this MyMemoryStream stream)
		{
            var val = default(float);
            stream.Read(ref val);
            return val;
        }
        public static double ReadDouble(this MyMemoryStream stream)
		{
            var val = default(double);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.ContainmentType ReadContainmentType(this MyMemoryStream stream)
		{
            var val = default(VRageMath.ContainmentType);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.CubeFace ReadCubeFace(this MyMemoryStream stream)
		{
            var val = default(VRageMath.CubeFace);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.CurveContinuity ReadCurveContinuity(this MyMemoryStream stream)
		{
            var val = default(VRageMath.CurveContinuity);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.CurveLoopType ReadCurveLoopType(this MyMemoryStream stream)
		{
            var val = default(VRageMath.CurveLoopType);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.CurveTangent ReadCurveTangent(this MyMemoryStream stream)
		{
            var val = default(VRageMath.CurveTangent);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.PlaneIntersectionType ReadPlaneIntersectionType(this MyMemoryStream stream)
		{
            var val = default(VRageMath.PlaneIntersectionType);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Base27Directions.Direction ReadBase27Direction(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Base27Directions.Direction);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Base6Directions.Direction ReadBase6Direction(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Base6Directions.Direction);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Base6Directions.DirectionFlags ReadBase6DirectionFlags(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Base6Directions.DirectionFlags);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Base6Directions.Axis ReadBase6Axis(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Base6Directions.Axis);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingBox2I ReadBoundingBox2I(this MyMemoryStream stream)
		{
            var val = default(VRageMath.BoundingBox2I);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingBox2D ReadBoundingBox2D(this MyMemoryStream stream)
		{
            var val = default(VRageMath.BoundingBox2D);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingBox2 ReadBoundingBox2(this MyMemoryStream stream)
		{
            var val = default(VRageMath.BoundingBox2);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingBoxI ReadBoundingBoxI(this MyMemoryStream stream)
		{
            var val = default(VRageMath.BoundingBoxI);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingBox ReadBoundingBox(this MyMemoryStream stream)
		{
            var val = default(VRageMath.BoundingBox);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingSphere ReadBoundingSphere(this MyMemoryStream stream)
		{
            var val = default(VRageMath.BoundingSphere);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingBoxD ReadBoundingBoxD(this MyMemoryStream stream)
		{
            var val = default(VRageMath.BoundingBoxD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.BoundingSphereD ReadBoundingSphereD(this MyMemoryStream stream)
		{
            var val = default(VRageMath.BoundingSphereD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.CapsuleD ReadCapsuleD(this MyMemoryStream stream)
		{
            var val = default(VRageMath.CapsuleD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyTransformD ReadMyTransformD(this MyMemoryStream stream)
		{
            var val = default(VRageMath.MyTransformD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.QuaternionD ReadQuaternionD(this MyMemoryStream stream)
		{
            var val = default(VRageMath.QuaternionD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.SerializableRange ReadSerializableRange(this MyMemoryStream stream)
		{
            var val = default(VRageMath.SerializableRange);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.SymetricSerializableRange ReadSymetricSerializableRange(this MyMemoryStream stream)
		{
            var val = default(VRageMath.SymetricSerializableRange);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector2D ReadVector2D(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Vector2D);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector4D ReadVector4D(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Vector4D);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyOrientedBoundingBoxD ReadMyOrientedBoundingBoxD(this MyMemoryStream stream)
		{
            var val = default(VRageMath.MyOrientedBoundingBoxD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.PlaneD ReadPlaneD(this MyMemoryStream stream)
		{
            var val = default(VRageMath.PlaneD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.LineD ReadLineD(this MyMemoryStream stream)
		{
            var val = default(VRageMath.LineD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.RayD ReadRayD(this MyMemoryStream stream)
		{
            var val = default(VRageMath.RayD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MatrixD ReadMatrixD(this MyMemoryStream stream)
		{
            var val = default(VRageMath.MatrixD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3D ReadVector3D(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Vector3D);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MatrixI ReadMatrixI(this MyMemoryStream stream)
		{
            var val = default(VRageMath.MatrixI);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyQuad ReadMyQuad(this MyMemoryStream stream)
		{
            var val = default(VRageMath.MyQuad);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyQuadD ReadMyQuadD(this MyMemoryStream stream)
		{
            var val = default(VRageMath.MyQuadD);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyShort4 ReadMyShort4(this MyMemoryStream stream)
		{
            var val = default(VRageMath.MyShort4);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Color ReadColor(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Color);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Capsule ReadCapsule(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Capsule);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyBlockOrientation ReadMyBlockOrientation(this MyMemoryStream stream)
		{
            var val = default(VRageMath.MyBlockOrientation);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyBounds ReadMyBounds(this MyMemoryStream stream)
		{
            var val = default(VRageMath.MyBounds);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyOrientedBoundingBox ReadMyOrientedBoundingBox(this MyMemoryStream stream)
		{
            var val = default(VRageMath.MyOrientedBoundingBox);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyTransform ReadMyTransform(this MyMemoryStream stream)
		{
            var val = default(VRageMath.MyTransform);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.MyUShort4 ReadMyUShort4(this MyMemoryStream stream)
		{
            var val = default(VRageMath.MyUShort4);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3B ReadVector3B(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Vector3B);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3S ReadVector3S(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Vector3S);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector2I ReadVector2I(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Vector2I);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3UByte ReadVector3UByte(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Vector3UByte);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3Ushort ReadVector3Ushort(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Vector3Ushort);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector4I ReadVector4I(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Vector4I);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3I ReadVector3I(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Vector3I);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Line ReadLine(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Line);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Matrix ReadMatrix(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Matrix);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Plane ReadPlane(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Plane);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Point ReadPoint(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Point);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Quaternion ReadQuaternion(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Quaternion);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Ray ReadRay(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Ray);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Rectangle ReadRectangle(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Rectangle);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.RectangleF ReadRectangleF(this MyMemoryStream stream)
		{
            var val = default(VRageMath.RectangleF);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector2 ReadVector2(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Vector2);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector3 ReadVector3(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Vector3);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector4 ReadVector4(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Vector4);
            stream.Read(ref val);
            return val;
        }
        public static VRageMath.Vector4UByte ReadVector4UByte(this MyMemoryStream stream)
		{
            var val = default(VRageMath.Vector4UByte);
            stream.Read(ref val);
            return val;
        }
    }
}

public static class MySerializerExtensions {
    public class MyByteSerializer : MySerializer<byte> {
        public static readonly MyByteSerializer Instance = new MyByteSerializer();
        public override void Read(ref byte val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref byte val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MySbyteSerializer : MySerializer<sbyte> {
        public static readonly MySbyteSerializer Instance = new MySbyteSerializer();
        public override void Read(ref sbyte val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref sbyte val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyShortSerializer : MySerializer<short> {
        public static readonly MyShortSerializer Instance = new MyShortSerializer();
        public override void Read(ref short val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref short val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyUshortSerializer : MySerializer<ushort> {
        public static readonly MyUshortSerializer Instance = new MyUshortSerializer();
        public override void Read(ref ushort val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref ushort val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyIntSerializer : MySerializer<int> {
        public static readonly MyIntSerializer Instance = new MyIntSerializer();
        public override void Read(ref int val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref int val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyUintSerializer : MySerializer<uint> {
        public static readonly MyUintSerializer Instance = new MyUintSerializer();
        public override void Read(ref uint val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref uint val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyLongSerializer : MySerializer<long> {
        public static readonly MyLongSerializer Instance = new MyLongSerializer();
        public override void Read(ref long val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref long val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyUlongSerializer : MySerializer<ulong> {
        public static readonly MyUlongSerializer Instance = new MyUlongSerializer();
        public override void Read(ref ulong val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref ulong val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyFloatSerializer : MySerializer<float> {
        public static readonly MyFloatSerializer Instance = new MyFloatSerializer();
        public override void Read(ref float val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref float val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyDoubleSerializer : MySerializer<double> {
        public static readonly MyDoubleSerializer Instance = new MyDoubleSerializer();
        public override void Read(ref double val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref double val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyContainmentTypeSerializer : MySerializer<VRageMath.ContainmentType> {
        public static readonly MyContainmentTypeSerializer Instance = new MyContainmentTypeSerializer();
        public override void Read(ref VRageMath.ContainmentType val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.ContainmentType val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyCubeFaceSerializer : MySerializer<VRageMath.CubeFace> {
        public static readonly MyCubeFaceSerializer Instance = new MyCubeFaceSerializer();
        public override void Read(ref VRageMath.CubeFace val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.CubeFace val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyCurveContinuitySerializer : MySerializer<VRageMath.CurveContinuity> {
        public static readonly MyCurveContinuitySerializer Instance = new MyCurveContinuitySerializer();
        public override void Read(ref VRageMath.CurveContinuity val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.CurveContinuity val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyCurveLoopTypeSerializer : MySerializer<VRageMath.CurveLoopType> {
        public static readonly MyCurveLoopTypeSerializer Instance = new MyCurveLoopTypeSerializer();
        public override void Read(ref VRageMath.CurveLoopType val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.CurveLoopType val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyCurveTangentSerializer : MySerializer<VRageMath.CurveTangent> {
        public static readonly MyCurveTangentSerializer Instance = new MyCurveTangentSerializer();
        public override void Read(ref VRageMath.CurveTangent val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.CurveTangent val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyPlaneIntersectionTypeSerializer : MySerializer<VRageMath.PlaneIntersectionType> {
        public static readonly MyPlaneIntersectionTypeSerializer Instance = new MyPlaneIntersectionTypeSerializer();
        public override void Read(ref VRageMath.PlaneIntersectionType val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.PlaneIntersectionType val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBase27DirectionSerializer : MySerializer<VRageMath.Base27Directions.Direction> {
        public static readonly MyBase27DirectionSerializer Instance = new MyBase27DirectionSerializer();
        public override void Read(ref VRageMath.Base27Directions.Direction val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Base27Directions.Direction val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBase6DirectionSerializer : MySerializer<VRageMath.Base6Directions.Direction> {
        public static readonly MyBase6DirectionSerializer Instance = new MyBase6DirectionSerializer();
        public override void Read(ref VRageMath.Base6Directions.Direction val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Base6Directions.Direction val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBase6DirectionFlagsSerializer : MySerializer<VRageMath.Base6Directions.DirectionFlags> {
        public static readonly MyBase6DirectionFlagsSerializer Instance = new MyBase6DirectionFlagsSerializer();
        public override void Read(ref VRageMath.Base6Directions.DirectionFlags val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Base6Directions.DirectionFlags val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBase6AxisSerializer : MySerializer<VRageMath.Base6Directions.Axis> {
        public static readonly MyBase6AxisSerializer Instance = new MyBase6AxisSerializer();
        public override void Read(ref VRageMath.Base6Directions.Axis val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Base6Directions.Axis val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBoundingBox2ISerializer : MySerializer<VRageMath.BoundingBox2I> {
        public static readonly MyBoundingBox2ISerializer Instance = new MyBoundingBox2ISerializer();
        public override void Read(ref VRageMath.BoundingBox2I val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingBox2I val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBoundingBox2DSerializer : MySerializer<VRageMath.BoundingBox2D> {
        public static readonly MyBoundingBox2DSerializer Instance = new MyBoundingBox2DSerializer();
        public override void Read(ref VRageMath.BoundingBox2D val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingBox2D val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBoundingBox2Serializer : MySerializer<VRageMath.BoundingBox2> {
        public static readonly MyBoundingBox2Serializer Instance = new MyBoundingBox2Serializer();
        public override void Read(ref VRageMath.BoundingBox2 val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingBox2 val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBoundingBoxISerializer : MySerializer<VRageMath.BoundingBoxI> {
        public static readonly MyBoundingBoxISerializer Instance = new MyBoundingBoxISerializer();
        public override void Read(ref VRageMath.BoundingBoxI val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingBoxI val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBoundingBoxSerializer : MySerializer<VRageMath.BoundingBox> {
        public static readonly MyBoundingBoxSerializer Instance = new MyBoundingBoxSerializer();
        public override void Read(ref VRageMath.BoundingBox val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingBox val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBoundingSphereSerializer : MySerializer<VRageMath.BoundingSphere> {
        public static readonly MyBoundingSphereSerializer Instance = new MyBoundingSphereSerializer();
        public override void Read(ref VRageMath.BoundingSphere val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingSphere val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBoundingBoxDSerializer : MySerializer<VRageMath.BoundingBoxD> {
        public static readonly MyBoundingBoxDSerializer Instance = new MyBoundingBoxDSerializer();
        public override void Read(ref VRageMath.BoundingBoxD val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingBoxD val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBoundingSphereDSerializer : MySerializer<VRageMath.BoundingSphereD> {
        public static readonly MyBoundingSphereDSerializer Instance = new MyBoundingSphereDSerializer();
        public override void Read(ref VRageMath.BoundingSphereD val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.BoundingSphereD val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyCapsuleDSerializer : MySerializer<VRageMath.CapsuleD> {
        public static readonly MyCapsuleDSerializer Instance = new MyCapsuleDSerializer();
        public override void Read(ref VRageMath.CapsuleD val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.CapsuleD val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyTransformDSerializer : MySerializer<VRageMath.MyTransformD> {
        public static readonly MyTransformDSerializer Instance = new MyTransformDSerializer();
        public override void Read(ref VRageMath.MyTransformD val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyTransformD val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyQuaternionDSerializer : MySerializer<VRageMath.QuaternionD> {
        public static readonly MyQuaternionDSerializer Instance = new MyQuaternionDSerializer();
        public override void Read(ref VRageMath.QuaternionD val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.QuaternionD val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MySerializableRangeSerializer : MySerializer<VRageMath.SerializableRange> {
        public static readonly MySerializableRangeSerializer Instance = new MySerializableRangeSerializer();
        public override void Read(ref VRageMath.SerializableRange val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.SerializableRange val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MySymetricSerializableRangeSerializer : MySerializer<VRageMath.SymetricSerializableRange> {
        public static readonly MySymetricSerializableRangeSerializer Instance = new MySymetricSerializableRangeSerializer();
        public override void Read(ref VRageMath.SymetricSerializableRange val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.SymetricSerializableRange val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyVector2DSerializer : MySerializer<VRageMath.Vector2D> {
        public static readonly MyVector2DSerializer Instance = new MyVector2DSerializer();
        public override void Read(ref VRageMath.Vector2D val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector2D val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyVector4DSerializer : MySerializer<VRageMath.Vector4D> {
        public static readonly MyVector4DSerializer Instance = new MyVector4DSerializer();
        public override void Read(ref VRageMath.Vector4D val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector4D val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyOrientedBoundingBoxDSerializer : MySerializer<VRageMath.MyOrientedBoundingBoxD> {
        public static readonly MyOrientedBoundingBoxDSerializer Instance = new MyOrientedBoundingBoxDSerializer();
        public override void Read(ref VRageMath.MyOrientedBoundingBoxD val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyOrientedBoundingBoxD val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyPlaneDSerializer : MySerializer<VRageMath.PlaneD> {
        public static readonly MyPlaneDSerializer Instance = new MyPlaneDSerializer();
        public override void Read(ref VRageMath.PlaneD val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.PlaneD val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyLineDSerializer : MySerializer<VRageMath.LineD> {
        public static readonly MyLineDSerializer Instance = new MyLineDSerializer();
        public override void Read(ref VRageMath.LineD val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.LineD val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyRayDSerializer : MySerializer<VRageMath.RayD> {
        public static readonly MyRayDSerializer Instance = new MyRayDSerializer();
        public override void Read(ref VRageMath.RayD val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.RayD val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyMatrixDSerializer : MySerializer<VRageMath.MatrixD> {
        public static readonly MyMatrixDSerializer Instance = new MyMatrixDSerializer();
        public override void Read(ref VRageMath.MatrixD val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MatrixD val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyVector3DSerializer : MySerializer<VRageMath.Vector3D> {
        public static readonly MyVector3DSerializer Instance = new MyVector3DSerializer();
        public override void Read(ref VRageMath.Vector3D val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3D val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyMatrixISerializer : MySerializer<VRageMath.MatrixI> {
        public static readonly MyMatrixISerializer Instance = new MyMatrixISerializer();
        public override void Read(ref VRageMath.MatrixI val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MatrixI val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyQuadSerializer : MySerializer<VRageMath.MyQuad> {
        public static readonly MyQuadSerializer Instance = new MyQuadSerializer();
        public override void Read(ref VRageMath.MyQuad val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyQuad val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyQuadDSerializer : MySerializer<VRageMath.MyQuadD> {
        public static readonly MyQuadDSerializer Instance = new MyQuadDSerializer();
        public override void Read(ref VRageMath.MyQuadD val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyQuadD val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyShort4Serializer : MySerializer<VRageMath.MyShort4> {
        public static readonly MyShort4Serializer Instance = new MyShort4Serializer();
        public override void Read(ref VRageMath.MyShort4 val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyShort4 val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyColorSerializer : MySerializer<VRageMath.Color> {
        public static readonly MyColorSerializer Instance = new MyColorSerializer();
        public override void Read(ref VRageMath.Color val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Color val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyCapsuleSerializer : MySerializer<VRageMath.Capsule> {
        public static readonly MyCapsuleSerializer Instance = new MyCapsuleSerializer();
        public override void Read(ref VRageMath.Capsule val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Capsule val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBlockOrientationSerializer : MySerializer<VRageMath.MyBlockOrientation> {
        public static readonly MyBlockOrientationSerializer Instance = new MyBlockOrientationSerializer();
        public override void Read(ref VRageMath.MyBlockOrientation val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyBlockOrientation val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyBoundsSerializer : MySerializer<VRageMath.MyBounds> {
        public static readonly MyBoundsSerializer Instance = new MyBoundsSerializer();
        public override void Read(ref VRageMath.MyBounds val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyBounds val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyOrientedBoundingBoxSerializer : MySerializer<VRageMath.MyOrientedBoundingBox> {
        public static readonly MyOrientedBoundingBoxSerializer Instance = new MyOrientedBoundingBoxSerializer();
        public override void Read(ref VRageMath.MyOrientedBoundingBox val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyOrientedBoundingBox val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyTransformSerializer : MySerializer<VRageMath.MyTransform> {
        public static readonly MyTransformSerializer Instance = new MyTransformSerializer();
        public override void Read(ref VRageMath.MyTransform val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyTransform val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyUShort4Serializer : MySerializer<VRageMath.MyUShort4> {
        public static readonly MyUShort4Serializer Instance = new MyUShort4Serializer();
        public override void Read(ref VRageMath.MyUShort4 val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.MyUShort4 val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyVector3BSerializer : MySerializer<VRageMath.Vector3B> {
        public static readonly MyVector3BSerializer Instance = new MyVector3BSerializer();
        public override void Read(ref VRageMath.Vector3B val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3B val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyVector3SSerializer : MySerializer<VRageMath.Vector3S> {
        public static readonly MyVector3SSerializer Instance = new MyVector3SSerializer();
        public override void Read(ref VRageMath.Vector3S val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3S val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyVector2ISerializer : MySerializer<VRageMath.Vector2I> {
        public static readonly MyVector2ISerializer Instance = new MyVector2ISerializer();
        public override void Read(ref VRageMath.Vector2I val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector2I val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyVector3UByteSerializer : MySerializer<VRageMath.Vector3UByte> {
        public static readonly MyVector3UByteSerializer Instance = new MyVector3UByteSerializer();
        public override void Read(ref VRageMath.Vector3UByte val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3UByte val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyVector3UshortSerializer : MySerializer<VRageMath.Vector3Ushort> {
        public static readonly MyVector3UshortSerializer Instance = new MyVector3UshortSerializer();
        public override void Read(ref VRageMath.Vector3Ushort val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3Ushort val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyVector4ISerializer : MySerializer<VRageMath.Vector4I> {
        public static readonly MyVector4ISerializer Instance = new MyVector4ISerializer();
        public override void Read(ref VRageMath.Vector4I val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector4I val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyVector3ISerializer : MySerializer<VRageMath.Vector3I> {
        public static readonly MyVector3ISerializer Instance = new MyVector3ISerializer();
        public override void Read(ref VRageMath.Vector3I val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3I val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyLineSerializer : MySerializer<VRageMath.Line> {
        public static readonly MyLineSerializer Instance = new MyLineSerializer();
        public override void Read(ref VRageMath.Line val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Line val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyMatrixSerializer : MySerializer<VRageMath.Matrix> {
        public static readonly MyMatrixSerializer Instance = new MyMatrixSerializer();
        public override void Read(ref VRageMath.Matrix val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Matrix val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyPlaneSerializer : MySerializer<VRageMath.Plane> {
        public static readonly MyPlaneSerializer Instance = new MyPlaneSerializer();
        public override void Read(ref VRageMath.Plane val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Plane val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyPointSerializer : MySerializer<VRageMath.Point> {
        public static readonly MyPointSerializer Instance = new MyPointSerializer();
        public override void Read(ref VRageMath.Point val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Point val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyQuaternionSerializer : MySerializer<VRageMath.Quaternion> {
        public static readonly MyQuaternionSerializer Instance = new MyQuaternionSerializer();
        public override void Read(ref VRageMath.Quaternion val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Quaternion val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyRaySerializer : MySerializer<VRageMath.Ray> {
        public static readonly MyRaySerializer Instance = new MyRaySerializer();
        public override void Read(ref VRageMath.Ray val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Ray val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyRectangleSerializer : MySerializer<VRageMath.Rectangle> {
        public static readonly MyRectangleSerializer Instance = new MyRectangleSerializer();
        public override void Read(ref VRageMath.Rectangle val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Rectangle val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyRectangleFSerializer : MySerializer<VRageMath.RectangleF> {
        public static readonly MyRectangleFSerializer Instance = new MyRectangleFSerializer();
        public override void Read(ref VRageMath.RectangleF val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.RectangleF val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyVector2Serializer : MySerializer<VRageMath.Vector2> {
        public static readonly MyVector2Serializer Instance = new MyVector2Serializer();
        public override void Read(ref VRageMath.Vector2 val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector2 val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyVector3Serializer : MySerializer<VRageMath.Vector3> {
        public static readonly MyVector3Serializer Instance = new MyVector3Serializer();
        public override void Read(ref VRageMath.Vector3 val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector3 val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyVector4Serializer : MySerializer<VRageMath.Vector4> {
        public static readonly MyVector4Serializer Instance = new MyVector4Serializer();
        public override void Read(ref VRageMath.Vector4 val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector4 val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }
    public class MyVector4UByteSerializer : MySerializer<VRageMath.Vector4UByte> {
        public static readonly MyVector4UByteSerializer Instance = new MyVector4UByteSerializer();
        public override void Read(ref VRageMath.Vector4UByte val, MyMemoryStream stream)
		{
            stream.Read(ref val);
        }
        public override void Write(ref VRageMath.Vector4UByte val, MyMemoryStream stream)
		{
            stream.Write(ref val);
        }
    }

    public static void RegisterBuiltinTypes()
	{
            MySerializerRegistry.RegisterSerializer(MyByteSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MySbyteSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyShortSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyUshortSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyIntSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyUintSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyLongSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyUlongSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyFloatSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyDoubleSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyContainmentTypeSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyCubeFaceSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyCurveContinuitySerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyCurveLoopTypeSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyCurveTangentSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyPlaneIntersectionTypeSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyBase27DirectionSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyBase6DirectionSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyBase6DirectionFlagsSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyBase6AxisSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyBoundingBox2ISerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyBoundingBox2DSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyBoundingBox2Serializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyBoundingBoxISerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyBoundingBoxSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyBoundingSphereSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyBoundingBoxDSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyBoundingSphereDSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyCapsuleDSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyTransformDSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyQuaternionDSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MySerializableRangeSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MySymetricSerializableRangeSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyVector2DSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyVector4DSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyOrientedBoundingBoxDSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyPlaneDSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyLineDSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyRayDSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyMatrixDSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyVector3DSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyMatrixISerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyQuadSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyQuadDSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyShort4Serializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyColorSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyCapsuleSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyBlockOrientationSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyBoundsSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyOrientedBoundingBoxSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyTransformSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyUShort4Serializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyVector3BSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyVector3SSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyVector2ISerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyVector3UByteSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyVector3UshortSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyVector4ISerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyVector3ISerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyLineSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyMatrixSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyPlaneSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyPointSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyQuaternionSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyRaySerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyRectangleSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyRectangleFSerializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyVector2Serializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyVector3Serializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyVector4Serializer.Instance);
            MySerializerRegistry.RegisterSerializer(MyVector4UByteSerializer.Instance);
    }
}