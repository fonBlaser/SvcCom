namespace SvcCom.Utility.Extensions;

public static class TypeValueDetailsExtensions
{
    public static ValueDetails GetValueDetails(this Type type)
    {
        Type valueType = type;
        bool isNullable = false;
        bool isTask = false;


        if (valueType.IsNullable())
        {
            isNullable = true;
            valueType = Nullable.GetUnderlyingType(valueType)
                        ?? throw new InvalidOperationException("Underlying type for Nullable generic is null.");
        }

        if (valueType.IsTask())
        {
            isTask = true;
            valueType = valueType.GetTaskResultType();
        }

        if (valueType.IsNullable())
        {
            isNullable = true;
            valueType = Nullable.GetUnderlyingType(valueType)
                        ?? throw new InvalidOperationException("Underlying type for Nullable generic is null.");
        }

        return new(valueType, isNullable, isTask);
    }

    public static bool IsNullable(this Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    public static bool IsTask(this Type type)
    {
        if (type == typeof(Task) || type == typeof(ValueTask))
            return true;

        if (type.IsGenericType &&
            (type.GetGenericTypeDefinition() == typeof(Task<>)
             || type.GetGenericTypeDefinition() == typeof(ValueTask<>)))
        {
            return true;
        }

        return false;
    }

    public static Type GetTaskResultType(this Type type)
    {
        if (type.IsGenericType &&
            (type.GetGenericTypeDefinition() == typeof(Task<>)
             || type.GetGenericTypeDefinition() == typeof(ValueTask<>)))
        {
            return type.GetGenericArguments()[0];
        }

        return typeof(void);
    }
}