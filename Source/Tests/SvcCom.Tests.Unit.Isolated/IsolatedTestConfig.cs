using Xunit;

namespace SvcCom.Tests.Unit.Isolated;

internal static class IsolatedTestConfig
{
    private static string Configuration
#if DEBUG
        => "Debug";
#else
        => "Release";
#endif

    internal static string TargetAssemblyName 
        => "SvcCom.Samples.SampleWiki";

    internal static string TargetAssemblyPath
        => Path.Combine(Directory.GetCurrentDirectory(), $"../../../../../Samples/{TargetAssemblyName}/bin/{Configuration}/net8.0/{TargetAssemblyName}.dll");
}