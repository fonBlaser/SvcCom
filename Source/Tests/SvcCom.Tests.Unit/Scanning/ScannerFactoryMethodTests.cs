using SvcCom.Configs;
using SvcCom.Scanning;
using Xunit;

namespace SvcCom.Tests.Unit.Scanning;

[Trait("Category", "Unit")]
public abstract class ScannerFactoryMethodTests : TestBase
{
    [Fact]
    public async Task ScannerFactoryMethod_WithNullConfig_ThrowsException()
    {
        ScanConfig config = null!;
        
        await Assert.ThrowsAnyAsync<ArgumentNullException>(async () => await Scanner.Create(config));
    }
    
    [Fact]
    public async Task ScannerFactoryMethod_WithEmptyAssemblies_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .Build();
        
        await Assert.ThrowsAnyAsync<ArgumentException>(async () => await Scanner.Create(config));
    }

    [Fact]
    public virtual async Task ScannerFactoryMethod_WithOneNonExistentAssembly_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddNonExistentAssembly()
            .Build();
        
        await Assert.ThrowsAnyAsync<FileNotFoundException>(async () => await Scanner.Create(config));
    }

    [Fact]
    public virtual async Task ScannerFactoryMethod_WithOneCorruptedAssembly_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddCorruptedAssembly(AdditionalAssembliesDirectory)
            .Build();

        await Assert.ThrowsAnyAsync<BadImageFormatException>(async () => await Scanner.Create(config));
    }
    
    [Fact]
    public virtual async Task ScannerFactoryMethod_WithMainAndNonExistentAssemblies_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddNonExistentAssembly()
            .Build();

        await Assert.ThrowsAnyAsync<FileNotFoundException>(async () => await Scanner.Create(config));
    }
    
    [Fact]
    public virtual async Task ScannerFactoryMethod_WithMainAndCorruptedAssemblies_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddCorruptedAssembly(AdditionalAssembliesDirectory)
            .Build();

        await Assert.ThrowsAnyAsync<BadImageFormatException>(async () => await Scanner.Create(config));
    }

    [Fact]
    public async Task ScannerFactoryMethod_WithMainAssemblyAndNoRootServices_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .Build();
        
        await Assert.ThrowsAnyAsync<ArgumentException>(async () => await Scanner.Create(config));
    }
    
    [Fact]
    public virtual async Task ScannerFactoryMethod_WithMainAssemblyAndNonExistentRootService_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddNonExistentRootService()
            .Build();
        
        await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await Scanner.Create(config));
    }

    [Fact]
    public virtual async Task ScannerFactoryMethod_WithMainAssemblyAndDtoRootService_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddDtoRootService()
            .Build();
        
        await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await Scanner.Create(config));
    }
    
    [Fact]
    public virtual async Task ScannerFactoryMethod_WithDtoAssemblyAndMainRootService_ThrowsException()
    {
        ScanConfig config = ConfigBuilder
            .AddDtoAssembly()
            .AddMainRootService()
            .Build();
        
        await Assert.ThrowsAnyAsync<InvalidOperationException>(async () => await Scanner.Create(config));
    }
    
    [Fact]
    public async Task ScannerFactoryMethod_WithMainAssemblyAndMainRootService_CreatesScanner()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddMainRootService()
            .Build();

        Scanner scanner = await Scanner.Create(config);
        
        Assert.NotNull(scanner);
    }
    
    [Fact]
    public async Task ScannerFactoryMethod_WithDtoAssemblyAndDtoRootService_CreatesScanner()
    {
        ScanConfig config = ConfigBuilder
            .AddDtoAssembly()
            .AddDtoRootService()
            .Build();
        
        Scanner scanner = await Scanner.Create(config);
        
        Assert.NotNull(scanner);
    }
    
    [Fact]
    public async Task ScannerFactoryMethod_WithMainAssemblyAndMainRootServiceAndDtoAssemblyAndDtoRootService_CreatesScanner()
    {
        ScanConfig config = ConfigBuilder
            .AddMainAssembly()
            .AddMainRootService()
            .AddDtoAssembly()
            .AddDtoRootService()
            .Build();
        
        Scanner scanner = await Scanner.Create(config);
        
        Assert.NotNull(scanner);
    }
}