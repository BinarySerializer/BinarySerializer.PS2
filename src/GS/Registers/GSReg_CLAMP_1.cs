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
            s.DoBits<ulong>(b =>
            {
                WMS = (WrapMode)b.SerializeBits<int>((int)WMS, 2, name: nameof(WMS));
                WMT = (WrapMode)b.SerializeBits<int>((int)WMT, 2, name: nameof(WMT));
                MINU = (int)b.SerializeBits<int>(MINU, 10, name: nameof(MINU));
                MAXU = (int)b.SerializeBits<int>(MAXU, 10, name: nameof(MAXU));
                MINV = (int)b.SerializeBits<int>(MINV, 10, name: nameof(MINV));
                MAXV = (int)b.SerializeBits<int>(MAXV, 10, name: nameof(MAXV));
                b.SerializeBits<int>(default, 20, name: "Padding");
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