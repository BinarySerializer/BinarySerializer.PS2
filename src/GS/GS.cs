namespace BinarySerializer.PS2
{
    public class GS
    {
        public class VRAM
        {
            public byte[] VRAM_Bytes { get; set; }

            /// <summary>
            /// Creates a new PS2 Graphics Synthesizer VRAM object
            /// </summary>
            public VRAM()
            {
                VRAM_Bytes = new byte[1024 * 1024 * 4]; // 4 MB
            }

            // TODO: Add functions for writing and reading image data
        }

        /// <see href="https://openkh.dev/common/tm2.html#psm-register-pixel-storage-mode">PSM documentation</see>
        public enum PixelStorageMode
        {
            PSMCT32 = 0x00,
            PSMCT24 = 0x01,
            PSMCT16 = 0x02,
            PSMCT16S = 0x0A,
            PSMT8 = 0x13,
            PSMT4 = 0x14,
            PSMT8H = 0x1B,
            PSMT4HL = 0x24,
            PSMT4HH = 0x2C,
            PSMZ32 = 0x30,
            PSMZ24 = 0x31,
            PSMZ16 = 0x32,
            PSMZ16S = 0x3A
        }
    }
}