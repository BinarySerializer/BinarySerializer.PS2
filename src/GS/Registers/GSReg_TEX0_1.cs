namespace BinarySerializer.PS2
{
    public class GSReg_TEX0_1 : GSRegister
    {
        public override GSRegisters RegisterByte => GSRegisters.TEX0_1;

        public ushort TPB0 { get; set; }
        public byte TBW { get; set; }
        public GS.PixelStorageMode PSM { get; set; }
        public byte TW { get; set; }
        public byte TH { get; set; }
        public bool TCC { get; set; }
        public GS.TextureFunction TFX { get; set; }
        public ushort CBP { get; set; }
        public GS.CLUTPixelStorageMode CPSM { get; set; }
        public GS.ColorStorageMode CSM { get; set; }
        public byte CSA { get; set; }
        public byte CLD { get; set; }

        public override void SerializeRegisterImpl(SerializerObject s)
        {
            s.DoBits<ulong>(b =>
            {
                TPB0 = (ushort)b.SerializeBits<int>(TPB0, 14, name: nameof(TPB0));
                TBW = (byte)b.SerializeBits<int>(TBW, 6, name: nameof(TBW));
                PSM = (GS.PixelStorageMode)b.SerializeBits<int>((int)PSM, 6, name: nameof(PSM));
                TW = (byte)b.SerializeBits<int>(TW, 4, name: nameof(TW));
                TH = (byte)b.SerializeBits<int>(TH, 4, name: nameof(TH));
                TCC = b.SerializeBits<int>(TCC ? 1 : 0, 1, name: nameof(TCC)) == 1;
                TFX = (GS.TextureFunction)b.SerializeBits<int>((int)TFX, 2, name: nameof(TFX));
                CBP = (ushort)b.SerializeBits<int>(CBP, 14, name: nameof(CBP));
                CPSM = (GS.CLUTPixelStorageMode)b.SerializeBits<int>((int)CPSM, 4, name: nameof(CPSM));
                CSM = (GS.ColorStorageMode)b.SerializeBits<int>((int)CSM, 1, name: nameof(CSM));
                CSA = (byte)b.SerializeBits<int>(CSA, 5, name: nameof(CSA));
                CLD = (byte)b.SerializeBits<int>(CLD, 3, name: nameof(CLD));
            });
        }
    }
}