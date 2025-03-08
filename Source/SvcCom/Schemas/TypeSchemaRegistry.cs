using System.Collections;
using SvcCom.Schemas.Types;
using SvcCom.Utility.Extensions;

namespace SvcCom.Schemas;

public class TypeSchemaRegistry : IEnumerable<TypeSchemaRegistryEntry>
{
    private readonly List<TypeSchemaRegistryEntry> _entries = [];

    public TypeSchema GetOrCreateSchema(Type type)
        => GetOrCreateEntry(type).Schema;

    public TypeSchemaRegistryEntry GetOrCreateEntry(Type type)
    {
        if(type.IsNotAllowedForRegistration())
            throw new TypeAccessException($"Type {type.FullName} is not allowed for registration.");
        
        TypeSchemaRegistryEntry? entry = _entries.FirstOrDefault(entry => entry.Schema.Type == type);

        if (entry is not null)
            return entry;

        entry = type switch
        {
            _ when type.IsPrimitive() => CreatePrimitiveTypeSchema(type),
            _ when type.IsEnum        => CreateEnumTypeSchema(type),
            _ when type.IsObject()    => CreateObjectTypeSchema(type),
            _                         => CreateDefaultTypeSchema(type)
        };

        _entries.Add(entry);
        return entry;
    }

    private static TypeSchemaRegistryEntry CreatePrimitiveTypeSchema(Type type)
    {
        TypeSchemaRegistryEntry entry = new(new PrimitiveTypeSchema(type))
        {
            IsScanned = true
        };
        return entry;
    }

    private static TypeSchemaRegistryEntry CreateEnumTypeSchema(Type type)
        => new(new EnumTypeSchema(type));

    private static TypeSchemaRegistryEntry CreateDefaultTypeSchema(Type type) 
        => new(new(type));
    
    private static TypeSchemaRegistryEntry CreateObjectTypeSchema(Type type)
        => new(new ObjectTypeSchema(type));


    #region Enumerators

    public IEnumerator<TypeSchemaRegistryEntry> GetEnumerator()
        => _entries.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    #endregion
}