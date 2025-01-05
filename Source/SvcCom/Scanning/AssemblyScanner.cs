using System.Reflection;
using SvcCom.Exceptions;
using SvcCom.Registries;
using SvcCom.Schemas;

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

    public async Task<AssemblySchema> Scan(ScanTarget target, CancellationToken ctx = default)
    {
        Assembly assembly = await LoadAssembly(TargetAssemblyPath);
        AssemblySchema assemblySchema = new(assembly.FullName ?? string.Empty);
        
        await ScanRootServices(assembly, assemblySchema, target, ctx);

        return assemblySchema;
    }

    private async Task ScanRootServices(Assembly assembly, AssemblySchema assemblySchema, ScanTarget target, CancellationToken ctx = default)
    {
        foreach (ScanTargetService rootService in target.RootServices)
        {
            Type? rootServiceType = assembly.GetType(rootService.FullName);
            if (rootServiceType is null)
                throw new TypeLoadException(rootService.FullName);

            string rootServiceFullName = rootServiceType.FullName ?? string.Empty;

            TypeRegistryEntry? typeRegistryEntry = assemblySchema.TypeRegistry
                                                                 .Entries
                                                                 .FirstOrDefault(t
                                                                     => t.IsService
                                                                     && t.TypeFullName == rootServiceFullName);

            if (typeRegistryEntry is null)
            {
                typeRegistryEntry = new(rootServiceFullName, true);
                assemblySchema.TypeRegistry.Add(typeRegistryEntry);
            }

            if(!typeRegistryEntry.Scanned)
            {
                TypeSchema serviceSchema = await ScanTypeSchema(rootServiceType, ctx);
                typeRegistryEntry.SetSchema(serviceSchema);
            }

            assemblySchema.AddRootService(typeRegistryEntry);
        }
    }

    private async Task<TypeSchema> ScanTypeSchema(Type serviceType, CancellationToken ctx = default)
    {
        TypeSchema schema = new(serviceType);
        return schema;
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