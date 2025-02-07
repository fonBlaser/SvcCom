namespace SvcCom.Config;

public record ScannerConfig
{
    public string[] ServiceTypeFullNames { get; init; } = [];
}