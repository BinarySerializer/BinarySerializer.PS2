namespace BinarySerializer.PS2
{
    /// <see href="https://psi-rockin.github.io/ps2tek/#vifcommands">VIFcode documentation</see>
    public class VIFcode : BinarySerializable
    {
        public uint IMMEDIATE { get; set; }
        public uint NUM { get; set; }
        public uint CMD { get; set; }
        public bool Stall { get; set; }

        public override void SerializeImpl(SerializerObject s)
        {
            s.SerializeBitValues<int>(bitFunc =>
            {
                IMMEDIATE = (uint)bitFunc((int)IMMEDIATE, 16, name: nameof(IMMEDIATE));
                NUM = (uint)bitFunc((int)NUM, 8, name: nameof(NUM));
                CMD = (uint)bitFunc((int)CMD, 7, name: nameof(CMD));
                Stall = bitFunc(Stall ? 1 : 0, 1, name: nameof(Stall)) == 1;
            });
        }


    }
}