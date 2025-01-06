namespace SvcCom.Samples.SampleWiki.Engine;

public interface IEngineInfo
{
    public Version? Version { get; }
    public string StatusLabel { get; }

    public void ThrowIfError();
    public Task ThrowIfErrorAsync();

    public bool IsAlive();
    public Task<bool> IsAliveAsync();
}