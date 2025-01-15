namespace SvcCom.Samples.SampleWiki.Dtos;

public interface IDataConverterService
{
    public Task<string?> ConvertIntToStringAsync(int? arg);
}