using Xunit;

namespace SvcCom.Tests.Unit.Referenced.Scanning;

[Trait("Isolation", "Referenced")]
public class ScannerFactoryMethodTests : Unit.Scanning.ScannerFactoryMethodTests
{
    protected override TestConfigBuilder ConfigBuilder => new ReferencedTestConfigBuilder();

    [Fact(Skip = "Referenced mode does not support non-existent assemblies.")]
    public override Task ScannerFactoryMethod_WithOneNonExistentAssembly_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support corrupted assemblies.")]
    public override Task ScannerFactoryMethod_WithOneCorruptedAssembly_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent assemblies.")]
    public override Task ScannerFactoryMethod_WithMainAndNonExistentAssemblies_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support corrupted assemblies.")]
    public override Task ScannerFactoryMethod_WithMainAndCorruptedAssemblies_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent root services.")]
    public override Task ScannerFactoryMethod_WithMainAssemblyAndNonExistentRootService_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent root services.")]
    public override Task ScannerFactoryMethod_WithMainAssemblyAndDtoRootService_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent root services.")]
    public override Task ScannerFactoryMethod_WithDtoAssemblyAndMainRootService_ThrowsException()
    {
        return Task.CompletedTask;
    }
}