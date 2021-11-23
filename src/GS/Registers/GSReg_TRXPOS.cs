namespace BinarySerializer.PS2
{
    public class GSReg_TRXPOS : GSRegister
    {
        public override GSRegisters RegisterByte => GSRegisters.TRXPOS;

        public ushort SSAX { get; set; }
        public ushort SSAY { get; set; }
        public ushort DSAX { get; set; }
        public ushort DSAY { get; set; }
        public TransmissionOrder DIR { get; set; }

        public override void SerializeRegisterImpl(SerializerObject s)
        {
            s.DoBits<ulong>(b =>
            {
                SSAX = (ushort)b.SerializeBits<int>(SSAX, 11, name: nameof(SSAX));
                b.SerializeBits<int>(default, 5, name: "Padding");
                SSAY = (ushort)b.SerializeBits<int>(SSAY, 11, name: nameof(SSAY));
                b.SerializeBits<int>(default, 5, name: "Padding");
                DSAX = (ushort)b.SerializeBits<int>(DSAX, 11, name: nameof(DSAX));
                b.SerializeBits<int>(default, 5, name: "Padding");
                DSAY = (ushort)b.SerializeBits<int>(DSAY, 11, name: nameof(DSAY));
                DIR = (TransmissionOrder)b.SerializeBits<int>((int)DIR, 2, name: nameof(DIR));
                b.SerializeBits<int>(default, 3, name: "Padding");
            });
        }

        public enum TransmissionOrder
        {
            UpperLeft_LowerRight,
            LowerLeft_UpperRight,
            UpperRight_LowerLeft,
            LowerRight_UpperLeft
        }
    }
}