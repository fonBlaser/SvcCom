using System.Reflection;
using SvcCom.Configs;
using SvcCom.Samples.SampleWiki;
using SvcCom.Samples.SampleWiki.Dtos;

namespace SvcCom.Tests.Unit.Referenced;

public class ReferencedTestConfigBuilder : TestConfigBuilder
{
    private ReferencedScanConfig _config = new();

    public override ScanConfig Build()
        => _config;

    public override TestConfigBuilder AddNonExistentAssembly() 
        => throw new NotSupportedException("Cannot add non-existent assembly in referenced mode.");
    
    public override TestConfigBuilder AddCorruptedAssembly(string location)
        => throw new NotSupportedException("Cannot add corrupted assembly in referenced mode.");

    public override TestConfigBuilder AddMainAssembly()
        => AddAssembly(typeof(IWiki).Assembly);
    
    public override TestConfigBuilder AddDtoAssembly()
        => AddAssembly(typeof(IDataConverterService).Assembly);
    
    public override TestConfigBuilder AddNonExistentRootService()
        => throw new NotSupportedException("Cannot add non-existent root service in referenced mode.");

    public override TestConfigBuilder AddMainRootService()
        => AddRootService("MainRootService", typeof(IWiki));

    public override TestConfigBuilder AddDtoRootService()
        => AddRootService("DtoRootService", typeof(IDataConverterService));
    
    private TestConfigBuilder AddAssembly(Assembly assembly)
    {
        _config = _config with
        {
            Assemblies = _config.Assemblies
                .Union(new[] { assembly })
                .ToArray()
        };

        return this;
    }
    
    private TestConfigBuilder AddRootService(string key, Type type)
    {
        _config = _config with
        {
            RootServiceTypes = _config.RootServiceTypes
                .Union(new Dictionary<string, Type>
                {
                    { key, type }
                })
                .ToDictionary(kv => kv.Key, kv => kv.Value) 
        };

        return this;
    }
}