namespace BinarySerializer.PS2
{
    public abstract class GSRegister : BinarySerializable
    {
        /// <summary>
        /// Byte identifier of the register
        /// </summary>
        public abstract GSRegisters RegisterByte { get; }
    }

    public enum GSRegisters : byte
    {
        PRIM,
        RGBAQ,
        ST,
        UV,
        XYZF2,
        XYZ2,
        TEX0_1,
        TEX0_2,
        CLAMP_1,
        CLAMP_2,
        FOG,
        XYZF3 = 0x0C,
        XYZ3,
        TEX1_1 = 0x14,
        TEX1_2,
        TEX2_1,
        TEX2_2,
        XYOFFSET_1,
        XYOFFSET_2,
        PRMODECONT,
        PRMODE,
        TEXCLUT,
        SCANMSK = 0x22,
        MIPTBP1_1 = 0x34,
        MIPTBP1_2,
        MIPTBP2_1,
        MIPTBP2_2,
        TEXA = 0x3B,
        FOGCOL = 0x3D,
        TEXFLUSH = 0x3F,
        SCISSOR_1,
        SCISSOR_2,
        ALPHA_1,
        ALPHA_2,
        DIMX,
        DTHE,
        COLCLAMP,
        TEST_1,
        TEST2,
        PABE,
        FBA_1,
        FBA_2,
        FRAME_1,
        FRAME_2,
        ZBUF_1,
        ZBUF_2,
        BITBLTBUF,
        TRXPOS,
        TRXREG,
        TRXDIR,
        HWREG,
        SIGNAL = 0x60,
        FINISH,
        LABEL
    }
}