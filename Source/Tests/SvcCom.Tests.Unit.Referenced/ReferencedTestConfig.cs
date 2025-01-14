using SvcCom.Samples.SampleWiki;

namespace SvcCom.Tests.Unit.Referenced;

public record ReferencedTestConfig : TestConfig
{
    public ReferencedTestConfig()
    {
        TargetAssemblyName = typeof(IWiki).Assembly.GetName().Name ?? string.Empty;
        TargetAssemblyPath = typeof(IWiki).Assembly.Location;
    }
}