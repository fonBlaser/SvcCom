namespace SvcCom.Utility.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsPrimitive(this Type type)
            => type.IsPrimitiveBool()
            || type.IsPrimitiveNumeric()
            || type.IsPrimitiveString()
            || type.IsPrimitiveGuid();

        public static bool IsPrimitiveBool(this Type type)
            => type == typeof(bool);

        public static bool IsPrimitiveNumeric(this Type type)
            => type == typeof(sbyte)
            || type == typeof(short)
            || type == typeof(int)
            || type == typeof(long)
            || type == typeof(byte)
            || type == typeof(ushort)
            || type == typeof(uint)
            || type == typeof(ulong)
            || type == typeof(float)
            || type == typeof(double)
            || type == typeof(decimal)
            || type == typeof(Int128)
            || type == typeof(UInt128);

        public static bool IsPrimitiveString(this Type type)
            => type == typeof(char)
            || type == typeof(string);

        public static bool IsPrimitiveGuid(this Type type)
            => type == typeof(Guid);
    }
}
