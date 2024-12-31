namespace SvcCom.Scanning;

public record ScanTarget(
    ScanTargetService[] RootServices,
    ScanTargetService[]? Services = null
    );