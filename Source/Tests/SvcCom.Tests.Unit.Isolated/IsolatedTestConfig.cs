using Xunit;

namespace SvcCom.Tests.Unit.Isolated;

internal record IsolatedTestConfig : TestConfig
{
    private static string Configuration
#if DEBUG
        => "Debug";
#else
        => "Release";
#endif

    public IsolatedTestConfig()
    {
        TargetAssemblyName = "SvcCom.Samples.SampleWiki";
        TargetAssemblyPath = Path.Combine(Directory.GetCurrentDirectory(), $"../../../../../Samples/{TargetAssemblyName}/bin/{Configuration}/net8.0/{TargetAssemblyName}.dll");
    }
}