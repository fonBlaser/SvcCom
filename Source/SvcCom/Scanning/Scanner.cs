using System.Reflection;
using SvcCom.Config;
using SvcCom.Schemas;

namespace SvcCom.Scanning;

public class Scanner
{
    internal ScannerConfig Config { get; }
    public SchemaRegistry Registry { get; }

    public Scanner(ScannerConfig config, SchemaRegistry registry)
    {
        Config = config ?? throw new ArgumentNullException(nameof(config));
        Registry = registry ?? throw new ArgumentNullException(nameof(registry));
    }

    // ToDo: Add IsScanned internal flag to TypeSchema
    public TypeSchema GetOrCreateTypeSchema(Type type)
        => Registry.GetOrCreate(type);

    public void AddProperties(TypeSchema schema)
        => GetOrCreateSchemasForProperties(schema.Type)
            .ToList()
            .ForEach(schema.AddProperty);
    
    public IEnumerable<NamedValueSchema> GetOrCreateSchemasForProperties(Type type)
        => GetSuitableProperties(type)
            .Select(GetPropertySchemaInfo)
            .Select(p 
                => new NamedValueSchema(
                    p.Name, 
                    GetOrCreateTypeSchema(p.PropertyValueType), 
                    p.IsNullable)
            );
    
    public (string Name, bool IsNullable, Type PropertyValueType) GetPropertySchemaInfo(PropertyInfo property)
    {
        bool isNullable = false;
        Type propertyType = property.PropertyType;
        
        if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
        {
            isNullable = true;
            propertyType = propertyType.GetGenericArguments().First();
        }
        
        return (property.Name, isNullable, propertyType);
    }
    
    public IEnumerable<PropertyInfo> GetSuitableProperties(Type type)
        => type.GetProperties()
               .Where(IsPropertySuitable);
    
    public bool IsPropertySuitable(PropertyInfo? property)
        => property != null
           && property.GetMethod != null
           && property.GetMethod.IsPublic;
}
