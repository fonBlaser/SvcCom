using Xunit;

namespace SvcCom.Tests.Unit;

public abstract class TestBase : IDisposable
{
    protected string CurrentTestId { get; }
    protected string CurrentTestDirectory { get; }
    protected TestConfig Config => GetConfig();
    protected string ExistentAssemblyPath => Config.TargetAssemblyPath;
    protected string NonExistentAssemblyPath => Config.TargetAssemblyPath + ".random.ext";
    
    
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
    
    protected abstract TestConfig GetConfig();
}