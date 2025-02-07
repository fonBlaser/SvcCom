namespace SvcCom.Schemas;

public class NamedValueSchema : ValueSchema
{
    public string Name { get; }

    public NamedValueSchema(string name, TypeSchema typeSchema, bool isNullable = false) 
        : base(typeSchema, isNullable)
    {
        Name = name;
    }
}
