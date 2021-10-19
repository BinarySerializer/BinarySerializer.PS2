namespace BinarySerializer.PS2
{
    public class GSReg_TRXPOS : BinarySerializable
    {
        public int SSAX { get; set; }
        public int SSAY { get; set; }
        public int DSAX { get; set; }
        public int DSAY { get; set; }
        public int DIR { get; set; }

        public override void SerializeImpl(SerializerObject s)
        {
            s.SerializeBitValues<ushort>(bitFunc =>
            {
                SSAX = bitFunc(SSAX, 11, name: nameof(SSAX));
                bitFunc(default, 4, name: "Padding");
                SSAY = bitFunc(SSAY, 11, name: nameof(SSAY));
                bitFunc(default, 4, name: "Padding");
                DSAX = bitFunc(DSAX, 11, name: nameof(DSAX));
                bitFunc(default, 4, name: "Padding");
                DSAY = bitFunc(DSAY, 11, name: nameof(DSAY));
                DIR = bitFunc(DIR, 2, name: nameof(DIR));
                bitFunc(default, 2, name: "Padding");
            });
        }
    }
}