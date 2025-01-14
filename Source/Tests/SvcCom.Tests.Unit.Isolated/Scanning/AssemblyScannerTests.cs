using Xunit;

namespace SvcCom.Tests.Unit.Isolated.Scanning;

[Trait("Isolation", "Isolated")]
public class AssemblyScannerTests : Unit.Scanning.AssemblyScannerTests
{
    protected override TestConfig GetConfig()
        => new IsolatedTestConfig();
}