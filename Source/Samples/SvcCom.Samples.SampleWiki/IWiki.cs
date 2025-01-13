using SvcCom.Samples.SampleWiki.Engine;

namespace SvcCom.Samples.SampleWiki;

public interface IWiki
{
    public IEngineInfo EngineInfo { get; }
    public IHealthCheckService HealthCheck { get; }
}