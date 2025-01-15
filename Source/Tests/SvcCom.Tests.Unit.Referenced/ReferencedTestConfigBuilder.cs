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
    {
        _config = _config with
        {
            Assemblies = _config.Assemblies
                               .Union(new[] { typeof(IWiki).Assembly })
                               .ToArray()
        };

        return this;
    }
    
    public override TestConfigBuilder AddDtoAssembly()
    {
        _config = _config with
        {
            Assemblies = _config.Assemblies
                .Union(new[] { typeof(IDataConverterService).Assembly })
                .ToArray()
        };

        return this;
    }
    
    public override TestConfigBuilder AddNonExistentRootService()
        => throw new NotSupportedException("Cannot add non-existent root service in referenced mode.");

    public override TestConfigBuilder AddMainRootService()
    {
        _config = _config with
        {
            RootServiceTypes = _config.RootServiceTypes
                .Union(new Dictionary<string, Type>
                {
                    { "MainRootService", typeof(IWiki) }
                })
                .ToDictionary(kv => kv.Key, kv => kv.Value) 
        };

        return this;
    }
    
    public override TestConfigBuilder AddDtoRootService()
    {
        _config = _config with
        {
            RootServiceTypes = _config.RootServiceTypes
                                    .Union(new Dictionary<string, Type>
                                    {
                                        { "DtoRootService", typeof(IDataConverterService) }
                                    })
                                    .ToDictionary(kv => kv.Key, kv => kv.Value) 
        };

        return this;
    }
}