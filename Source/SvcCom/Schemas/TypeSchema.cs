namespace SvcCom.Schemas;

public sealed class TypeSchema
{
    public Type Type { get; }
    public string FullName { get; set; }

    public TypeSchema(Type type)
    {
        Type = type;

        FullName = type.FullName 
                   ?? throw new InvalidOperationException("Anonymous types are not allowed: Type.FullName is null or empty.");
    }
}