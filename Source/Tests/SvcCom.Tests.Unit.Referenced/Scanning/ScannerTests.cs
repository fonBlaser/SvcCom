using Xunit;

namespace SvcCom.Tests.Unit.Referenced.Scanning;

[Trait("Isolation", "Referenced")]
public class ScannerTests : Unit.Scanning.ScannerTests
{
    protected override TestConfigBuilder ConfigBuilder => new ReferencedTestConfigBuilder();

    [Fact(Skip = "Referenced mode does not support non-existent assemblies.")]
    public override Task ScannerConstructor_WithOneNonExistentAssembly_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support corrupted assemblies.")]
    public override Task ScannerConstructor_WithOneCorruptedAssembly_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent assemblies.")]
    public override Task ScannerConstructor_WithMainAndNonExistentAssemblies_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support corrupted assemblies.")]
    public override Task ScannerConstructor_WithMainAndCorruptedAssemblies_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent root services.")]
    public override Task ScannerConstructor_WithMainAssemblyAndNonExistentRootService_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent root services.")]
    public override Task ScannerConstructor_WithMainAssemblyAndDtoRootService_ThrowsException()
    {
        return Task.CompletedTask;
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent root services.")]
    public override Task ScannerConstructor_WithDtoAssemblyAndMainRootService_ThrowsException()
    {
        return Task.CompletedTask;
    }
}