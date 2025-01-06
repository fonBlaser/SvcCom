namespace SvcCom.Schemas;

public class ValuableSchema
{
    public string Name { get; }
    public TypeSchema Type { get; }
    public bool IsNullable { get; }

    public ValuableSchema(string name, TypeSchema type, bool isNullable)
    {
        Name = name;
        Type = type;
        IsNullable = isNullable;
    }
}