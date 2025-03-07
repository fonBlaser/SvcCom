using SvcCom.Schemas;

namespace SvcCom.Tests.Unit;

public class TypeSchemaRegistryTestBase : TestBase
{
    protected TypeSchemaRegistry Registry { get; }

    protected TypeSchemaRegistryTestBase()
    {
        Registry = new();
    }
}