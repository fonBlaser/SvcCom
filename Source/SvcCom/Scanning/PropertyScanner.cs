using System.Reflection;
using SvcCom.Schemas;

namespace SvcCom.Scanning;

public class PropertyScanner : ScannerBase
{
    public PropertyScanner(TypeSchemaRegistry registry) 
        : base(registry)
    {
    }

    public List<PropertyInfo> GetAvailableProperties(Type type)
        => type.GetProperties()
               .Where(p => p.GetMethod?.IsPublic == true || p.SetMethod?.IsPublic == true)
               .ToList();
}