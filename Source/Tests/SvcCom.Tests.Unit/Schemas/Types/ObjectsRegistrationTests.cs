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
    public void TypeSchemaRegistryGetOrCreateSchema_ForPublicInterface_ReturnsSchemaWithIsInterfaceAndIsReferenceFlag()
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(typeof(IEmptyPublicInterface));

        Assert.NotNull(typeSchema);
        Assert.Equal(typeof(IEmptyPublicInterface), typeSchema.Type);

        ObjectTypeSchema? objectTypeSchema = typeSchema as ObjectTypeSchema;
        
        Assert.NotNull(objectTypeSchema);
        Assert.True(objectTypeSchema.IsInterface);
        Assert.True(objectTypeSchema.IsReferenceType);
        Assert.False(objectTypeSchema.IsValueType);
    }
    
    [Theory]
    [InlineData(typeof(EmptyPublicClass))]
    [InlineData(typeof(EmptyPublicRecord))]
    public void TypeSchemaRegistryGetOrCreateSchema_ForClassAndRecord_ReturnsSchemaWithIsReferenceTypeFlag(Type type)
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(type);

        Assert.NotNull(typeSchema);
        Assert.Equal(type, typeSchema.Type);

        ObjectTypeSchema? objectTypeSchema = typeSchema as ObjectTypeSchema;
        
        Assert.NotNull(objectTypeSchema);
        Assert.True(objectTypeSchema.IsReferenceType);
    }
    
    [Theory]
    [InlineData(typeof(EmptyPublicStruct))]
    [InlineData(typeof(EmptyPublicRecordStruct))]
    public void TypeSchemaRegistryGetOrCreateSchema_ForStructAndRecordStruct_ReturnsSchemaWithIsValueTypeFlag(Type type)
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(type);

        Assert.NotNull(typeSchema);
        Assert.Equal(type, typeSchema.Type);

        ObjectTypeSchema? objectTypeSchema = typeSchema as ObjectTypeSchema;
        
        Assert.NotNull(objectTypeSchema);
        Assert.False(objectTypeSchema.IsInterface);
        Assert.False(objectTypeSchema.IsReferenceType);
    }
}