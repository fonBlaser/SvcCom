using SvcCom.Schemas;

namespace SvcCom.Scanning;

public class EventScanner : ScannerBase
{
    public EventScanner(TypeSchemaRegistry registry) 
        : base(registry)
    {
    }
}