using SvcCom.Configs;

namespace SvcCom.Scanning;

public class Scanner
{
    public Scanner(ScanConfig config)
    {
        if (config == null)
            throw new ArgumentNullException(nameof(config), "Config cannot be null.");

        if (config.Assemblies == null || config.Assemblies.Length == 0)
            throw new ArgumentException("Assemblies cannot be null or empty.", nameof(config.Assemblies));
        
        if(config.RootServiceTypes == null || config.RootServiceTypes.Count == 0)
            throw new ArgumentException("Root service types cannot be null or empty.", nameof(config.RootServiceTypes));
    }
}