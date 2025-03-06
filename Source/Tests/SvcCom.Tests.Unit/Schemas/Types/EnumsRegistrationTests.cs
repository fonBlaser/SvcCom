using SvcCom.Schemas;
using SvcCom.Schemas.Types;
using SvcCom.Tests.Unit._TestData.SimpleCases;
using Xunit;

namespace SvcCom.Tests.Unit.Schemas.Types;

[Trait("Category", "Unit")]
public class EnumsRegistrationTests : TypeSchemaRegistryTestBase
{
    [Fact]
    public void TypeSchemaRegistryGetOrCreateSchema_ForEmptyEnumType_ReturnsEnumTypeSchemaWithoutValues()
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(typeof(EmptyEnum));

        Assert.NotNull(typeSchema);
        Assert.Equal(typeof(EmptyEnum), typeSchema.Type);

        EnumTypeSchema? enumTypeSchema = typeSchema as EnumTypeSchema;
        Assert.NotNull(enumTypeSchema);
        Assert.Empty(enumTypeSchema.Values);
    }

    [Fact]
    public void TypeSchemaRegistryGetOrCreateSchema_ForEnumType_ReturnsEnumTypeSchemaWithoutValues()
    {
        TypeSchema typeSchema = Registry.GetOrCreateSchema(typeof(EnumWithDifferentValues));

        Assert.NotNull(typeSchema);
        Assert.Equal(typeof(EnumWithDifferentValues), typeSchema.Type);

        EnumTypeSchema? enumTypeSchema = typeSchema as EnumTypeSchema;
        Assert.NotNull(enumTypeSchema);
        Assert.Empty(enumTypeSchema.Values);
    }

    [Theory]
    [InlineData(typeof(EmptyEnum))]
    [InlineData(typeof(EnumWithDifferentValues))]
    public void TypeSchemaRegistryGetOrCreateEntry_ForBothEmptyAndFilledEnums_ReturnsEntryWithoutIsScannedFlag(Type enumType)
    {
        TypeSchemaRegistryEntry entry = Registry.GetOrCreateEntry(enumType);

        Assert.NotNull(entry);
        Assert.Equal(enumType, entry.Schema.Type);
        Assert.False(entry.IsScanned);
    }
}