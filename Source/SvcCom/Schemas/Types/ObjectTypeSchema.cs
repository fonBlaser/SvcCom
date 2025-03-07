using SvcCom.Utility.Extensions;

namespace SvcCom.Schemas.Types;

public class ObjectTypeSchema  : TypeSchema
{
    public bool IsInterface { get; }
    public bool IsReferenceType { get; }
    public bool IsValueType => !IsReferenceType;

    public ObjectTypeSchema(Type type) 
        : base(type)
    {
        if (!type.IsObject())
            throw new ArgumentException($"Specified type '{type.FullName}' is not an object type.", nameof(type));

        IsInterface = type.IsInterface;
        IsReferenceType = type.IsClass || type.IsInterface;
    }
}