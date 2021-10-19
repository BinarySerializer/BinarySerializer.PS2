namespace BinarySerializer.PS2
{
    public class GSReg_TRXDIR : BinarySerializable
    {
        public int XDIR { get; set; }

        public override void SerializeImpl(SerializerObject s)
        {
            s.SerializeBitValues<ushort>(bitFunc =>
            {
                XDIR = bitFunc(XDIR, 11, name: nameof(XDIR));
                bitFunc(default, 53, name: "Padding");
            });
        }
    }
}