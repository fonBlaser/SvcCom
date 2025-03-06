using System.Collections;

namespace SvcCom.Schemas;

public class TypeSchemaRegistry : IEnumerable<TypeSchema>, IEnumerable<TypeSchemaRegistryEntry>
{
    private readonly List<TypeSchemaRegistryEntry> _entries = [];

    public TypeSchema GetOrCreate(Type type)
    {
        TypeSchema? typeSchema = _entries.FirstOrDefault(entry => entry.Schema.Type == type)?.Schema;

        if (typeSchema is null)
        {
            typeSchema = new TypeSchema(type);
            _entries.Add(new TypeSchemaRegistryEntry(typeSchema));
        }

        return typeSchema;
    }

    public void CreateOrThrow(Type type)
    {
        if (_entries.Any(entry => entry.Schema.Type == type))
            throw new InvalidOperationException($"Type '{type.FullName}' already added to the registry.");

        _entries.Add(new TypeSchemaRegistryEntry(new TypeSchema(type)));
    }

    IEnumerator<TypeSchemaRegistryEntry> IEnumerable<TypeSchemaRegistryEntry>.GetEnumerator()
        => _entries.GetEnumerator();

    public IEnumerator<TypeSchema> GetEnumerator()
        => _entries.Select(entry => entry.Schema).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}

public class TypeSchemaRegistryEntry
{
    public TypeSchema Schema { get; }

    public bool ArePropertiesDefined { get; internal set; }
    public bool AreMethodsDefined { get; internal set; }

    public bool IsScanned => ArePropertiesDefined && AreMethodsDefined;

    public TypeSchemaRegistryEntry(TypeSchema schema)
    {
        Schema = schema;
    }
}