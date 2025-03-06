using SvcCom.Utility.Extensions;

namespace SvcCom.Schemas.Types;

public class PrimitiveTypeSchema : TypeSchema
{
    public bool IsNumeric => Type.IsPrimitiveNumeric();
    public bool IsBool => Type.IsPrimitiveBool();
    public bool IsString => Type.IsPrimitiveString();
    public bool IsGuid => Type.IsPrimitiveGuid();

    public PrimitiveTypeSchema(Type type)
        : base(type)
    {
        if (!type.IsPrimitive())
            throw new ArgumentException("Type is not primitive.", nameof(type));
    }
}