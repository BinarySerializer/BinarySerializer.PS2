namespace BinarySerializer.PS2
{
    public class GSReg_TRXREG : GSRegister
    {
        public override GSRegisters RegisterByte => GSRegisters.TRXREG;

        public int RRW { get; set; }
        public int RRH { get; set; }

        public override void SerializeImpl(SerializerObject s)
        {
            s.SerializeBitValues<ushort>(bitFunc =>
            {
                RRW = bitFunc(RRW, 12, name: nameof(RRW));
                bitFunc(default, 19, name: "Padding");
                RRH = bitFunc(RRH, 12, name: nameof(RRH));
                bitFunc(default, 19, name: "Padding");
            });
        }
    }
}