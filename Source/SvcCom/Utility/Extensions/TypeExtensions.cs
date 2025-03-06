namespace SvcCom.Utility.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsPrimitive(this Type type)
            => type.IsPrimitiveNumeric();

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
    }
}
