using SvcCom.Configs;

namespace SvcCom.Scanning;

public static class ScannerFactory
{
    public static async Task<Scanner> Create(ScanConfig config)
    {
        return await Task.Run(() =>
        {
            if (config == null)
                throw new ArgumentNullException(nameof(config), "Config cannot be null.");

            if (config.Assemblies == null || config.Assemblies.Length == 0)
                throw new ArgumentException("Assemblies cannot be null or empty.", nameof(config.Assemblies));

            if (config.RootServiceTypes == null || config.RootServiceTypes.Count == 0)
                throw new ArgumentException("Root service types cannot be null or empty.", nameof(config.RootServiceTypes));

            return new Scanner(config);
        });
    }
}