using Xunit;

namespace SvcCom.Tests.Unit.Isolated.Scanning;

[Trait("Isolation", "Isolated")]
public class ScannerFactoryMethodTests : Unit.Scanning.ScannerFactoryMethodTests
{
    protected override TestConfigBuilder ConfigBuilder => new IsolatedTestConfigBuilder();
}