using SvcCom.Configs;

namespace SvcCom.Scanning;

public class Scanner
{
    private readonly ScanConfig _config;

    internal Scanner(ScanConfig config)
    {
        _config = config;
    }
}