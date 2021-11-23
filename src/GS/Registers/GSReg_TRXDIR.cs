namespace BinarySerializer.PS2
{
    public class GSReg_TRXDIR : GSRegister
    {
        public override GSRegisters RegisterByte => GSRegisters.TRXDIR;

        public TransmissionDirection XDIR { get; set; }

        public override void SerializeRegisterImpl(SerializerObject s)
        {
            s.DoBits<ulong>(b =>
            {
                XDIR = (TransmissionDirection)b.SerializeBits<int>((int)XDIR, 11, name: nameof(XDIR));
                b.SerializeBits<long>(default, 53, name: "Padding");
            });
        }

        public enum TransmissionDirection
        {
            GIFtoVRAM,
            VRAMtoGIF,
            VRAMtoVRAM,
            DEACTIVATED
        }
    }
}