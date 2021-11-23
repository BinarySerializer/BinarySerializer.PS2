namespace BinarySerializer.PS2
{
    public class GSReg_BITBLTBUF : GSRegister
    {
        public override GSRegisters RegisterByte => GSRegisters.BITBLTBUF;

        public ushort SBP { get; set; }
        public ushort SBW { get; set; }
        public GS.PixelStorageMode SPSM { get; set; }
        public ushort DBP { get; set; }
        public ushort DBW { get; set; }
        public GS.PixelStorageMode DPSM { get; set; }

        public override void SerializeRegisterImpl(SerializerObject s)
        {
            s.DoBits<ulong>(b =>
            {
                SBP = (ushort)b.SerializeBits<int>(SBP, 14, name: nameof(SBP));
                b.SerializeBits<int>(default, 2, name: "Padding");
                SBW = (ushort)b.SerializeBits<int>(SBW, 6, name: nameof(SBW));
                b.SerializeBits<int>(default, 2, name: "Padding");
                SPSM = (GS.PixelStorageMode)b.SerializeBits<int>((int)SPSM, 6, name: nameof(SPSM));
                b.SerializeBits<int>(default, 2, name: "Padding");
                DBP = (ushort)b.SerializeBits<int>(DBP, 14, name: nameof(DBP));
                b.SerializeBits<int>(default, 2, name: "Padding");
                DBW = (ushort)b.SerializeBits<int>(DBW, 6, name: nameof(DBW));
                b.SerializeBits<int>(default, 2, name: "Padding");
                DPSM = (GS.PixelStorageMode)b.SerializeBits<int>((int)DPSM, 6, name: nameof(DPSM));
                b.SerializeBits<int>(default, 2, name: "Padding");
            });
        }
    }
}