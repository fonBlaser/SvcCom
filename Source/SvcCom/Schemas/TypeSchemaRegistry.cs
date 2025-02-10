namespace SvcCom.Schemas;

public class TypeSchemaRegistry
{
    private readonly List<TypeSchema> _schemas = new();
    public IReadOnlyCollection<TypeSchema> Schemas => _schemas.AsReadOnly();

    public void Add(TypeSchema schema)
    {
        if (_schemas.Any(s => s.Type == schema.Type))
            throw new InvalidOperationException($"Duplicate type schema for {schema.Type}");
        
        _schemas.Add(schema);
    }

    public TypeSchema GetOrCreate(Type type)
    {
        TypeSchema? schema = _schemas.FirstOrDefault(s => s.Type == type);

        if (schema == null)
        {
            schema = new TypeSchema(type);
            Add(schema);
        }

        return schema;
    }
}