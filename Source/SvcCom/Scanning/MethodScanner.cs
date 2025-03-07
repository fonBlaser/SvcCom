using SvcCom.Schemas;

namespace SvcCom.Scanning;

public class MethodScanner : ScannerBase
{
    public MethodScanner(TypeSchemaRegistry registry) 
        : base(registry)
    {
    }
}