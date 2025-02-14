namespace SvcCom.Schemas;

public class NamedValueSchema : ValueSchema
{
    public string Name { get; }

    public NamedValueSchema(string name, TypeSchema schema, bool isNullable) 
        : base(schema, isNullable)
    {
        Name = name;
    }
}