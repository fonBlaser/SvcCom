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

    public TypeSchema AddTypeSchema(Type type)
    {
        TypeSchema? schema = Registry.Get(type);

        if (schema == null)
        {
            schema = new TypeSchema(type);
            Registry.Add(schema);
        }

        return schema;
    }

    public TypeSchema AddProperties(TypeSchema schema)
    {
        schema.Type.GetProperties()
            .Where(p => 
                        p.GetMethod != null
                        && p.GetMethod.IsPublic)
            .ToList()
            .ForEach(p =>
            {
                bool isNullable = false;
                Type propertyType = p.PropertyType;
                if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    isNullable = true;
                    propertyType = propertyType.GetGenericArguments().First();
                }
                
                TypeSchema propertyTypeSchema = AddTypeSchema(propertyType);
                NamedValueSchema valueSchema = new NamedValueSchema(p.Name, propertyTypeSchema, isNullable);
                schema.AddProperty(valueSchema);
            });

        return schema;
    }
}
