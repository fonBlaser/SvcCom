using SvcCom.Configs;
using SvcCom.Scanning;
using Xunit;

namespace SvcCom.Tests.Unit.Scanning;

[Trait("Category", "Unit")]
public abstract class ScannerFactoryTests : TestBase
{
    [Fact]
    public async Task ScannerFactory_WithNullConfig_ThrowsException()
    {
        ScanConfig config = null!;
        
        await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await ScannerFactory.Create(config));
    }
    
    [Fact]
    public async Task ScannerFactory_WithEmptyAssemblies_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .Build();
        
        await Assert.ThrowsAnyAsync<ArgumentException>(async () => await ScannerFactory.Create(config));
    }

    [Fact]
    public virtual async Task ScannerFactory_WithOneNonExistentAssembly_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddNonExistentAssembly()
            .Build();
        
        await Assert.ThrowsAnyAsync<FileNotFoundException>(async () => await ScannerFactory.Create(config));
    }

    [Fact]
    public virtual async Task ScannerFactory_WithOneCorruptedAssembly_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddCorruptedAssembly(AdditionalAssembliesDirectory)
            .Build();

        await Assert.ThrowsAnyAsync<BadImageFormatException>(async () => await ScannerFactory.Create(config));
    }
    
    [Fact]
    public virtual async Task ScannerFactory_WithMainAndNonExistentAssemblies_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddNonExistentAssembly()
            .Build();

        await Assert.ThrowsAnyAsync<FileNotFoundException>(async () => await ScannerFactory.Create(config));
    }
    
    [Fact]
    public virtual async Task ScannerFactory_WithMainAndCorruptedAssemblies_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddCorruptedAssembly(AdditionalAssembliesDirectory)
            .Build();

        await Assert.ThrowsAnyAsync<BadImageFormatException>(async () => await ScannerFactory.Create(config));
    }

    [Fact]
    public async Task ScannerFactory_WithMainAssemblyAndNoRootServices_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .Build();
        
        await Assert.ThrowsAnyAsync<ArgumentException>(async () => await ScannerFactory.Create(config));
    }
    
    [Fact]
    public virtual async Task ScannerFactory_WithMainAssemblyAndNonExistentRootService_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddNonExistentRootService()
            .Build();
        
        await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await ScannerFactory.Create(config));
    }

    [Fact]
    public virtual async Task ScannerFactory_WithMainAssemblyAndDtoRootService_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddDtoRootService()
            .Build();
        
        await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await ScannerFactory.Create(config));
    }
    
    [Fact]
    public virtual async Task ScannerFactory_WithDtoAssemblyAndMainRootService_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddDtoAssembly()
            .AddMainRootService()
            .Build();
        
        await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await ScannerFactory.Create(config));
    }
    
    [Fact]
    public async Task ScannerFactory_WithMainAssemblyAndMainRootService_CreatesScanner()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddMainRootService()
            .Build();

        Scanner scanner = await ScannerFactory.Create(config);
        
        Assert.NotNull(scanner);
    }
    
    [Fact]
    public async Task ScannerFactory_WithDtoAssemblyAndDtoRootService_CreatesScanner()
    {
        ScanConfig config = ConfigBuilder
            .AddDtoAssembly()
            .AddDtoRootService()
            .Build();
        
        Scanner scanner = await ScannerFactory.Create(config);
        
        Assert.NotNull(scanner);
    }
    
    [Fact]
    public async Task ScannerFactory_WithMainAssemblyAndMainRootServiceAndDtoAssemblyAndDtoRootService_CreatesScanner()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddMainRootService()
            .AddDtoAssembly()
            .AddDtoRootService()
            .Build();
        
        Scanner scanner = await ScannerFactory.Create(config);
        
        Assert.NotNull(scanner);
    }
}