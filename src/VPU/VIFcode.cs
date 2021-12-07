namespace BinarySerializer.PS2
{
    /// <see href="https://psi-rockin.github.io/ps2tek/#vifcommands">VIFcode documentation</see>
    public class VIFcode : BinarySerializable
    {
        public uint IMMEDIATE { get; set; }
        public uint NUM { get; set; }
        public uint CMD { get; set; }
        public bool Stall { get; set; }

        public override void SerializeImpl(SerializerObject s)
        {
            s.DoBits<int>(b =>
            {
                IMMEDIATE = b.SerializeBits<uint>(IMMEDIATE, 16, name: nameof(IMMEDIATE));
                NUM = b.SerializeBits<uint>(NUM, 8, name: nameof(NUM));
                CMD = b.SerializeBits<uint>(CMD, 7, name: nameof(CMD));
                Stall = b.SerializeBits<bool>(Stall, 1, name: nameof(Stall));
            });
        }

        public override string ToString() => $"VIFCode(CMD: {CMD:X2}, NUM: {NUM}, IMMEDIATE: {IMMEDIATE}, STALL: {Stall})";
        public override bool UseShortLog => true;
    }
}