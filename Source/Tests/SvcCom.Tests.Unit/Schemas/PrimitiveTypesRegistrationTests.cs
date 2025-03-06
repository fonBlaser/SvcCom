﻿using SvcCom.Schemas;
using Xunit;

namespace SvcCom.Tests.Unit.Schemas;

[Trait("Category", "Unit")]
public class PrimitiveTypesRegistrationTests : TestBase
{
    private TypeSchemaRegistry Registry { get; } = new();

    [Theory]
    #region Inline data
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
    #endregion
    public void TypeRegistryGetOrCreateSchema_ForNumericTypes_ReturnsPrimitiveTypeSchemaWithIsNumericFlag(Type numericBuiltInType)
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(numericBuiltInType);

        Assert.NotNull(typeSchema);
        Assert.Equal(numericBuiltInType, typeSchema.Type);
        
        PrimitiveTypeSchema? primitiveTypeSchema = typeSchema as PrimitiveTypeSchema;
        Assert.NotNull(primitiveTypeSchema);
        Assert.True(primitiveTypeSchema.IsNumeric);
    }

    [Fact]
    public void TypeRegistryGetOrCreateSchema_ForBoolType_ReturnsPrimitiveTypeSchemaWithIsBoolFlag()
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(typeof(bool));

        Assert.NotNull(typeSchema);
        Assert.Equal(typeof(bool), typeSchema.Type);

        PrimitiveTypeSchema? primitiveTypeSchema = typeSchema as PrimitiveTypeSchema;
        Assert.NotNull(primitiveTypeSchema);
        Assert.True(primitiveTypeSchema.IsBool);
    }

    [Theory]
    #region Inline data
    [InlineData(typeof(char))]
    [InlineData(typeof(string))]
    #endregion
    public void TypeRegistryGetOrCreateSchema_ForStringTypes_ReturnsPrimitiveTypeSchemaWithIsStringFlag(Type stringBuiltInType)
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(stringBuiltInType);

        Assert.NotNull(typeSchema);
        Assert.Equal(stringBuiltInType, typeSchema.Type);

        PrimitiveTypeSchema? primitiveTypeSchema = typeSchema as PrimitiveTypeSchema;
        Assert.NotNull(primitiveTypeSchema);
        Assert.True(primitiveTypeSchema.IsString);
    }

    [Fact]
    public void TypeRegistryGetOrCreateSchema_ForGuidType_ReturnsPrimitiveTypeSchemaWithIsGuidFlag()
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(typeof(Guid));

        Assert.NotNull(typeSchema);
        Assert.Equal(typeof(Guid), typeSchema.Type);

        PrimitiveTypeSchema? primitiveTypeSchema = typeSchema as PrimitiveTypeSchema;
        Assert.NotNull(primitiveTypeSchema);
        Assert.True(primitiveTypeSchema.IsGuid);
    }

    [Theory]
    #region Inline data
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
    #endregion
    public void TypeRegistryGetOrCreateEntry_ForPrimitiveTypes_ReturnsEntryWithPrimitiveTypeSchemaAndIsScannedFlag(Type primitiveType)
    {
        TypeSchemaRegistryEntry entry = Registry.GetOrCreateEntry(primitiveType);

        Assert.NotNull(entry);
        Assert.True(entry.IsScanned);
        Assert.Contains(entry, Registry);
    }
}