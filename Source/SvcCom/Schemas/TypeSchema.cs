namespace SvcCom.Schemas;

public class TypeSchema
{
    private List<NamedValueSchema> _properties = new();
    internal Type Type { get; }

    public IReadOnlyCollection<NamedValueSchema> Properties => _properties.AsReadOnly();
    public string Name => Type.FullName!;

    public TypeSchema(Type type)
    {
        if(type is null)
            throw new ArgumentNullException(nameof(type));
        
        if(string.IsNullOrWhiteSpace(type.FullName))
            throw new ArgumentException("Type must have a full name", nameof(type));
        
        Type = type;
    }

    public void AddProperty(NamedValueSchema schema)
    {
        if (_properties.Any(p => p.Name == schema.Name))
            return;
        
        _properties.Add(schema);
    }
}