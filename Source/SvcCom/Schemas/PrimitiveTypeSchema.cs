using SvcCom.Utility.Extensions;

namespace SvcCom.Schemas;

public class PrimitiveTypeSchema : TypeSchema
{
    public bool IsNumeric => Type.IsPrimitiveNumeric();
    public bool IsBool => Type.IsPrimitiveBool();

    public PrimitiveTypeSchema(Type type) 
        : base(type)
    {
        if(!type.IsPrimitive())
            throw new ArgumentException("Type is not primitive.", nameof(type));
    }
}