using SvcCom.Schemas.Types;

namespace SvcCom.Schemas.Values;

public class ValueSchema
{
    public TypeSchema Type { get; }
    public bool IsNullable { get; }
    public bool IsTask { get; }
    public bool IsTaskResultNullable { get; }

    public ValueSchema(TypeSchema type, bool isNullable, bool isTask, bool isTaskResultNullable)
    {
        Type = type;
        IsNullable = isNullable;
        IsTask = isTask;
        IsTaskResultNullable = isTaskResultNullable;
    }
}