namespace SvcCom.Tests.Unit._TestData.CasesWithProperties;

public interface IServiceWithProperties
{
    public Version Ver { get; }
    public string PropertyWithInternalGetter { internal get; set; }
    internal string InternalProperty { get; set; }
}