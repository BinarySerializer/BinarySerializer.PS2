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
        }
    }
}