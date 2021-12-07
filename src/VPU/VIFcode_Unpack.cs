namespace BinarySerializer.PS2
{
    public class VIFcode_Unpack
    {
        public bool M;
        public UnpackVN VN;
        public UnpackVL VL;
        public uint SIZE;
        public bool FLG;
        public bool USN;
        public uint ADDR;

        public VIFcode_Unpack(VIFcode vifcode)
        {
            M = ((vifcode.CMD) >> 3 & 0x01) == 1; // Bit 4
            VN = (UnpackVN)((vifcode.CMD >> 2) & 0x03); // Bits 2-3
            VL = (UnpackVL)(vifcode.CMD & 0x03); // Bits 0-1
            SIZE = vifcode.NUM;
            ADDR = vifcode.IMMEDIATE & 0x3FF; // Bits 0-9
            USN = ((vifcode.IMMEDIATE >> 13) & 0x01) == 1; // Bit 14
            FLG = ((vifcode.IMMEDIATE >> 14) & 0x01) == 1; // Bit 15
        }

        public override string ToString() {
            return $"{VN}_{VL}, SIZE: {SIZE}, ADDR: {ADDR}, M: {M}, USN: {USN}, FLG: {FLG}";
        }

        public enum UnpackVN
        {
            S,
            V2,
            V3,
            V4
        }

        public enum UnpackVL
        {
            VL_32,
            VL_16,
            VL_8,
            VL_5
        }
    }
}