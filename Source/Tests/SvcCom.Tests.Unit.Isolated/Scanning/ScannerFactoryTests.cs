using Xunit;

namespace SvcCom.Tests.Unit.Isolated.Scanning;

[Trait("Isolation", "Isolated")]
public class ScannerFactoryTests : Unit.Scanning.ScannerFactoryTests
{
    protected override TestConfigBuilder ConfigBuilder => new IsolatedTestConfigBuilder();
}