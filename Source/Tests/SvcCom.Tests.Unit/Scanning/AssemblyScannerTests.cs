using SvcCom.Exceptions;
using SvcCom.Scanning;
using Xunit;

namespace SvcCom.Tests.Unit.Scanning;

[Trait("Category", "Unit")]
public abstract class AssemblyScannerTests : TestBase
{
    private string CorruptedAssemblyPath => Path.Combine(CurrentTestDirectory, "CorruptedAssembly.dll");

    protected AssemblyScannerTests()
    {
        File.WriteAllBytes(CorruptedAssemblyPath, new byte[] { 0x00, 0x01, 0x02, 0x03 });
    }
    
    [Fact]
    public void AssemblyScanner_ThrowsException_WhenAssemblyIsNotFound()
    {
        string nonExistentAssembly = NonExistentAssemblyPath;
        
        Assert.ThrowsAny<AssemblyLoadException>(() => new AssemblyScanner(nonExistentAssembly));
    }
    
    [Fact]
    public void AssemblyScanner_DoesNotThrowException_WhenAssemblyIsFound()
    {
        string existentAssembly = ExistentAssemblyPath;
        
        Assert.Null(Record.Exception(() => new AssemblyScanner(existentAssembly)));
    }
    
    [Fact]
    public void AssemblyScanner_ThrowsException_WhenAssemblyIsCorrupted()
    {
        string corruptedAssembly = CorruptedAssemblyPath;
        
        Assert.ThrowsAny<AssemblyLoadException>(() => new AssemblyScanner(corruptedAssembly));
    }
}