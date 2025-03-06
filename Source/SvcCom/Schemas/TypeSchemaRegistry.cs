using System.Collections;
using SvcCom.Utility.Extensions;

namespace SvcCom.Schemas;

public class TypeSchemaRegistry : IEnumerable<TypeSchema>, IEnumerable<TypeSchemaRegistryEntry>
{
    private readonly List<TypeSchemaRegistryEntry> _entries = [];

    public TypeSchema GetOrCreate(Type type)
    {
        TypeSchema? typeSchema = _entries.FirstOrDefault(entry => entry.Schema.Type == type)?.Schema;

        if (typeSchema is not null)
            return typeSchema;

        if (type.IsPrimitive())
            return CreateAndRegisterPrimitiveTypeSchema(type);

        return CreateAndRegisterDefaultTypeSchema(type);
    }

    private TypeSchema CreateAndRegisterPrimitiveTypeSchema(Type type)
    {
        TypeSchema typeSchema = new PrimitiveTypeSchema(type);
        _entries.Add(new TypeSchemaRegistryEntry(typeSchema) { IsScanned = true });
        return typeSchema;
    }

    private TypeSchema CreateAndRegisterDefaultTypeSchema(Type type)
    {
        TypeSchema typeSchema = new TypeSchema(type);
        _entries.Add(new TypeSchemaRegistryEntry(typeSchema));
        return typeSchema;
    }

    IEnumerator<TypeSchemaRegistryEntry> IEnumerable<TypeSchemaRegistryEntry>.GetEnumerator()
        => _entries.GetEnumerator();

    public IEnumerator<TypeSchema> GetEnumerator()
        => _entries.Select(entry => entry.Schema).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();
}