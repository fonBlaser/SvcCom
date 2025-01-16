using SvcCom.Configs;

namespace SvcCom.Scanning;

public class Scanner
{
    private readonly ScanConfig _config;

    private Scanner(ScanConfig config)
    {
        _config = config;
    }

    public static async Task<Scanner> Create(ScanConfig config)
    {
        return await Task.Run(() =>
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config), "Config cannot be null.");

            if (config.Assemblies == null || config.Assemblies.Length == 0)
                throw new ArgumentException("Assemblies cannot be null or empty.", nameof(config.Assemblies));
        
            if(config.RootServiceTypes == null || config.RootServiceTypes.Count == 0)
                throw new ArgumentException("Root service types cannot be null or empty.", nameof(config.RootServiceTypes));

            return new Scanner(config);
        });
    }
}