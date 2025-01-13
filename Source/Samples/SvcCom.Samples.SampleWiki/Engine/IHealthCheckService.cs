namespace SvcCom.Samples.SampleWiki.Engine;

public interface IHealthCheckService
{
    public bool IsHealthy { get; }
    
    public void ThrowIfError();
    public Task ThrowIfErrorAsync();

    public bool IsAlive();
    public Task<bool> IsAliveAsync();
}