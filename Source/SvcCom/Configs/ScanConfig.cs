using System.Reflection;

namespace SvcCom.Configs;

public abstract record ScanConfig
{
    public virtual Assembly[] Assemblies { get; init; } = [];
    public virtual IReadOnlyDictionary<string, Type> RootServiceTypes { get; init; } 
        = new Dictionary<string, Type>();

    public virtual Type[] ServiceTypes { get; init; } = [];
}