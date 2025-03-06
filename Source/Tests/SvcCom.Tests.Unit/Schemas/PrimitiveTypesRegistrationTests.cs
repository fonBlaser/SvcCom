﻿using SvcCom.Schemas;
using Xunit;

namespace SvcCom.Tests.Unit.Schemas;

[Trait("Category", "Unit")]
public class PrimitiveTypesRegistrationTests : TestBase
{
    private TypeSchemaRegistry Registry { get; } = new TypeSchemaRegistry();

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
    public void TypeRegistryGetOrCreate_ForNumericTypes_ReturnsPrimitiveNumericTypeSchema(Type numericBuiltInType)
    {
        TypeSchema typeSchema = Registry.GetOrCreate(numericBuiltInType);

        Assert.NotNull(typeSchema);
        Assert.Equal(numericBuiltInType, typeSchema.Type);
        
        PrimitiveTypeSchema? primitiveTypeSchema = typeSchema as PrimitiveTypeSchema;
        Assert.NotNull(primitiveTypeSchema);
        Assert.True(primitiveTypeSchema.IsNumeric);
    }

    [Fact]
    public void TypeRegistryGetOrCreate_ForBoolType_ReturnsPrimitiveBoolTypeSchema()
    {
        TypeSchema typeSchema = Registry.GetOrCreate(typeof(bool));

        Assert.NotNull(typeSchema);
        Assert.Equal(typeof(bool), typeSchema.Type);

        PrimitiveTypeSchema? primitiveTypeSchema = typeSchema as PrimitiveTypeSchema;
        Assert.NotNull(primitiveTypeSchema);
        Assert.True(primitiveTypeSchema.IsBool);
    }

    [Theory]
    [InlineData(typeof(char))]
    [InlineData(typeof(string))]
    public void TypeRegistryGetOrCreate_ForStringTypes_ReturnsPrimitiveStringTypeSchema(Type stringBuiltInType)
    {
        TypeSchema typeSchema = Registry.GetOrCreate(stringBuiltInType);

        Assert.NotNull(typeSchema);
        Assert.Equal(stringBuiltInType, typeSchema.Type);

        PrimitiveTypeSchema? primitiveTypeSchema = typeSchema as PrimitiveTypeSchema;
        Assert.NotNull(primitiveTypeSchema);
        Assert.True(primitiveTypeSchema.IsString);
    }

    //ToDo Later: Add methods and tests that checks IsScanned initial value for primitive type entries
}