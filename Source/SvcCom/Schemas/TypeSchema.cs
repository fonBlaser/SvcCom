namespace SvcCom.Schemas;

public class TypeSchema
{
    public string Name { get; }
    public string Namespace { get; }

    public TypeSchema(Type type)
    {
        Name = type.Name;
        Namespace = type.Namespace ?? string.Empty;
    }
}