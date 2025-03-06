namespace SvcCom.Tests.Unit._TestData.SimpleCases;

public enum EnumWithDifferentValues
{
    // This is the first value so it is zero
    ValueBeforeNegative,

    NegativeValue = -1,

    // This value is zero, and it is same to the first value
    ZeroValue = 0,
    PositiveValue = 1,
    ValueAfterPositive
}