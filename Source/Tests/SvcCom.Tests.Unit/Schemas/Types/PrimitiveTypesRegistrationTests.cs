using SvcCom.Schemas;
using SvcCom.Schemas.Types;
using Xunit;

namespace SvcCom.Tests.Unit.Schemas.Types;

[Trait("Category", "Unit")]
public class PrimitiveTypesRegistrationTests : TypeSchemaRegistryTestBase
{
    [Theory]
    [InlineData(typeof(sbyte))]
    [InlineData(typeof(short))]
    [InlineData(typeof(int))]
    [InlineData(typeof(long))]
    [InlineData(typeof(Int128))]
    [InlineData(typeof(byte))]
    [InlineData(typeof(ushort))]
    [InlineData(typeof(uint))]
    [InlineData(typeof(ulong))]
    [InlineData(typeof(UInt128))]
    [InlineData(typeof(float))]
    [InlineData(typeof(double))]
    [InlineData(typeof(decimal))]
    public void TypeSchemaRegistryGetOrCreateSchema_ForNumericTypes_ReturnsPrimitiveTypeSchemaWithIsNumericFlag(
        Type numericBuiltInType)
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(numericBuiltInType);

        Assert.NotNull(typeSchema);
        Assert.Equal(numericBuiltInType, typeSchema.Type);

        PrimitiveTypeSchema? primitiveTypeSchema = typeSchema as PrimitiveTypeSchema;
        Assert.NotNull(primitiveTypeSchema);
        Assert.True(primitiveTypeSchema.IsNumeric);
    }

    [Fact]
    public void TypeSchemaRegistryGetOrCreateSchema_ForBoolType_ReturnsPrimitiveTypeSchemaWithIsBoolFlag()
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(typeof(bool));

        Assert.NotNull(typeSchema);
        Assert.Equal(typeof(bool), typeSchema.Type);

        PrimitiveTypeSchema? primitiveTypeSchema = typeSchema as PrimitiveTypeSchema;
        Assert.NotNull(primitiveTypeSchema);
        Assert.True(primitiveTypeSchema.IsBool);
    }

    [Theory]
    [InlineData(typeof(char))]
    [InlineData(typeof(string))]
    public void TypeSchemaRegistryGetOrCreateSchema_ForStringTypes_ReturnsPrimitiveTypeSchemaWithIsStringFlag(
        Type stringBuiltInType)
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(stringBuiltInType);

        Assert.NotNull(typeSchema);
        Assert.Equal(stringBuiltInType, typeSchema.Type);

        PrimitiveTypeSchema? primitiveTypeSchema = typeSchema as PrimitiveTypeSchema;
        Assert.NotNull(primitiveTypeSchema);
        Assert.True(primitiveTypeSchema.IsString);
    }

    [Fact]
    public void TypeSchemaRegistryGetOrCreateSchema_ForGuidType_ReturnsPrimitiveTypeSchemaWithIsGuidFlag()
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(typeof(Guid));

        Assert.NotNull(typeSchema);
        Assert.Equal(typeof(Guid), typeSchema.Type);

        PrimitiveTypeSchema? primitiveTypeSchema = typeSchema as PrimitiveTypeSchema;
        Assert.NotNull(primitiveTypeSchema);
        Assert.True(primitiveTypeSchema.IsGuid);
    }

    [Theory]
    [InlineData(typeof(sbyte))]
    [InlineData(typeof(short))]
    [InlineData(typeof(int))]
    [InlineData(typeof(long))]
    [InlineData(typeof(Int128))]
    [InlineData(typeof(byte))]
    [InlineData(typeof(ushort))]
    [InlineData(typeof(uint))]
    [InlineData(typeof(ulong))]
    [InlineData(typeof(UInt128))]
    [InlineData(typeof(float))]
    [InlineData(typeof(double))]
    [InlineData(typeof(decimal))]
    [InlineData(typeof(bool))]
    [InlineData(typeof(char))]
    [InlineData(typeof(string))]
    [InlineData(typeof(Guid))]
    public void TypeSchemaRegistryGetOrCreateEntry_ForPrimitiveTypes_ReturnsEntryWithPrimitiveTypeSchemaAndIsScannedFlag(
        Type primitiveType)
    {
        TypeSchemaRegistryEntry entry = Registry.GetOrCreateEntry(primitiveType);

        Assert.NotNull(entry);
        Assert.True(entry.IsScanned);
        Assert.Contains(entry, Registry);
    }
    
    [Fact]
    public void TypeSchemaRegistryGetOrCreateSchema_ForVoidType_ReturnsSchemaWithIsVoidAndWithoutIsTaskFlags()
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(typeof(void));

        Assert.NotNull(typeSchema);
        Assert.Equal(typeof(void), typeSchema.Type);

        PrimitiveTypeSchema? objectTypeSchema = typeSchema as PrimitiveTypeSchema;
        
        Assert.NotNull(objectTypeSchema);
        Assert.True(objectTypeSchema.IsVoid);
        Assert.False(objectTypeSchema.IsTask);
    }

    [Theory]
    [InlineData(typeof(Task))]
    [InlineData(typeof(ValueTask))]
    public void TypeSchemaRegistryGetOrCreateSchema_ForTaskTypes_ReturnsSchemaWithIsVoidAndIsTaskFlags(Type type)
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(type);

        Assert.NotNull(typeSchema);
        Assert.Equal(type, typeSchema.Type);

        PrimitiveTypeSchema? objectTypeSchema = typeSchema as PrimitiveTypeSchema;
        
        Assert.NotNull(objectTypeSchema);
        Assert.True(objectTypeSchema.IsVoid);
        Assert.True(objectTypeSchema.IsTask);
    }
}