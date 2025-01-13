using Xunit;

namespace SvcCom.Tests.Unit.Isolated.Scanning;

[Trait("Isolation", "Isolated")]
public class AssemblyScannerTests : Unit.Scanning.AssemblyScannerTests
{
    protected override string ExistentAssemblyPath => IsolatedTestConfig.TargetAssemblyPath;
    protected override string NonExistentAssemblyPath => IsolatedTestConfig.TargetAssemblyPath + ".random.ext";
}