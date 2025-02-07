using SvcCom.Schemas;

namespace SvcCom.Scanning;

public class Scanner
{
    private readonly SchemaRegistry _registry;

    public Scanner(SchemaRegistry registry)
    {
        _registry = registry ?? throw new ArgumentNullException(nameof(registry));
    }

    public TypeSchema AddServiceSchema(Type type)
    {
        TypeSchema? schema = _registry.Get(type);

        if (schema == null)
        {
            schema = new TypeSchema(type);
            _registry.Add(schema);
        }

        return schema;
    }
}
