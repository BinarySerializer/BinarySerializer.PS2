namespace BinarySerializer.PS2
{
    /// <see href="https://psi-rockin.github.io/ps2tek/#vifcommands">VIFcode documentation</see>
    public class VIFcode : BinarySerializable
    {
        public ushort IMMEDIATE { get; set; }
        public byte NUM { get; set; }
        public byte CMD { get; set; }
        public bool Stall { get; set; }

        public override void SerializeImpl(SerializerObject s)
        {
            s.SerializeBitValues<uint>(bitFunc =>
            {
                IMMEDIATE = (ushort)bitFunc(IMMEDIATE, 16, name: nameof(IMMEDIATE));
                NUM = (byte)bitFunc(NUM, 8, name: nameof(NUM));
                CMD = (byte)bitFunc(CMD, 7, name: nameof(CMD));
                Stall = bitFunc(Stall ? 1 : 0, 1, name: nameof(Stall)) == 1;
            });
        }
    }
}