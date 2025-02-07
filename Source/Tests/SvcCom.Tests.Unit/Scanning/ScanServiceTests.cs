using SvcCom.Samples.SampleWiki;
using SvcCom.Scanning;
using SvcCom.Schemas;
using Xunit;

namespace SvcCom.Tests.Unit.Scanning;

[Trait("Category", "Unit")]
public class ScanServiceTests
{
    private SchemaRegistry Registry { get; }
    private Scanner Scanner { get; }

    public ScanServiceTests()
    {
        Registry = new SchemaRegistry();
        Scanner = new Scanner(Registry);
    }
    
    [Fact]
    public void AddServiceSchema_AddsInterface_ToTypeRegistry()
    {
        TypeSchema schema = Scanner.AddServiceSchema(typeof(IWiki));

        Assert.NotNull(schema);
        Assert.Single(Registry.Types);
        Assert.Same(schema, Registry.Types.First());
    }
    
    [Fact]
    public void AddServiceSchema_Twice_AddsSchemaOnlyOnce()
    {
        TypeSchema schema1 = Scanner.AddServiceSchema(typeof(IWiki));
        TypeSchema schema2 = Scanner.AddServiceSchema(typeof(IWiki));

        Assert.NotNull(schema1);
        Assert.NotNull(schema2);
        Assert.Single(Registry.Types);
        Assert.Same(schema1, schema2);
        Assert.Same(schema1, Registry.Types.First());
    }
}