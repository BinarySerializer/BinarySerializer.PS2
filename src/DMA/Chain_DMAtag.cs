namespace BinarySerializer.PS2
{
    /// <see href="https://psi-rockin.github.io/ps2tek/#dmacchainmode">DMAtag documentation</see>
    public class Chain_DMAtag : BinarySerializable
    {
        public ushort QWC { get; set; }
        public byte PCE { get; set; }
        public TagID ID { get; set; }
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
                ID = (TagID)bitFunc((int)ID, 3, name: nameof(ID));
                IRQ = (byte)bitFunc(IRQ, 1, name: nameof(IRQ));
                ADDR = (uint)bitFunc(ADDR, 31, name: nameof(ADDR));
                SPR = (byte)bitFunc(SPR, 1, name: nameof(SPR));
            });
        }

        public enum TagID
        {
            REFE_CNTS, // refe for source chain tag, cnts for destination chain tag
            CNT,
            NEXT,
            REF,
            REFS,
            CALL,
            RET,
            END
        }

        public override string ToString() => $"DMATag(TagID: {ID}, QWC: {QWC}, ADDR: {ADDR:X8}, SPR: {SPR}, PCE: {PCE}, IRQ: {IRQ})";
        public override bool UseShortLog => true;
    }
}