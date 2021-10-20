namespace BinarySerializer.PS2
{
    public class GSReg_TEXFLSUH : GSRegister
    {
        public override GSRegisters RegisterByte => GSRegisters.TEXFLUSH;

        public override void SerializeImpl(SerializerObject s)
        {
            s.SerializeBitValues64<ulong>(bitFunc =>
            {
                s.SerializePadding(64);
            });
        }
    }
}