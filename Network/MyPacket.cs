using Equinox.Utils.Stream;

namespace Equinox.Utils.Network
{
    public abstract class MyPacket
    {
        public MyEndpointId Source;

        public virtual void ReadFrom(MyMemoryStream input)
        {
            ulong id = 0;
            input.Read(ref id);
            Source = (MyEndpointId) id;
        }

        public virtual void WriteTo(MyMemoryStream output)
        {
            output.Write((ulong) Source);
        }
    }

    public abstract class MySubStreamPacket : MyPacket
    {
        public readonly MyMemoryStream Stream;

        protected MySubStreamPacket(int initialSize)
        {
            Stream = MyMemoryStream.CreateEmptyStream(128);
        }

        public override void ReadFrom(MyMemoryStream input)
        {
            base.ReadFrom(input);
            var len = input.Read7BitEncodedInt();
            Stream.WriteHead = Stream.ReadHead = 0;
            input.Read(Stream.Backing, 0, len);
            Stream.WriteHead += len;
        }

        public override void WriteTo(MyMemoryStream output)
        {
            base.WriteTo(output);
            var count = Stream.WriteHead - Stream.ReadHead;
            output.Write7BitEncodedInt(count);
            output.Write(Stream.Backing, Stream.ReadHead, count);
            Stream.ReadHead = Stream.WriteHead;
        }
    }
}
