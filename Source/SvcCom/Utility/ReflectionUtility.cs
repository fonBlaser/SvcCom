using System.Reflection;

namespace SvcCom.Utility;

public static class ReflectionUtility
{
    public static ValueDetails GetValueDetails(this PropertyInfo propertyInfo)
    {
        if (propertyInfo is null)
            throw new ArgumentNullException(nameof(propertyInfo));

        Type propertyType = propertyInfo.PropertyType;
        bool isNullable = false;
        bool isTask = false;
        
        if(propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Task<>))
        {
            isTask = true;
            propertyType = propertyType.GetGenericArguments()[0];
        }
        
        if(propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            isNullable = true;
            propertyType = Nullable.GetUnderlyingType(propertyType) 
                           ?? throw new InvalidOperationException("Underlying type for Nullable generic is null.");
        }
        
        return new ValueDetails(isTask, isNullable, propertyType);
    }

    public static PropertyInfo GetPropertyInfo(Type type, string propertyName)
        => type.GetProperty(propertyName) 
           ?? throw new ArgumentException($"Property {propertyName} not found on type {type.FullName}");
}