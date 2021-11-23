namespace BinarySerializer.PS2
{
    public class GSReg_TRXREG : GSRegister
    {
        public override GSRegisters RegisterByte => GSRegisters.TRXREG;

        public ushort RRW { get; set; }
        public ushort RRH { get; set; }

        public override void SerializeRegisterImpl(SerializerObject s)
        {
            s.DoBits<ulong>(b =>
            {
                RRW = (ushort)b.SerializeBits<int>(RRW, 12, name: nameof(RRW));
                b.SerializeBits<int>(default, 20, name: "Padding");
                RRH = (ushort)b.SerializeBits<int>(RRH, 12, name: nameof(RRH));
                b.SerializeBits<int>(default, 20, name: "Padding");
            });
        }
    }
}