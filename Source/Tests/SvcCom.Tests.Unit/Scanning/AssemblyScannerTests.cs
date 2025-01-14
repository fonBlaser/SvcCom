using SvcCom.Exceptions;
using SvcCom.Scanning;
using Xunit;

namespace SvcCom.Tests.Unit.Scanning;

[Trait("Category", "Unit")]
public abstract class AssemblyScannerTests : TestBase
{
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
}