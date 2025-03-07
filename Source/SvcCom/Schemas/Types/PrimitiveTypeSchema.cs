using SvcCom.Utility.Extensions;

namespace SvcCom.Schemas.Types;

public class PrimitiveTypeSchema : TypeSchema
{
    public bool IsNumeric { get; }
    public bool IsBool { get; }
    public bool IsString { get; }
    public bool IsGuid { get; }

    public PrimitiveTypeSchema(Type type)
        : base(type)
    {
        if (!type.IsPrimitive())
            throw new ArgumentException("Type is not primitive.", nameof(type));
        
        IsNumeric = type.IsPrimitiveNumeric();
        IsBool = type.IsPrimitiveBool();
        IsString = type.IsPrimitiveString();
        IsGuid = type.IsPrimitiveGuid();
    }
}