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

        public override void SerializeImpl(SerializerObject s)
        {
            s.SerializeBitValues64<ulong>(bitFunc =>
            {
                SBP = (ushort)bitFunc(SBP, 14, name: nameof(SBP));
                bitFunc(default, 2, name: "Padding");
                SBW = (ushort)bitFunc(SBW, 6, name: nameof(SBW));
                bitFunc(default, 2, name: "Padding");
                SPSM = (GS.PixelStorageMode)bitFunc((int)SPSM, 6, name: nameof(SPSM));
                bitFunc(default, 2, name: "Padding");
                DBP = (ushort)bitFunc(DBP, 14, name: nameof(DBP));
                bitFunc(default, 2, name: "Padding");
                DBW = (ushort)bitFunc(DBW, 6, name: nameof(DBW));
                bitFunc(default, 2, name: "Padding");
                DPSM = (GS.PixelStorageMode)bitFunc((int)DPSM, 6, name: nameof(DPSM));
                bitFunc(default, 2, name: "Padding");
            });
        }
    }
}