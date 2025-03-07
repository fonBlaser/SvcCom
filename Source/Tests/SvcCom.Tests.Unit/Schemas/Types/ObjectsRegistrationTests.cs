using SvcCom.Schemas.Types;
using SvcCom.Tests.Unit._TestData.SimpleCases.EmptyObjectTypes;
using SvcCom.Tests.Unit._TestData.SimpleCases.TypesWithDifferentVisibility;
using Xunit;

namespace SvcCom.Tests.Unit.Schemas.Types;

[Trait("Category", "Unit")]
public class ObjectsRegistrationTests : TypeSchemaRegistryTestBase
{
    [Theory]
    [InlineData(typeof(void))]
    [InlineData(typeof(object))]
    [InlineData(typeof(Task))]
    [InlineData(typeof(Task<>))]
    [InlineData(typeof(ValueTask))]
    [InlineData(typeof(ValueTask<>))]
    public void TypeSchemaRegistryGetOrCreateSchema_ForSystemObjectTypes_ThrowsException(Type incorrectType)
    {
        Assert.Throws<TypeAccessException>(() => Registry.GetOrCreateSchema(incorrectType));
    }
    
    [Theory]
    [InlineData(typeof(IPrivateInterface))]
    [InlineData(typeof(PrivateClass))]
    [InlineData(typeof(PrivateStruct))]
    [InlineData(typeof(PrivateRecord))]
    [InlineData(typeof(PrivateRecordStruct))]
    [InlineData(typeof(PrivateEnum))]
    [InlineData(typeof(IInternalInterface))]
    [InlineData(typeof(InternalClass))]
    [InlineData(typeof(InternalStruct))]
    [InlineData(typeof(InternalRecord))]
    [InlineData(typeof(InternalRecordStruct))]
    [InlineData(typeof(InternalEnum))]
    public void TypeSchemaRegistryGetOrCreateSchema_ForPrivateOrInternalObject_ThrowsException(Type incorrectType)
    {
        Assert.Throws<TypeAccessException>(() => Registry.GetOrCreateSchema(incorrectType));
    }

    [Fact]
    public void TypeSchemaRegistryGetOrCreateSchema_ForPublicInterface_ReturnsSchemaWithIsInterfaceFlag()
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(typeof(IEmptyPublicInterface));

        Assert.NotNull(typeSchema);
        Assert.Equal(typeof(IEmptyPublicInterface), typeSchema.Type);

        ObjectTypeSchema? objectTypeSchema = typeSchema as ObjectTypeSchema;
        
        Assert.NotNull(objectTypeSchema);
        Assert.True(objectTypeSchema.IsInterface);
    }
}