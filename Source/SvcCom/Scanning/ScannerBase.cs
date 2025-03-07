using SvcCom.Schemas;

namespace SvcCom.Scanning;

public abstract class ScannerBase
{
    public TypeSchemaRegistry Registry { get; }

    protected ScannerBase(TypeSchemaRegistry registry)
    {
        Registry = registry ?? throw new ArgumentNullException(nameof(registry));
    }
}