using System.Reflection;
using SvcCom.Utility;
using SvcCom.Utility.Extensions;
using Xunit;

namespace SvcCom.Tests.Unit.Utility.Extensions;

[Trait("Category", "Unit")]
public class TypeExtensionsTests
{
    public Task? NullableTaskTestProperty { get; }
    public Task<bool>? NullableTaskUnderlyingBoolTestProperty { get; }
    public Task<bool?>? NullableTaskUnderlyingNullableBoolTestProperty { get; }

    [Theory]
    [InlineData(typeof(bool), typeof(bool), false, false)]
    [InlineData(typeof(bool?), typeof(bool), true, false)]
    [InlineData(typeof(Task), typeof(void), false, true)]
    [InlineData(typeof(Task<bool>), typeof(bool), false, true)]
    [InlineData(typeof(Task<bool?>), typeof(bool), true, true)]
    public void TypeExtensionsGetValueDetails_ForPrimitiveTypes_ReturnsCorrectFlags(Type type, Type valueType, bool isNullable, bool isTask)
    {
        ValueDetails valueDetails = type.GetValueDetails();

        Assert.Equal(valueType, valueDetails.ValueType);
        Assert.Equal(isNullable, valueDetails.IsNullable);
        Assert.Equal(isTask, valueDetails.IsTask);
    }

    [Fact]
    public void TypeExtensionsGetValueDetails_ForNullableTaskWithoutUnderlyingType_ReturnsCorrectFlags()
    {
        PropertyInfo pi = GetType().GetProperty(nameof(NullableTaskTestProperty))!;
        Type type = pi.PropertyType;

        ValueDetails valueDetails = type.GetValueDetails();

        Assert.Equal(typeof(void), valueDetails.ValueType);
        Assert.False(valueDetails.IsNullable);
        Assert.True(valueDetails.IsTask);
    }

    [Fact]
    public void TypeExtensionsGetValueDetails_ForNullableTaskWithUnderlyingBoolType_ReturnsCorrectFlags()
    {
        Type type = GetType().GetProperty(nameof(NullableTaskUnderlyingBoolTestProperty))!.PropertyType;

        ValueDetails valueDetails = type.GetValueDetails();

        Assert.Equal(typeof(bool), valueDetails.ValueType);
        Assert.False(valueDetails.IsNullable);
        Assert.True(valueDetails.IsTask);
    }

    [Fact]
    public void TypeExtensionsGetValueDetails_ForNullableTaskWithUnderlyingNullableBoolType_ReturnsCorrectFlags()
    {
        Type type = GetType().GetProperty(nameof(NullableTaskUnderlyingNullableBoolTestProperty))!.PropertyType;

        ValueDetails valueDetails = type.GetValueDetails();

        Assert.Equal(typeof(bool), valueDetails.ValueType);
        Assert.True(valueDetails.IsNullable);
        Assert.True(valueDetails.IsTask);
    }
}