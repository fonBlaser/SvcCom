using SvcCom.Samples.SampleWiki.Dtos.Engine;

namespace SvcCom.Samples.SampleWiki.Engine;

public interface IEngineInfo
{
    public Version? Version { get; }
    public EngineStatus Status { get; }
}