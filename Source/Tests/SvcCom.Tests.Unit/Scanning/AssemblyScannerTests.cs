using SvcCom.Exceptions;
using SvcCom.Registries;
using SvcCom.Scanning;
using SvcCom.Schemas;

namespace SvcCom.Tests.Unit.Scanning;

[Trait("Category", "UnitTests")]
public class AssemblyScannerTests : TestBase
{
    [Fact]
    public void AssemblyScannerConstructor_WithNonExistentAssemblyPath_ThrowsException()
    {
        string invalidPath = Path.Combine(Directory.GetCurrentDirectory(), "InvalidPath.dll");

        AssemblyLoadException exception = Assert.Throws<AssemblyLoadException>(() 
            => new AssemblyScanner(invalidPath));

        Assert.IsType<FileNotFoundException>(exception.InnerException);
    }

    [Fact]
    public void AssemblyScannerConstructor_WithExistingAssemblyPath_CreatesDefaultConfig()
    {
        AssemblyScanner scanner = new(TargetAssemblyPath, null);

        Assert.NotNull(scanner.Config);
        Assert.True(scanner.Config.IncludePublicTypes);
        Assert.False(scanner.Config.IncludeInternalTypes);
        Assert.False(scanner.Config.IncludePrivateTypes);
    }

    [Fact]
    public async Task ScanCorruptedAssembly_WithValidTarget_ThrowsException()
    {
        string corruptedAssemblyPath = Path.Combine(CurrentTestDirectory, "Corrupted.dll");
        await File.WriteAllTextAsync(corruptedAssemblyPath, Guid.NewGuid().ToString());
        string rootServiceFullName = "SvcCom.Samples.SampleWiki.IWiki";

        ScanTarget target = new(RootServices:
            [new ScanTargetService(rootServiceFullName)]
        );
        AssemblyScanner scanner = new(corruptedAssemblyPath);

        AssemblyLoadException exception = await Assert.ThrowsAsync<AssemblyLoadException>(async () => await scanner.Scan(target));
        Assert.IsType<BadImageFormatException>(exception.InnerException);
    }

    [Fact]
    public async Task ScanValidAssembly_WithoutSpecifiedServices_ReturnsEmptySchema()
    {
        AssemblyScanner scanner = new(TargetAssemblyPath);

        ScanTarget target = new([]);
        AssemblySchema schema = await scanner.Scan(target);

        Assert.NotNull(schema);
        Assert.Empty(schema.TypeRegistry.Entries);
        Assert.Empty(schema.RootServices);
    }

    [Fact]
    public async Task ScanValidAssembly_WithOneSpecifiedService_ReturnsSchemaWithSpecifiedType()
    {
        AssemblyScanner scanner = new(TargetAssemblyPath);
        string rootServiceFullName = "SvcCom.Samples.SampleWiki.IWiki";

        ScanTarget target = new(RootServices:
            [new ScanTargetService(rootServiceFullName)]
        );
        AssemblySchema schema = await scanner.Scan(target);

        TypeRegistryEntry? service = schema.RootServices.FirstOrDefault(s => s.TypeFullName == rootServiceFullName);
        Assert.NotNull(service);
        Assert.True(service.Scanned);
        Assert.True(service.IsService);
        Assert.True(service.TypeFullName == rootServiceFullName);
        Assert.NotNull(service.Schema);
        Assert.Equal(rootServiceFullName, $"{service.Schema.Namespace}.{service.Schema.Name}");
        Assert.Contains(schema.TypeRegistry.Entries, entry => entry.TypeFullName == rootServiceFullName);
    }

    [Fact]
    public async Task ScannedService_ContainsProperty_WithTypeIncludedToRegistry()
    {
        AssemblyScanner scanner = new(TargetAssemblyPath);
        string rootServiceFullName = "SvcCom.Samples.SampleWiki.IWiki";

        ScanTarget target = new(RootServices:
            [new ScanTargetService(rootServiceFullName)]
        );
        AssemblySchema schema = await scanner.Scan(target);
        TypeSchema service = schema.RootServices.First(s => s.TypeFullName == rootServiceFullName).Schema!;

        Assert.NotNull(service);
        Assert.NotNull(service.Properties);
        Assert.NotEmpty(service.Properties);
        Assert.Contains(service.Properties, p => p.Name == "EngineInfo");
    }
}