using SvcCom.Schemas.Types;

namespace SvcCom.Schemas;

public class TypeSchemaRegistryEntry
{
    public TypeSchema Schema { get; }
    public virtual bool IsScanned { get; internal set; }

    public TypeSchemaRegistryEntry(TypeSchema schema)
    {
        Schema = schema;
    }
}