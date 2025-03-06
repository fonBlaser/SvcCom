using SvcCom.Schemas;
using SvcCom.Schemas.Types;
using Xunit;

namespace SvcCom.Tests.Unit.Schemas;

[Trait("Category", "Unit")]
public class TypeSchemaRegistryTests : TypeSchemaRegistryTestBase
{
    [Fact]
    public void TypeSchemaRegistry_DoesNotContainTypes_ByDefault()
    {
        Assert.Empty(Registry);
    }
    
    [Fact]
    public void TypeSchemaRegistryGetOrCreateSchema_ForNewType_CreatesNewTypeSchema()
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(typeof(string));
        
        Assert.NotNull(typeSchema);
        Assert.Equal(typeof(string), typeSchema.Type);
    }
    
    [Fact]
    public void TypeSchemaRegistryGetOrCreateSchema_ForExistingType_ReturnsExistingTypeSchema()
    {
        TypeSchema typeSchema1 = Registry.GetOrCreateSchema(typeof(string));
        TypeSchema typeSchema2 = Registry.GetOrCreateSchema(typeof(string));
        
        Assert.Same(typeSchema1, typeSchema2);
    }

    [Fact]
    public void TypeSchemaRegistryGetOrCreateEntry_ForNewType_CreatesNewTypeSchemaRegistryEntry()
    {
        TypeSchemaRegistryEntry entry = Registry.GetOrCreateEntry(typeof(string));

        Assert.NotNull(entry);
        Assert.Equal(typeof(string), entry.Schema.Type);
    }

    [Fact]
    public void TypeSchemaRegistryGetOrCreateEntry_ForExistingType_ReturnsExistingTypeSchemaRegistryEntry()
    {
        TypeSchemaRegistryEntry entry1 = Registry.GetOrCreateEntry(typeof(string));
        TypeSchemaRegistryEntry entry2 = Registry.GetOrCreateEntry(typeof(string));

        Assert.Same(entry1, entry2);
    }
}