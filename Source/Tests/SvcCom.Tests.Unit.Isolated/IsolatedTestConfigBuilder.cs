using SvcCom.Configs;

namespace SvcCom.Tests.Unit.Isolated;

public class IsolatedTestConfigBuilder : TestConfigBuilder
{
#if DEBUG
    private const string Configuration = "Debug";
#else
    private const string Configuration = "Release";
#endif
    
    private IsolatedScanConfig _config = new();
    
    private string MainSampleProjectName => "SvcCom.Samples.SampleWiki";
    private string MainAssemblyPath 
        => Path.Combine(
            Directory.GetCurrentDirectory(), 
            $"../../../../../Samples/{MainSampleProjectName}/bin/{Configuration}/net8.0/{MainSampleProjectName}.dll"
            );

    private string DtoSampleProjectName => "SvcCom.Samples.SampleWiki.Dtos";
    private string DtoAssemblyPath 
        => Path.Combine(
            Directory.GetCurrentDirectory(), 
            $"../../../../../Samples/{DtoSampleProjectName}/bin/{Configuration}/net8.0/{DtoSampleProjectName}.dll"
            );

    public override ScanConfig Build()
        => _config;

    public override TestConfigBuilder AddNonExistentAssembly()
        => AddAssembly("NonExistentAssembly.dll");
    
    public override TestConfigBuilder AddCorruptedAssembly(string location)
    {
        string assemblyFullPath = Path.Combine(location, "CorruptedAssembly.dll");
        File.WriteAllText(assemblyFullPath, "Corrupted Assembly");
        
        return AddAssembly(assemblyFullPath);
    }

    public override TestConfigBuilder AddMainAssembly()
        => AddAssembly(MainAssemblyPath);
    
    public override TestConfigBuilder AddDtoAssembly()
        => AddAssembly(DtoAssemblyPath);

    public override TestConfigBuilder AddNonExistentRootService()
        => AddRootService("NonExistentRootService", "SvcCom.Samples.SampleWiki.INonExistentService");

    public override TestConfigBuilder AddMainRootService()
        => AddRootService("MainRootService", "SvcCom.Samples.SampleWiki.IWiki");
    
    public override TestConfigBuilder AddDtoRootService()
        => AddRootService("DtoRootService", "SvcCom.Samples.SampleWiki.Dtos.IDataConverterService");
    
    private TestConfigBuilder AddAssembly(string assemblyLocation)
    {
        _config = _config with
        {
            AssemblyPaths = _config.AssemblyPaths
                .Union(new[] { assemblyLocation })
                .ToArray()
        };

        return this;
    }
    
    private TestConfigBuilder AddRootService(string key, string fullTypeName)
    {
        _config = _config with
        {
            RootServiceFullTypeNames = _config.RootServiceFullTypeNames
                .Union(
                    new[]
                    {
                        new KeyValuePair<string, string>(key, fullTypeName)
                    })
                .ToDictionary()
        };

        return this;
    }
}