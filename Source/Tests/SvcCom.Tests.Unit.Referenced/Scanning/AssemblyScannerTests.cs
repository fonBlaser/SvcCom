using Xunit;

namespace SvcCom.Tests.Unit.Referenced.Scanning;

[Trait("Isolation", "Referenced")]
public class AssemblyScannerTests : Unit.Scanning.AssemblyScannerTests
{
    protected override string ExistentAssemblyPath => ReferencedTestConfig.TargetAssemblyPath;
    protected override string NonExistentAssemblyPath => ReferencedTestConfig.TargetAssemblyPath + ".random.ext";
}