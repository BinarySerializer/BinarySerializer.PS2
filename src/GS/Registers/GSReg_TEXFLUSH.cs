namespace BinarySerializer.PS2
{
    public class GSReg_TEXFLUSH : GSRegister
    {
        public override GSRegisters RegisterByte => GSRegisters.TEXFLUSH;

        public override void SerializeRegisterImpl(SerializerObject s)
        {
            s.SerializeBitValues64<ulong>(bitFunc =>
            {
                bitFunc(default, 64, name: "Padding");
            });
        }
    }
}