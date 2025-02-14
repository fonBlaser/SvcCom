using System.Reflection;
using SvcCom.Utility;

namespace SvcCom.Schemas;

public class PropertySchema : NamedValueSchema
{
    public bool IsTask { get; }
    
    public PropertySchema(string name, TypeSchema schema, bool isNullable, bool isTask) 
        : base(name, schema, isNullable)
    {
        IsTask = isTask;
    }
    
    public static PropertySchema Create(PropertyInfo propertyInfo, TypeSchemaRegistry registry)
    {
        if(propertyInfo is null)
            throw new ArgumentNullException(nameof(propertyInfo));

        ValueDetails propValueDetails = propertyInfo.GetValueDetails();
        TypeSchema propTypeSchema = registry.GetOrCreate(propValueDetails.ValueType);
        
        return new PropertySchema(propertyInfo.Name, propTypeSchema, propValueDetails.IsNullable, propValueDetails.IsTask);
    }
}