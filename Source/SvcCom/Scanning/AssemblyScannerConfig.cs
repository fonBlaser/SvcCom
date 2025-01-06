namespace SvcCom.Scanning;

public record AssemblyScannerConfig(
    bool IncludePublicTypes = true,
    bool IncludeInternalTypes = false,
    bool IncludePrivateTypes = false
    )
{
    public static readonly AssemblyScannerConfig Default = new();
}