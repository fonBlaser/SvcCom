using System.Reflection;

namespace SvcCom.Configs;

public sealed record ReferencedScanConfig : ScanConfig
{
    public override Assembly[] Assemblies { get; init; } = [];

    public override IReadOnlyDictionary<string, Type> RootServiceTypes { get; init; }
        = new Dictionary<string, Type>();

    public override Type[] ServiceTypes { get; init; } = [];
}