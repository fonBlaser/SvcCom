using System.Reflection;
using SvcCom.Schemas;
using SvcCom.Tests.Unit._TestData.SimpleCases;
using SvcCom.Utility;
using Xunit;

namespace SvcCom.Tests.Unit.Schemas;

[Trait("Category", "Unit")]
public class PropertySchemaTests
{
    private TypeSchemaRegistry registry = new();

    [Fact]
    public void PropertySchema_ThrowsException_WhenPropertyInfoIsNull()
    {
        PropertyInfo pi = null!;

        Assert.Throws<ArgumentNullException>(() => PropertySchema.Create(pi, registry));
    }

    [Fact]
    public void PropertySchema_WithValueType_AddsValueTypeToRegistry()
    {
        PropertyInfo pi = ReflectionUtility.GetPropertyInfo(typeof(IInterfaceWithProperties),
            nameof(IInterfaceWithProperties.PropertyWithPublicGetter));
        
        PropertySchema propertySchema = PropertySchema.Create(pi, registry);
        
        Assert.NotNull(propertySchema);
        Assert.Equal(nameof(IInterfaceWithProperties.PropertyWithPublicGetter), propertySchema.Name);
        Assert.False(propertySchema.IsNullable);
        Assert.False(propertySchema.IsTask);
        Assert.Contains(registry, schema => schema.Type == pi.PropertyType);
    }
}