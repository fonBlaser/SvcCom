using System.Collections;
using SvcCom.Utility.Extensions;

namespace SvcCom.Schemas;

public class TypeSchemaRegistry : IEnumerable<TypeSchemaRegistryEntry>
{
    private readonly List<TypeSchemaRegistryEntry> _entries = [];

    public TypeSchema GetOrCreateSchema(Type type)
        => GetOrCreateEntry(type).Schema;

    public TypeSchemaRegistryEntry GetOrCreateEntry(Type type)
    {
        TypeSchemaRegistryEntry? entry = _entries.FirstOrDefault(entry => entry.Schema.Type == type);

        if (entry is not null)
            return entry;

        entry = type switch
        {
            _ when type.IsPrimitive() => CreateAndRegisterPrimitiveTypeSchema(type),
            _ => CreateAndRegisterDefaultTypeSchema(type)
        };

        _entries.Add(entry);
        return entry;
    }

    private static TypeSchemaRegistryEntry CreateAndRegisterPrimitiveTypeSchema(Type type)
    {
        TypeSchemaRegistryEntry entry = new(new PrimitiveTypeSchema(type))
        {
            IsScanned = true
        };
        return entry;
    }

    private static TypeSchemaRegistryEntry CreateAndRegisterDefaultTypeSchema(Type type) 
        => new(new TypeSchema(type));


    #region Enumerators

    public IEnumerator<TypeSchemaRegistryEntry> GetEnumerator()
        => _entries.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    #endregion
}