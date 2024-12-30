using System.Reflection;
using SvcCom.Exceptions;

namespace SvcCom.Scanning;

public sealed class AssemblyScanner
{
    public string TargetAssemblyPath { get; }
    public AssemblyScannerConfig Config { get; }

    public AssemblyScanner(string targetAssemblyPath, AssemblyScannerConfig? config = null)
    {
        if(string.IsNullOrWhiteSpace(targetAssemblyPath))
            throw new ArgumentException("TargetAssemblyPath cannot be null or whitespace.", nameof(targetAssemblyPath));

        if (!File.Exists(targetAssemblyPath))
            throw new AssemblyLoadException(targetAssemblyPath, new FileNotFoundException());

        TargetAssemblyPath = targetAssemblyPath;
        Config = config ?? AssemblyScannerConfig.Default;
    }

    public async Task Scan()
    {
        Assembly assembly = await LoadAssembly(TargetAssemblyPath);
    }

    private static async Task<Assembly> LoadAssembly(string assemblyPath)
    {
        try
        {
            return await Task.Run(() => Assembly.LoadFrom(assemblyPath));
        }
        catch (Exception ex)
        {
            throw new AssemblyLoadException(assemblyPath, ex);
        }
    }
}