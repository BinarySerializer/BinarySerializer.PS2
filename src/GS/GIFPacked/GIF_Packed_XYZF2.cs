namespace BinarySerializer.PS2
{
    public class GIF_Packed_XYZF2 : BinarySerializable
    {
        public FixedPointUInt16 X { get; set; }
        public FixedPointUInt16 Y { get; set; }
        public uint Z { get; set; }
        public byte F { get; set; }
        public bool DisableDrawing { get; set; }

        public override void SerializeImpl(SerializerObject s)
        {
            X = s.SerializeObject<FixedPointUInt16>(X, onPreSerialize: o => o.Pre_PointPosition = 4, name: nameof(X));
            s.Align(4, Offset);
            Y = s.SerializeObject<FixedPointUInt16>(Y, onPreSerialize: o => o.Pre_PointPosition = 4, name: nameof(Y));
            s.Align(4, Offset);
            s.DoBits<long>(b => {
                b.SerializePadding(4);
                Z = b.SerializeBits<uint>(Z, 24, name: nameof(Z));
                b.SerializePadding(8);
                F = b.SerializeBits<byte>(F, 8, name: nameof(F));
                b.SerializePadding(2);
                DisableDrawing = b.SerializeBits<bool>(DisableDrawing, 1, name: nameof(DisableDrawing));
            });
        }
    }
}