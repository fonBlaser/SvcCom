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
    {
        _config = _config with
        {
            AssemblyPaths = _config.AssemblyPaths
                                   .Union(new[] { "Assembly.Not.Exists.dll" })
                                   .ToArray()
        };

        return this;
    }
    
    public override TestConfigBuilder AddCorruptedAssembly(string location)
    {
        string assemblyFullPath = Path.Combine(location, "CorruptedAssembly.dll");
        File.WriteAllText(assemblyFullPath, "Corrupted Assembly");
        
        _config = _config with
        {
            AssemblyPaths = _config.AssemblyPaths
                                   .Union(new[] { assemblyFullPath })
                                   .ToArray()
        };

        return this;
    }
    
    public override TestConfigBuilder AddMainAssembly()
    {
        _config = _config with
        {
            AssemblyPaths = _config.AssemblyPaths
                                   .Union(new[] { MainAssemblyPath })
                                   .ToArray()
        };

        return this;
    }
    
    public override TestConfigBuilder AddDtoAssembly()
    {
        _config = _config with
        {
            AssemblyPaths = _config.AssemblyPaths
                .Union(new[] { DtoAssemblyPath })
                .ToArray()
        };

        return this;
    }
    
    public override TestConfigBuilder AddNonExistentRootService()
    {
        _config = _config with
        {
            RootServiceFullTypeNames = _config.RootServiceFullTypeNames
                                           .Union(
                                               new[]
                                               {
                                                   new KeyValuePair<string, string>(
                                                       "NonExistentRootService", 
                                                       "SvcCom.Samples.SampleWiki.INonExistentService"
                                                       )
                                               })
                                           .ToDictionary()
        };

        return this;
    }

    public override TestConfigBuilder AddMainRootService()
    {
        _config = _config with
        {
            RootServiceFullTypeNames = _config.RootServiceFullTypeNames
                .Union(
                    new[]
                    {
                        new KeyValuePair<string, string>(
                            "MainRootService", 
                            "SvcCom.Samples.SampleWiki.IWiki"
                        )
                    })
                .ToDictionary()
        };
        
        return this;
    }

    public override TestConfigBuilder AddDtoRootService()
    {
        _config = _config with
        {
            RootServiceFullTypeNames = _config.RootServiceFullTypeNames
                .Union(
                    new[]
                    {
                        new KeyValuePair<string, string>(
                            "DtoRootService", 
                            "SvcCom.Samples.SampleWiki.Dtos.IDataConverterService"
                        )
                    })
                .ToDictionary()
        };
        
        return this;
    }
}