using System.Reflection;

namespace SvcCom.Configs;

public sealed record IsolatedScanConfig : ScanConfig
{
    public string[] AssemblyPaths { get; init; } = [];
    public IReadOnlyDictionary<string, string> RootServiceFullTypeNames { get; init; } 
        = Enumerable.Empty<KeyValuePair<string, string>>().ToDictionary();

    public string[] ServiceFullTypeNames { get; init; } = [];
    
    public override Assembly[] Assemblies 
        => AssemblyPaths.Select(Assembly.LoadFrom).ToArray();
    
    public override IReadOnlyDictionary<string, Type> RootServiceTypes
        => RootServiceFullTypeNames.ToDictionary(
            kvp => kvp.Key,
            kvp => Assemblies.SelectMany(asm => asm.GetTypes())
                .First(t => t.FullName == kvp.Value));
    
    public override Type[] ServiceTypes
        => ServiceFullTypeNames.Select(name => Assemblies.SelectMany(asm => asm.GetTypes())
            .First(t => t.FullName == name)).ToArray();
}