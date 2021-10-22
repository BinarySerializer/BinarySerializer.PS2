namespace BinarySerializer.PS2
{
    public class GSReg_CLAMP_1 : GSRegister
    {
        public override GSRegisters RegisterByte => GSRegisters.CLAMP_1;

        public WrapMode WMS { get; set; }
        public WrapMode WMT { get; set; }
        public int MINU { get; set; }
        public int MAXU { get; set; }
        public int MINV { get; set; }
        public int MAXV { get; set; }
        
        public override void SerializeRegisterImpl(SerializerObject s)
        {
            s.SerializeBitValues64<ulong>(bitFunc =>
            {
                WMS = (WrapMode)bitFunc((int)WMS, 2, name: nameof(WMS));
                WMT = (WrapMode)bitFunc((int)WMT, 2, name: nameof(WMT));
                MINU = (int)bitFunc(MINU, 10, name: nameof(MINU));
                MAXU = (int)bitFunc(MAXU, 10, name: nameof(MAXU));
                MINV = (int)bitFunc(MINV, 10, name: nameof(MINV));
                MAXV = (int)bitFunc(MAXV, 10, name: nameof(MAXV));
                s.SerializePadding(20);
            });
        }
    }

    public enum WrapMode
    {
        REPEAT,
        CLAMP,
        REGION_CLAMP,
        REGION_REPEAT
    }
}