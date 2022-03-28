namespace BinarySerializer.PS2
{
    /// <see href="https://psi-rockin.github.io/ps2tek/#vifcommands">VIFcode documentation</see>
    public class VIFcode : BinarySerializable
    {
        public uint IMMEDIATE { get; set; }
        public uint NUM { get; set; }
        public Command CMD { get; set; }
        public bool Stall { get; set; }

        public override void SerializeImpl(SerializerObject s)
        {
            s.DoBits<int>(b =>
            {
                IMMEDIATE = b.SerializeBits<uint>(IMMEDIATE, 16, name: nameof(IMMEDIATE));
                NUM = b.SerializeBits<uint>(NUM, 8, name: nameof(NUM));
                CMD = b.SerializeBits<Command>(CMD, 7, name: nameof(CMD));
                Stall = b.SerializeBits<bool>(Stall, 1, name: nameof(Stall));
            });
        }

        public enum Command
        {
            NOP = 0x00,
            STCYCL = 0x01,
            OFFSET = 0x02,
            BASE = 0x03,
            ITOP = 0x04,
            STMOD = 0x05,
            MSKPATH3 = 0x06,
            MARK = 0x07,
            FLUSHE = 0x10,
            FLUSH = 0x11,
            FLUSHA = 0x13,
            MSCAL = 0x14,
            MSCALF = 0x15,
            MSCNT = 0x17,
            STMASK = 0x20,
            STROW = 0x30,
            STCOL = 0x31,
            MPG = 0x4A,
            DIRECT = 0x50,
            DIRECTHL = 0x51,
            UNPACK_S_32 = 0x60,
            UNPACK_S_16 = 0x61,
            UNPACK_S_8 = 0x62,
            UNPACK_V2_32 = 0x64,
            UNPACK_V2_16 = 0x65,
            UNPACK_V2_8 = 0x66,
            UNPACK_V3_32 = 0x68,
            UNPACK_V3_16 = 0x69,
            UNPACK_V3_8 = 0x6A,
            UNPACK_V4_32 = 0x6C,
            UNPACK_V4_16 = 0x6D,
            UNPACK_V4_8 = 0x6E,
            UNPACK_V4_5 = 0x6F
        }

        public override string ToString() => $"VIFCode(CMD: {CMD}, NUM: {NUM}, IMMEDIATE: {IMMEDIATE}, STALL: {Stall})";
        public override bool UseShortLog => true;
    }
}