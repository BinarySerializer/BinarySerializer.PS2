namespace BinarySerializer.PS2
{
    public abstract class GSRegister : BinarySerializable
    {
        /// <summary>
        /// Byte identifier of the register
        /// </summary>
        public abstract byte RegisterByte { get; }
    }
}