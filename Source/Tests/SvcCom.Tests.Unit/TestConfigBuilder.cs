using SvcCom.Configs;

namespace SvcCom.Tests.Unit;

public abstract class TestConfigBuilder
{
    public abstract ScanConfig Build();
    public abstract TestConfigBuilder AddNonExistentAssembly();
    public abstract TestConfigBuilder AddCorruptedAssembly(string location);
    public abstract TestConfigBuilder AddMainAssembly();
    public abstract TestConfigBuilder AddDtoAssembly();
    public abstract TestConfigBuilder AddNonExistentRootService();
    public abstract TestConfigBuilder AddMainRootService();
    public abstract TestConfigBuilder AddDtoRootService();
}