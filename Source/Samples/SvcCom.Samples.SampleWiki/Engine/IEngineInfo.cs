namespace SvcCom.Samples.SampleWiki.Engine;

public interface IEngineInfo
{
    public void ThrowIfError();
    public Task ThrowIfErrorAsync();

    public bool IsAlive();
    public Task<bool> IsAliveAsync();
}