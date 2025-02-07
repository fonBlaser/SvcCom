namespace SvcCom.Tests.Unit._TestData.SimpleCase;

public interface IRootService
{
    public ISubService Sub { get; }
    public IAnotherSubService AnotherSub { get; }
    public IAnotherSubService YetAnotherSub { get; }
}