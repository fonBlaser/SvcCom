using SvcCom.Exceptions;
using SvcCom.Scanning;

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
    public void AssemblyScannerConstructor_WithExistingAssemblyPath_ThrowsException()
    {
        AssemblyScanner scanner = new(TargetAssemblyPath, null);

        Assert.NotNull(scanner.Config);
        Assert.False(scanner.Config.InterfacePropertiesAreServices);
    }

    [Fact]
    public async Task Scan_ForCorruptedAssemblyBinary_ThrowsException()
    {
        string corruptedAssemblyPath = Path.Combine(CurrentTestDirectory, "Corrupted.dll");
        await File.WriteAllTextAsync(corruptedAssemblyPath, Guid.NewGuid().ToString());

        AssemblyScanner scanner = new(corruptedAssemblyPath, null);

        AssemblyLoadException exception = await Assert.ThrowsAsync<AssemblyLoadException>(scanner.Scan);
        Assert.IsType<BadImageFormatException>(exception.InnerException);
    }
}