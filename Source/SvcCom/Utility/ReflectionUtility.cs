using System.Reflection;

namespace SvcCom.Utility;

public static class ReflectionUtility
{
    public static ValueDetails GetValueDetails(this Type type)
    {
        bool isNullable = false;
        bool isTask = type == typeof(Task) || type == typeof(ValueTask);
        
        if(type.IsGenericType && 
           (type.GetGenericTypeDefinition() == typeof(Task<>)
            || type.GetGenericTypeDefinition() == typeof(ValueTask<>)))
        {
            isTask = true;
            type = type.GetGenericArguments()[0];
        }
        
        if(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            isNullable = true;
            type = Nullable.GetUnderlyingType(type) 
                   ?? throw new InvalidOperationException("Underlying type for Nullable generic is null.");
        }
        
        return new ValueDetails(isTask, isNullable, type);
    }

    public static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        => type.GetProperty(propertyName) 
           ?? throw new ArgumentException($"Property {propertyName} not found on type {type.FullName}");
}