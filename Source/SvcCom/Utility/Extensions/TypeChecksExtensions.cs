namespace SvcCom.Utility.Extensions;

internal static class TypeChecksExtensions
{
    public static bool IsPrimitive(this Type type)
        => type.IsPrimitiveVoid()
        || type.IsPrimitiveEmptyTask()
        || type.IsPrimitiveBool()
        || type.IsPrimitiveNumeric()
        || type.IsPrimitiveString()
        || type.IsPrimitiveGuid();

    public static bool IsPrimitiveVoid(this Type type)
        => type == typeof(void);
    
    public static bool IsPrimitiveEmptyTask(this Type type)
        => type == typeof(Task)
        || type == typeof(ValueTask);
    
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

    public static bool IsNotAllowedForRegistration(this Type type)
        => type.IsNotAllowedForRegistrationByType()
        || type.IsNotAllowedForRegistrationByModifiers();

    public static bool IsNotAllowedForRegistrationByType(this Type type)
        => type == typeof(object)
        || type == typeof(Task<>)
        || type == typeof(ValueTask<>);

    public static bool IsNotAllowedForRegistrationByModifiers(this Type type)
        => !type.IsPublic;

    public static bool IsObject(this Type type)
        => type.IsInterface
        || type.IsClass
        || (type.IsValueType 
            && !type.IsPrimitive() 
            && !type.IsEnum);
}