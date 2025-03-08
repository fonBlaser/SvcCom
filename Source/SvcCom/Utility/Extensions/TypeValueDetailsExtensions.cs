using System.Collections.ObjectModel;
using System.Reflection;

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

    public static bool IsNullable(this PropertyInfo property) =>
        IsNullableHelper(property.PropertyType, property.DeclaringType, property.CustomAttributes);

    public static bool IsNullable(this ParameterInfo parameter) =>
        IsNullableHelper(parameter.ParameterType, parameter.Member, parameter.CustomAttributes);

    private static bool IsNullableHelper(Type memberType, MemberInfo? declaringType,
        IEnumerable<CustomAttributeData> customAttributes)
    {
        if (memberType.IsValueType)
            return Nullable.GetUnderlyingType(memberType) != null;

        var nullable = customAttributes
            .FirstOrDefault(x => x.AttributeType.FullName == "System.Runtime.CompilerServices.NullableAttribute");
        if (nullable != null && nullable.ConstructorArguments.Count == 1)
        {
            var attributeArgument = nullable.ConstructorArguments[0];
            if (attributeArgument.ArgumentType == typeof(byte[]))
            {
                var args = (ReadOnlyCollection<CustomAttributeTypedArgument>)attributeArgument.Value!;
                if (args.Count > 0 && args[0].ArgumentType == typeof(byte))
                {
                    return (byte)args[0].Value! == 2;
                }
            }
            else if (attributeArgument.ArgumentType == typeof(byte))
            {
                return (byte)attributeArgument.Value! == 2;
            }
        }

        for (var type = declaringType; type != null; type = type.DeclaringType)
        {
            var context = type.CustomAttributes
                .FirstOrDefault(x =>
                    x.AttributeType.FullName == "System.Runtime.CompilerServices.NullableContextAttribute");
            if (context != null &&
                context.ConstructorArguments.Count == 1 &&
                context.ConstructorArguments[0].ArgumentType == typeof(byte))
            {
                return (byte)context.ConstructorArguments[0].Value! == 2;
            }
        }

        // Couldn't find a suitable attribute
        return false;
    }
}