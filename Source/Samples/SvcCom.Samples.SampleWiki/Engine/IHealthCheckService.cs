namespace SvcCom.Samples.SampleWiki.Engine;

public interface IHealthCheckService
{
    public bool IsHealthy { get; }
    
    public bool IsAlive();
    public Task<bool> IsAliveAsync();
    
    public void ThrowIfError();
    public Task ThrowIfErrorAsync();
}