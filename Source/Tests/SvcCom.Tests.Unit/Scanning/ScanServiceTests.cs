using SvcCom.Samples.SampleWiki;
using SvcCom.Scanning;
using SvcCom.Schemas;
using Xunit;

namespace SvcCom.Tests.Unit.Scanning;

[Trait("Category", "Unit")]
public class ScanServiceTests
{
    [Fact]
    public void AddServiceSchema_AddsInterface_ToTypeRegistry()
    {
        SchemaRegistry registry = new();

        TypeSchema schema = Scanner.AddServiceSchema(typeof(IWiki), registry);

        Assert.NotNull(schema);
        Assert.Single(registry.Types);
        Assert.Same(schema, registry.Types.First());
    }
    
    [Fact]
    public void AddServiceSchema_Twice_AddsSchemaOnlyOnce()
    {
        SchemaRegistry registry = new();

        TypeSchema schema1 = Scanner.AddServiceSchema(typeof(IWiki), registry);
        TypeSchema schema2 = Scanner.AddServiceSchema(typeof(IWiki), registry);

        Assert.NotNull(schema1);
        Assert.NotNull(schema2);
        Assert.Single(registry.Types);
        Assert.Same(schema1, schema2);
        Assert.Same(schema1, registry.Types.First());
    }
}