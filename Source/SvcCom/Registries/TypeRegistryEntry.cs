using SvcCom.Schemas;

namespace SvcCom.Registries;

public sealed class TypeRegistryEntry
{
    public string TypeFullName { get; }
    public bool IsService { get; }
    public bool Scanned { get; private set; }
    public TypeSchema? Schema { get; private set; }

    public TypeRegistryEntry(string typeFullName, bool isService)
    {
        TypeFullName = typeFullName;
        IsService = isService;
    }

    public void SetSchema(TypeSchema schema)
    {
        Schema = schema;
        Scanned = true;
    }
}