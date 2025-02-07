using SvcCom.Schemas;

namespace SvcCom.Scanning;

public class Scanner
{
    public static TypeSchema AddServiceSchema(Type type, SchemaRegistry registry)
    {
        TypeSchema? schema = registry.Get(type);

        if (schema == null)
        {
            schema = new TypeSchema(type);
            registry.Add(schema);
        }

        return schema;
    }
}
