using SvcCom.Scanning;

namespace SvcCom.Tests.Unit;

public class TypeSchemaScannerTestBase : TypeSchemaRegistryTestBase
{
    protected TypeSchemaScanner Scanner { get; }

    public TypeSchemaScannerTestBase()
    {
        Scanner = new(Registry);
    }
}