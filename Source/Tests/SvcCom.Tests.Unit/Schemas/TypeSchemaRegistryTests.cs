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
    public void GetOrCreate_ForNewType_CreatesNewTypeSchema()
    {
        TypeSchemaRegistry registry = new();
        
        TypeSchema typeSchema = registry.GetOrCreate(typeof(string));
        
        Assert.NotNull(typeSchema);
        Assert.Equal(typeof(string), typeSchema.Type);
    }
    
    [Fact]
    public void GetOrCreate_ForExistingType_ReturnsExistingTypeSchema()
    {
        TypeSchemaRegistry registry = new();
        
        TypeSchema typeSchema1 = registry.GetOrCreate(typeof(string));
        TypeSchema typeSchema2 = registry.GetOrCreate(typeof(string));
        
        Assert.Same(typeSchema1, typeSchema2);
    }
}