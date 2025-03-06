namespace SvcCom.Schemas.Types;

public class TypeSchema
{
    public Type Type { get; }
    public string Name { get; }
    public string FullName { get; }

    public TypeSchema(Type type)
    {
        Type = type;
        FullName = type.FullName 
                   ?? throw new InvalidOperationException("Anonymous types are not allowed: Type.FullName is null or empty.");
        Name = type.Name;
    }
}