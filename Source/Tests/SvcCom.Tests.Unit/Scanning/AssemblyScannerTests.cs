﻿using SvcCom.Exceptions;
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
            => new AssemblyScanner(invalidPath, new(InterfacePropertiesAreServices: false)));

        Assert.IsType<FileNotFoundException>(exception.InnerException);
    }

    [Fact]
    public void AssemblyScannerConstructor_WithExistingAssemblyPath_CreatesDefaultConfig()
    {
        AssemblyScanner scanner = new(TargetAssemblyPath, null);

        Assert.NotNull(scanner.Config);
        Assert.False(scanner.Config.InterfacePropertiesAreServices);
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
        AssemblyScanner scanner = new(corruptedAssemblyPath, null);

        AssemblyLoadException exception = await Assert.ThrowsAsync<AssemblyLoadException>(async () => await scanner.Scan(target));
        Assert.IsType<BadImageFormatException>(exception.InnerException);
    }

    [Fact]
    public async Task ScanValidAssembly_WithoutSpecifiedServices_ReturnsEmptySchema()
    {
        AssemblyScanner scanner = new(TargetAssemblyPath, new(InterfacePropertiesAreServices: false));

        ScanTarget target = new([]);
        AssemblySchema schema = await scanner.Scan(target);

        Assert.NotNull(schema);
        Assert.Empty(schema.Types);
    }

    [Fact]
    public async Task ScanValidAssembly_WithOneSpecifiedService_ReturnsSchemaWithSpecifiedType()
    {
        AssemblyScanner scanner = new(TargetAssemblyPath, new(InterfacePropertiesAreServices: true));
        string rootServiceFullName = "SvcCom.Samples.SampleWiki.IWiki";

        ScanTarget target = new(RootServices:
            [new ScanTargetService(rootServiceFullName)]
        );
        AssemblySchema schema = await scanner.Scan(target);

        Assert.NotNull(schema);
        Assert.NotEmpty(schema.Types);
        Assert.Contains(schema.Types, type => type.Namespace + type.Name == rootServiceFullName);
    }
}