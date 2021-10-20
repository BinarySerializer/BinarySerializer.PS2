namespace BinarySerializer.PS2
{
    /// <see href="https://psi-rockin.github.io/ps2tek/#giftags">GIFtag documentation</see>
    public class GIFtag : BinarySerializable
    {
        public ushort NLOOP { get; set; }
        public byte EOP { get; set; }
        public byte PRE { get; set; }
        public ushort PRIM { get; set; }
        public DataFormat FLG { get; set; }
        public byte NREG { get; set; }
        public ulong REGS { get; set; }

        public override void SerializeImpl(SerializerObject s)
        {
            s.SerializeBitValues64<ulong>(bitFunc =>
            {
                NLOOP = (ushort)bitFunc(NLOOP, 15, name: nameof(NLOOP));
                EOP = (byte)bitFunc(EOP, 1, name: nameof(EOP));
                bitFunc(default, 30, name: "Padding");
                PRE = (byte)bitFunc(PRE, 1, name: nameof(PRE));
                PRIM = (ushort)bitFunc(PRIM, 11, name: nameof(PRIM));
                FLG = (DataFormat)bitFunc((int)FLG, 2, name: nameof(FLG));
                NREG = (byte)bitFunc(NREG, 4, name: nameof(NREG));
            });
            REGS = s.Serialize<ulong>(REGS, name: nameof(REGS)); // TODO: Parse this properly (4 bits per register field)
        }

        public enum DataFormat
        {
            PACKED,
            REGLIST,
            IMAGE,
            DISABLE
        }
    }
}