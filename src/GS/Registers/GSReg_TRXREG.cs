namespace BinarySerializer.PS2
{
    public class GSReg_TRXREG : GSRegister
    {
        public override GSRegisters RegisterByte => GSRegisters.TRXREG;

        public ushort RRW { get; set; }
        public ushort RRH { get; set; }

        public override void SerializeImpl(SerializerObject s)
        {
            s.SerializeBitValues64<ulong>(bitFunc =>
            {
                RRW = (ushort)bitFunc(RRW, 12, name: nameof(RRW));
                bitFunc(default, 20, name: "Padding");
                RRH = (ushort)bitFunc(RRH, 12, name: nameof(RRH));
                bitFunc(default, 20, name: "Padding");
            });
        }
    }
}