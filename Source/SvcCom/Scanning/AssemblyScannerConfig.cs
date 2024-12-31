namespace SvcCom.Scanning;

public record AssemblyScannerConfig(
    bool InterfacePropertiesAreServices = false
    )
{
    public static readonly AssemblyScannerConfig Default = new();
}