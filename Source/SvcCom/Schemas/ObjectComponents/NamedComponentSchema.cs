using SvcCom.Schemas.Values;

namespace SvcCom.Schemas.ObjectComponents;

public class NamedComponentSchema
{
    public string Name { get; }
    public ValueSchema Value { get; }

    public NamedComponentSchema(string name, ValueSchema value)
    {
        Name = name;
        Value = value;
    }
}