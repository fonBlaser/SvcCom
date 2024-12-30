using SvcCom.SampleWiki.Engine;

namespace SvcCom.SampleWiki;

public interface IWiki
{
    public IEngineInfo EngineInfo { get; }
}