using System.Reflection;
using SvcCom.Schemas;
using SvcCom.Schemas.ObjectComponents;
using SvcCom.Schemas.Types;
using SvcCom.Schemas.Values;
using SvcCom.Utility;

namespace SvcCom.Scanning;

public class PropertyScanner : ScannerBase
{
    public PropertyScanner(TypeSchemaRegistry registry) 
        : base(registry)
    {
    }

    public List<PropertyInfo> GetAvailableProperties(Type type)
        => type.GetProperties()
               .Where(p => p.GetMethod?.IsPublic == true 
                                  || p.SetMethod?.IsPublic == true)
               .ToList();

    public PropertySchema CreateSchema(PropertyInfo property)
    {
        bool canGet = property.GetMethod?.IsPublic == true;
        bool canSet = property.SetMethod?.IsPublic == true;
        
        ValueDetails valueDetails = property.PropertyType.GetValueDetails();
        TypeSchema valueTypeSchema = Registry.GetOrCreateSchema(property.PropertyType);
        ValueSchema valueSchema = new ValueSchema(valueTypeSchema, valueDetails.IsNullable, valueDetails.IsTask);
        
        return new PropertySchema(property.Name, valueSchema, canGet, canSet);
    }
}