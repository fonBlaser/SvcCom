using Xunit;

namespace SvcCom.Tests.Unit.Referenced.Scanning;

[Trait("Isolation", "Referenced")]
public class ScannerTests : Unit.Scanning.ScannerTests
{
    protected override TestConfigBuilder ConfigBuilder => new ReferencedTestConfigBuilder();

    [Fact(Skip = "Referenced mode does not support non-existent assemblies.")]
    public override void ScannerConstructor_WithOneNonExistentAssembly_ThrowsException()
    {
    }
    
    [Fact(Skip = "Referenced mode does not support corrupted assemblies.")]
    public override void ScannerConstructor_WithOneCorruptedAssembly_ThrowsException()
    {
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent assemblies.")]
    public override void ScannerConstructor_WithMainAndNonExistentAssemblies_ThrowsException()
    {
    }
    
    [Fact(Skip = "Referenced mode does not support corrupted assemblies.")]
    public override void ScannerConstructor_WithMainAndCorruptedAssemblies_ThrowsException()
    {
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent root services.")]
    public override void ScannerConstructor_WithMainAssemblyAndNonExistentRootService_ThrowsException()
    {
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent root services.")]
    public override void ScannerConstructor_WithMainAssemblyAndDtoRootService_ThrowsException()
    {
    }
    
    [Fact(Skip = "Referenced mode does not support non-existent root services.")]
    public override void ScannerConstructor_WithDtoAssemblyAndMainRootService_ThrowsException()
    {
    }
}