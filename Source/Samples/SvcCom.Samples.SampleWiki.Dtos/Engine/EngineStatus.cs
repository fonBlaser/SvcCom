namespace SvcCom.Samples.SampleWiki.Dtos.Engine;

public record EngineStatus
{
    public bool IsRunning { get; init; }
    public string? Label { get; init; }
}