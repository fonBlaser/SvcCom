using SvcCom.Schemas.Values;

namespace SvcCom.Schemas.ObjectComponents;

public class PropertySchema : NamedComponentSchema
{
    public bool CanGet { get; }
    public bool CanSet { get; }

    public PropertySchema(string name, ValueSchema value, bool canGet, bool canSet) 
        : base(name, value)
    {
        CanGet = canGet;
        CanSet = canSet;
    }
}