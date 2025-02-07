namespace SvcCom.Schemas;

public class SchemaRegistry
{
    private readonly List<TypeSchema> _schemas = new();
    public IReadOnlyCollection<TypeSchema> Types => _schemas.AsReadOnly();

    public void Add(TypeSchema schema)
    {
        if (_schemas.Any(s => s.Type == schema.Type))
            throw new InvalidOperationException($"Duplicate type schema for {schema.Type}");
        
        _schemas.Add(schema);
    }

    public TypeSchema? Get(Type type)
        => _schemas.FirstOrDefault(s => s.Type == type);

    public TypeSchema GetOrCreate(Type type)
    {
        TypeSchema? schema = Get(type);

        if (schema == null)
        {
            schema = new TypeSchema(type);
            Add(schema);
        }

        return schema;
    }
}