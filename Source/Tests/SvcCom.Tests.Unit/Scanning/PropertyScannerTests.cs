using System.Reflection;
using SvcCom.Scanning;
using SvcCom.Schemas.ObjectComponents;
using SvcCom.Tests.Unit._TestData.SimpleCases;
using Xunit;

namespace SvcCom.Tests.Unit.Scanning;

[Trait("Category", "Unit")]
public class PropertyScannerTests : TypeSchemaScannerTestBase
{
    private new PropertyScanner Scanner { get; }

    public PropertyScannerTests()
    {
        Scanner = base.Scanner.PropertyScanner;
    }

    [Theory]
    [InlineData(nameof(IInterfaceWithProperties.PropertyWithInternalGetter))]
    [InlineData(nameof(IInterfaceWithProperties.PropertyWithInternalSetter))]
    [InlineData(nameof(IInterfaceWithProperties.PropertyWithPublicGetterAndSetter))]
    [InlineData(nameof(IInterfaceWithProperties.TaskPropertyWithPublicGetter))]
    [InlineData(nameof(IInterfaceWithProperties.TaskValuePropertyWithPublicGetter))]
    [InlineData(nameof(IInterfaceWithProperties.TaskNullablePropertyWithPublicGetter))]
    [InlineData(nameof(IInterfaceWithProperties.VersionPropertyWithPublicGetter))]
    public void PropertyScannerGetAvailableProperties_ForPublicInterface_ReturnsAllPublicProperties(string propName)
    {
        List<PropertyInfo> properties = Scanner.GetAvailableProperties(typeof(IInterfaceWithProperties));

        Assert.NotNull(properties);
        Assert.Contains(properties, p => p.Name == propName);
    }

    [Theory]
    [InlineData(nameof(IInterfaceWithProperties.InternalBoolProperty))]
    [InlineData(nameof(IInterfaceWithProperties.InternalTaskProperty))]
    [InlineData(nameof(IInterfaceWithProperties.InternalGenericTaskProperty))]
    public void PropertyScannerGetAvailableProperties_ForPublicInterface_DoesNotReturnInternalProperties(string propName)
    {
        List<PropertyInfo> properties = Scanner.GetAvailableProperties(typeof(IInterfaceWithProperties));

        Assert.NotNull(properties);

        List<string> propertyNames = properties.Select(p => p.Name).ToList();

        Assert.DoesNotContain(propertyNames, name => name == propName);
    }
    
    [Fact]
    public void PropertyScannerCreateSchema_ForPropertyWithGetSet_ReturnsCanGetAndCanSet()
    {
        PropertyInfo property 
            = typeof(IInterfaceWithProperties)
                .GetProperty(nameof(IInterfaceWithProperties.PropertyWithPublicGetterAndSetter))!;

        PropertySchema schema = Scanner.CreateSchema(property);

        Assert.True(schema.CanGet);
        Assert.True(schema.CanSet);
    }
    
    [Fact]
    public void PropertyScannerCreateSchema_ForPropertyWithGetOnly_ReturnsCanGetAndNotCanSet()
    {
        PropertyInfo property 
            = typeof(IInterfaceWithProperties)
                .GetProperty(nameof(IInterfaceWithProperties.PropertyWithInternalSetter))!;

        PropertySchema schema = Scanner.CreateSchema(property);

        Assert.True(schema.CanGet);
        Assert.False(schema.CanSet);
    }
    
    [Fact]
    public void PropertyScannerCreateSchema_ForPropertyWithSetOnly_ReturnsNotCanGetAndCanSet()
    {
        PropertyInfo property 
            = typeof(IInterfaceWithProperties)
                .GetProperty(nameof(IInterfaceWithProperties.PropertyWithInternalGetter))!;

        PropertySchema schema = Scanner.CreateSchema(property);

        Assert.False(schema.CanGet);
        Assert.True(schema.CanSet);
    }
}