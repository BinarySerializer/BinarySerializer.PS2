namespace BinarySerializer.PS2
{
    public class GSReg_BITBLTBUF : GSRegister
    {
        public override GSRegisters RegisterByte => GSRegisters.BITBLTBUF;

        public int SBP { get; set; }
        public int SBW { get; set; }
        public int SPSM { get; set; }
        public int DBP { get; set; }
        public int DBW { get; set; }
        public int DPSM { get; set; }

        public override void SerializeImpl(SerializerObject s)
        {
            s.SerializeBitValues<ushort>(bitFunc =>
            {
                SBP = bitFunc(SBP, 14, name: nameof(SBP));
                bitFunc(default, 2, name: "Padding");
                SBW = bitFunc(SBW, 6, name: nameof(SBW));
                bitFunc(default, 2, name: "Padding");
                SPSM = bitFunc(SPSM, 6, name: nameof(SPSM));
                bitFunc(default, 2, name: "Padding");
                DBP = bitFunc(DBP, 14, name: nameof(DBP));
                bitFunc(default, 2, name: "Padding");
                DBW = bitFunc(DBW, 6, name: nameof(DBW));
                bitFunc(default, 2, name: "Padding");
                DPSM = bitFunc(DPSM, 6, name: nameof(DPSM));
                bitFunc(default, 2, name: "Padding");
            });
        }
    }
}