using Equinox.Utils.Stream;

namespace Equinox.Utils.Network
{
    public abstract class Packet
    {
        public EndpointId Source;

        public virtual void ReadFrom(MemoryStream input)
        {
            ulong id = 0;
            input.Read(ref id);
            Source = (EndpointId) id;
        }

        public virtual void WriteTo(MemoryStream output)
        {
            output.Write((ulong) Source);
        }
    }

    public abstract class SubStreamPacket : Packet
    {
        public readonly MemoryStream Stream;

        protected SubStreamPacket(int initialSize)
        {
            Stream = MemoryStream.CreateEmptyStream(128);
        }

        public override void ReadFrom(MemoryStream input)
        {
            base.ReadFrom(input);
            var len = input.Read7BitEncodedInt();
            Stream.WriteHead = Stream.ReadHead = 0;
            input.Read(Stream.Backing, 0, len);
            Stream.WriteHead += len;
        }

        public override void WriteTo(MemoryStream output)
        {
            base.WriteTo(output);
            var count = Stream.WriteHead - Stream.ReadHead;
            output.Write7BitEncodedInt(count);
            output.Write(Stream.Backing, Stream.ReadHead, count);
            Stream.ReadHead = Stream.WriteHead;
        }
    }
}
