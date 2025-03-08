using SvcCom.Schemas.Types;

namespace SvcCom.Schemas.Values;

public class ValueSchema
{
    public TypeSchema Type { get; }
    public bool IsNullable { get; }
    public bool IsTask { get; }

    public ValueSchema(TypeSchema type, bool isNullable, bool isTask)
    {
        Type = type;
        IsNullable = isNullable;
        IsTask = isTask;
    }
}