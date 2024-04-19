using System;

namespace BinarySerializer.PS2
{
    public static class PS2Color
    {
        /// <summary>
        /// An RGBA8888 color on PS2 where the max alpha value is 128 (0x80) rather than 255
        /// </summary>
        public static SerializableColor RGBA8888(SerializerObject s, SerializableColor obj)
        {
            byte r = s.Serialize<byte>(SerializableColorHelpers.ToByte(obj.Red, 8), name: nameof(SerializableColor.Red));
            byte g = s.Serialize<byte>(SerializableColorHelpers.ToByte(obj.Green, 8), name: nameof(SerializableColor.Green));
            byte b = s.Serialize<byte>(SerializableColorHelpers.ToByte(obj.Blue, 8), name: nameof(SerializableColor.Blue));
            byte a = s.Serialize<byte>((byte)Math.Round(obj.Alpha * 128), name: nameof(SerializableColor.Alpha));

            return new SerializableColor(
                red: SerializableColorHelpers.ToFloat(r, 8),
                green: SerializableColorHelpers.ToFloat(g, 8),
                blue: SerializableColorHelpers.ToFloat(b, 8),
                alpha: a / 128f);
        }
    }
}