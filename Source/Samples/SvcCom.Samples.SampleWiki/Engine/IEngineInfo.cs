namespace SvcCom.Samples.SampleWiki.Engine;

public interface IEngineInfo
{
    public Version? Version { get; }
    public string StatusLabel { get; }
}