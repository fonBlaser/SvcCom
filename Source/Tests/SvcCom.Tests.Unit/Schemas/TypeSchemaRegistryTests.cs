using SvcCom.Schemas;
using Xunit;

namespace SvcCom.Tests.Unit.Schemas;

[Trait("Category", "Unit")]
public class TypeSchemaRegistryTests : TestBase
{
    [Fact]
    public void TypeSchemaRegistry_DoesNotContainTypes_ByDefault()
    {
        TypeSchemaRegistry registry = new();
        
        Assert.Empty(registry);
    }
    
    [Fact]
    public void GetOrCreateSchema_ForNewType_CreatesNewTypeSchema()
    {
        TypeSchemaRegistry registry = new();
        
        TypeSchema typeSchema = registry.GetOrCreateSchema(typeof(string));
        
        Assert.NotNull(typeSchema);
        Assert.Equal(typeof(string), typeSchema.Type);
    }
    
    [Fact]
    public void GetOrCreateSchema_ForExistingType_ReturnsExistingTypeSchema()
    {
        TypeSchemaRegistry registry = new();
        
        TypeSchema typeSchema1 = registry.GetOrCreateSchema(typeof(string));
        TypeSchema typeSchema2 = registry.GetOrCreateSchema(typeof(string));
        
        Assert.Same(typeSchema1, typeSchema2);
    }

    [Fact]
    public void GetOrCreateEntry_ForNewType_CreatesNewTypeSchemaRegistryEntry()
    {
        TypeSchemaRegistry registry = new();

        TypeSchemaRegistryEntry entry = registry.GetOrCreateEntry(typeof(string));

        Assert.NotNull(entry);
        Assert.Equal(typeof(string), entry.Schema.Type);
    }

    [Fact]
    public void GetOrCreateEntry_ForExistingType_ReturnsExistingTypeSchemaRegistryEntry()
    {
        TypeSchemaRegistry registry = new();

        TypeSchemaRegistryEntry entry1 = registry.GetOrCreateEntry(typeof(string));
        TypeSchemaRegistryEntry entry2 = registry.GetOrCreateEntry(typeof(string));

        Assert.Same(entry1, entry2);
    }
}