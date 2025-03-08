using SvcCom.Schemas.Values;

namespace SvcCom.Schemas.ObjectComponents;

//ToDo: Rename to MemberSchema?
//ToDo: Extract MemberDetails class?
//ToDo: Parameters are not members - define the behaviour?
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