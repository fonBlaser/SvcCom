namespace SvcCom.Tests.Unit;

public abstract class TestBase : IDisposable
{
    protected string CurrentTestId { get; }
    protected string CurrentTestDirectory { get; }

    private string Configuration
#if DEBUG
        => "Debug";
#else
        => "Release";
#endif

    protected virtual string TargetAssemblyName 
        => "SvcCom.Samples.SampleWiki";

    protected string TargetAssemblyPath
        => Path.Combine(Directory.GetCurrentDirectory(), $"../../../../../Samples/{TargetAssemblyName}/bin/{Configuration}/net8.0/{TargetAssemblyName}.dll");

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