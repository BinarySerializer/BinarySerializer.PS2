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
            s.SerializeBitValues64<ulong>(bitFunc =>
            {
                SSAX = (ushort)bitFunc(SSAX, 11, name: nameof(SSAX));
                bitFunc(default, 5, name: "Padding");
                SSAY = (ushort)bitFunc(SSAY, 11, name: nameof(SSAY));
                bitFunc(default, 5, name: "Padding");
                DSAX = (ushort)bitFunc(DSAX, 11, name: nameof(DSAX));
                bitFunc(default, 5, name: "Padding");
                DSAY = (ushort)bitFunc(DSAY, 11, name: nameof(DSAY));
                DIR = (TransmissionOrder)bitFunc((int)DIR, 2, name: nameof(DIR));
                bitFunc(default, 3, name: "Padding");
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