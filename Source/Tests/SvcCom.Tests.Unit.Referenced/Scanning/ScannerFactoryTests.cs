using Xunit;

namespace SvcCom.Tests.Unit.Referenced.Scanning;

[Trait("Isolation", "Referenced")]
public class ScannerFactoryTests : Unit.Scanning.ScannerFactoryTests
{
    protected override TestConfigBuilder ConfigBuilder => new ReferencedTestConfigBuilder();

    [Fact(Skip = "Referenced mode does not support non-existent assemblies.")]
    public override Task ScannerFactory_WithOneNonExistentAssembly_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support corrupted assemblies.")]
    public override Task ScannerFactory_WithOneCorruptedAssembly_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent assemblies.")]
    public override Task ScannerFactory_WithMainAndNonExistentAssemblies_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support corrupted assemblies.")]
    public override Task ScannerFactory_WithMainAndCorruptedAssemblies_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent root services.")]
    public override Task ScannerFactory_WithMainAssemblyAndNonExistentRootService_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent root services.")]
    public override Task ScannerFactory_WithMainAssemblyAndDtoRootService_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent root services.")]
    public override Task ScannerFactory_WithDtoAssemblyAndMainRootService_ThrowsException()
    {
        return Task.CompletedTask;
    }
}