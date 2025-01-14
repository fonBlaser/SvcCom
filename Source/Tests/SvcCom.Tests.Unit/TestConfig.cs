namespace SvcCom.Tests.Unit;

public record TestConfig
{
    public string TargetAssemblyName { get; protected set; } = string.Empty;
    public string TargetAssemblyPath { get; protected set; } = string.Empty;
}