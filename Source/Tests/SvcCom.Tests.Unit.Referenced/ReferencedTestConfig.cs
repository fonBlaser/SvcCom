using SvcCom.Samples.SampleWiki;

namespace SvcCom.Tests.Unit.Referenced;

public class ReferencedTestConfig
{
    internal static string TargetAssemblyName
        => typeof(IWiki).Assembly.GetName().Name ?? string.Empty;

    internal static string TargetAssemblyPath
        => typeof(IWiki).Assembly.Location;
}