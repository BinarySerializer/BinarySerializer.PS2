namespace BinarySerializer.PS2
{
    public class IPU_File : BinarySerializable
    {
        public uint DataSize { get; set; }
        public ushort Width { get; set; }
        public ushort Height { get; set; }
        public uint FrameCount { get; set; }
        public byte[] FrameData { get; set; }
        public virtual int FPS => 30;
        public virtual bool IsAligned => false;

        public override void SerializeImpl(SerializerObject s)
        {
            s.SerializeMagicString("ipum", 4);
            DataSize = s.Serialize<uint>(DataSize, name: nameof(DataSize));
            Width = s.Serialize<ushort>(Width, name: nameof(Width));
            Height = s.Serialize<ushort>(Height, name: nameof(Height));
            FrameCount = s.Serialize<uint>(FrameCount, name: nameof(FrameCount));
            FrameData = s.SerializeArray<byte>(FrameData, DataSize - 8, name: nameof(FrameData));
        }
    }
}