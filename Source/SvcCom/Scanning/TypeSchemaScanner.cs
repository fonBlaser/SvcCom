using SvcCom.Schemas;

namespace SvcCom.Scanning;

public class TypeSchemaScanner : ScannerBase
{
    public PropertyScanner PropertyScanner { get; }
    public MethodScanner MethodScanner { get; }
    public EventScanner EventScanner { get; }

    public TypeSchemaScanner(TypeSchemaRegistry registry) 
        : base(registry)
    {
        PropertyScanner = new(registry);
        MethodScanner = new(registry);
        EventScanner = new(registry);
    }
}