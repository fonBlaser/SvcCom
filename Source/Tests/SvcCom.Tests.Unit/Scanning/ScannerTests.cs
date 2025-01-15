using SvcCom.Configs;
using SvcCom.Scanning;
using Xunit;

namespace SvcCom.Tests.Unit.Scanning;

[Trait("Category", "Unit")]
public abstract class ScannerTests : TestBase
{
    [Fact]
    public void ScannerConstructor_WithNullConfig_ThrowsException()
    {
        ScanConfig config = null!;
        
        Assert.ThrowsAny<ArgumentNullException>(() => new Scanner(config));
    }
    
    [Fact]
    public void ScannerConstructor_WithEmptyAssemblies_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .Build();
        
        Assert.ThrowsAny<ArgumentException>(() => new Scanner(config));
    }

    [Fact]
    public virtual void ScannerConstructor_WithOneNonExistentAssembly_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddNonExistentAssembly()
            .Build();
        
        Assert.ThrowsAny<FileNotFoundException>(() => new Scanner(config));
    }

    [Fact]
    public virtual void ScannerConstructor_WithOneCorruptedAssembly_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddCorruptedAssembly(AdditionalAssembliesDirectory)
            .Build();

        Assert.ThrowsAny<BadImageFormatException>(() => new Scanner(config));
    }
    
    [Fact]
    public virtual void ScannerConstructor_WithMainAndNonExistentAssemblies_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddNonExistentAssembly()
            .Build();

        Assert.ThrowsAny<FileNotFoundException>(() => new Scanner(config));
    }
    
    [Fact]
    public virtual void ScannerConstructor_WithMainAndCorruptedAssemblies_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddCorruptedAssembly(AdditionalAssembliesDirectory)
            .Build();

        Assert.ThrowsAny<BadImageFormatException>(() => new Scanner(config));
    }

    [Fact]
    public void ScannerConstructor_WithMainAssemblyAndNoRootServices_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .Build();
        
        Assert.ThrowsAny<ArgumentException>(() => new Scanner(config));
    }
    
    [Fact]
    public virtual void ScannerConstructor_WithMainAssemblyAndNonExistentRootService_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddNonExistentRootService()
            .Build();
        
        Assert.ThrowsAny<InvalidOperationException>(() => new Scanner(config));
    }

    [Fact]
    public virtual void ScannerConstructor_WithMainAssemblyAndDtoRootService_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddDtoRootService()
            .Build();
        
        Assert.ThrowsAny<InvalidOperationException>(() => new Scanner(config));
    }
    
    [Fact]
    public virtual void ScannerConstructor_WithDtoAssemblyAndMainRootService_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddDtoAssembly()
            .AddMainRootService()
            .Build();
        
        Assert.ThrowsAny<InvalidOperationException>(() => new Scanner(config));
    }
    
    [Fact]
    public void ScannerConstructor_WithMainAssemblyAndMainRootService_CreatesScanner()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddMainRootService()
            .Build();
        
        Scanner scanner = new(config);
        
        Assert.NotNull(scanner);
    }
    
    [Fact]
    public void ScannerConstructor_WithDtoAssemblyAndDtoRootService_CreatesScanner()
    {
        ScanConfig config = ConfigBuilder
            .AddDtoAssembly()
            .AddDtoRootService()
            .Build();
        
        Scanner scanner = new(config);
        
        Assert.NotNull(scanner);
    }
    
    [Fact]
    public void ScannerConstructor_WithMainAssemblyAndMainRootServiceAndDtoAssemblyAndDtoRootService_CreatesScanner()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddMainRootService()
            .AddDtoAssembly()
            .AddDtoRootService()
            .Build();
        
        Scanner scanner = new(config);
        
        Assert.NotNull(scanner);
    }
}