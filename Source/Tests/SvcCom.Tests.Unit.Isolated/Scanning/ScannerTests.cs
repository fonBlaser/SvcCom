using Xunit;

namespace SvcCom.Tests.Unit.Isolated.Scanning;

[Trait("Isolation", "Isolated")]
public class ScannerTests : Unit.Scanning.ScannerTests
{
    protected override TestConfigBuilder ConfigBuilder => new IsolatedTestConfigBuilder();
}