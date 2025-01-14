using Xunit;

namespace SvcCom.Tests.Unit.Referenced.Scanning;

[Trait("Isolation", "Referenced")]
public class AssemblyScannerTests : Unit.Scanning.AssemblyScannerTests
{
    protected override TestConfig GetConfig()
        => new ReferencedTestConfig();
}