namespace SvcCom.Schemas;

public class TypeSchema
{
    private List<ValuableSchema> _properties = new();

    public string Name { get; }
    public string Namespace { get; }

    public IReadOnlyList<ValuableSchema> Properties => _properties;

    public TypeSchema(Type type)
    {
        Name = type.Name;
        Namespace = type.Namespace ?? string.Empty;
    }
}