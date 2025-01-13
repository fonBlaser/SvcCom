using Xunit;

namespace SvcCom.Tests.Unit;

public abstract class TestBase : IDisposable
{
    protected string CurrentTestId { get; }
    protected string CurrentTestDirectory { get; }
    
    protected TestBase()
    {
        CurrentTestId = Guid.NewGuid().ToString("D");
        CurrentTestDirectory = Path.Combine(Path.GetTempPath(), "SvcCom", "Tests", "Unit", CurrentTestId);

        if(!Directory.Exists(CurrentTestDirectory))
            Directory.CreateDirectory(CurrentTestDirectory);

        Assert.True(Directory.Exists(CurrentTestDirectory));
    }

    public void Dispose()
    {
        Directory.Delete(CurrentTestDirectory, recursive: true);
        Assert.False(Directory.Exists(CurrentTestDirectory));
    }
}