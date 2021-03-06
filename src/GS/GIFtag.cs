namespace BinarySerializer.PS2
{
    /// <see href="https://psi-rockin.github.io/ps2tek/#giftags">GIFtag documentation</see>
    public class GIFtag : BinarySerializable
    {
        public ushort NLOOP { get; set; }
        public byte EOP { get; set; }
        public byte PRE { get; set; }
        public ushort PRIM { get; set; }
        public DataFormat FLG { get; set; }
        public byte NREG { get; set; }
        public Register[] REGS { get; set; }

        public override void SerializeImpl(SerializerObject s)
        {
            s.DoBits<long>(b =>
            {
                NLOOP = b.SerializeBits<ushort>(NLOOP, 15, name: nameof(NLOOP));
                EOP = b.SerializeBits<byte>(EOP, 1, name: nameof(EOP));
                b.SerializePadding(30);
                PRE = b.SerializeBits<byte>(PRE, 1, name: nameof(PRE));
                PRIM = b.SerializeBits<ushort>(PRIM, 11, name: nameof(PRIM));
                FLG = b.SerializeBits<DataFormat>(FLG, 2, name: nameof(FLG));
                NREG = b.SerializeBits<byte>(NREG, 4, name: nameof(NREG));
            });
            s.DoBits<long>(b =>
            {
                if (NREG != 0)
                {
                    REGS = new Register[NREG];
                    for (int i = 0; i < NREG; i++)
                        REGS[i] = b.SerializeBits<Register>(REGS[i], 4, name: $"{nameof(REGS)}[{i}]");
                }
                
                if (64 - NREG * 4 != 0)
                    b.SerializePadding(64 - NREG * 4);
            });
        }

        public enum DataFormat
        {
            PACKED,
            REGLIST,
            IMAGE,
            DISABLE
        }

        public enum Register
        {
            PRIM,
            RGBAQ,
            ST,
            UV,
            XYZF2,
            XYZ2,
            TEX0_1,
            TEX0_2,
            CLAMP_1,
            CLAMP_2,
            FOG,
            RESERVED,
            XYZF3,
            XYZ3,
            AD,
            NOP
        }
    }
}