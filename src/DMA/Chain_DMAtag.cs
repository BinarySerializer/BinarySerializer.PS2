namespace BinarySerializer.PS2
{
    public class Chain_DMAtag : BinarySerializable
    {
        public ushort QWC { get; set; }
        public byte PCE { get; set; }
        public byte ID { get; set; }
        public byte IRQ { get; set; }
        public uint ADDR { get; set; }
        public byte SPR { get; set; }

        public override void SerializeImpl(SerializerObject s)
        {
            s.SerializeBitValues64<ulong>(bitFunc =>
            {
                QWC = (ushort)bitFunc(QWC, 16, name: nameof(QWC));
                bitFunc(default, 10, name: "Padding");
                PCE = (byte)bitFunc(PCE, 2, name: nameof(PCE));
                ID = (byte)bitFunc(ID, 3, name: nameof(ID));
                IRQ = (byte)bitFunc(IRQ, 1, name: nameof(IRQ));
                ADDR = (uint)bitFunc(ADDR, 31, name: nameof(ADDR));
                SPR = (byte)bitFunc(SPR, 1, name: nameof(SPR));
            });
        }
    }
}