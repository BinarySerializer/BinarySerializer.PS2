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
            s.DoBits<ulong>(b =>
            {
                NLOOP = (ushort)b.SerializeBits<int>(NLOOP, 15, name: nameof(NLOOP));
                EOP = (byte)b.SerializeBits<int>(EOP, 1, name: nameof(EOP));
                b.SerializeBits<int>(default, 30, name: "Padding");
                PRE = (byte)b.SerializeBits<int>(PRE, 1, name: nameof(PRE));
                PRIM = (ushort)b.SerializeBits<int>(PRIM, 11, name: nameof(PRIM));
                FLG = (DataFormat)b.SerializeBits<int>((int)FLG, 2, name: nameof(FLG));
                NREG = (byte)b.SerializeBits<int>(NREG, 4, name: nameof(NREG));
            });
            s.DoBits<ulong>(b =>
            {
                if (NREG != 0)
                {
                    REGS = new Register[NREG];
                    for (int i = 0; i < NREG; i++)
                        REGS[i] = (Register)b.SerializeBits<int>((int)REGS[i], 4, name: $"{nameof(REGS)}[{i}]");
                }
                
                if (64 - NREG * 4 != 0)
                    b.SerializeBits<long>(default, 64 - NREG * 4, name: "Padding");
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