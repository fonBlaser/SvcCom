using System.Collections;

namespace SvcCom.Schemas;

public class TypeSchemaRegistry : IEnumerable<TypeSchema>
{
    private List<TypeSchema> _types = new();

    public TypeSchema GetOrCreate(Type type)
    {
        TypeSchema? typeSchema = _types.FirstOrDefault(schema => schema.Type == type);

        if (typeSchema is null)
        {
            typeSchema = new TypeSchema(type);
            _types.Add(typeSchema);
        }

        return typeSchema;
    }
    
    public IEnumerator<TypeSchema> GetEnumerator()
        => _types.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    public void CreateOrThrow(Type type)
    {
        if (_types.Any(schema => schema.Type == type))
            throw new InvalidOperationException($"Type '{type.FullName}' already added to the registry.");
        
        _types.Add(new TypeSchema(type));
    }
}

public sealed class TypeSchema
{
    public Type Type { get; }
    public string FullName => Type.FullName;

    public TypeSchema(Type type)
    {
        Type = type;
    }
}