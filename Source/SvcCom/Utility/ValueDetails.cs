namespace SvcCom.Utility;

public record ValueDetails
{
    public bool IsTask { get; }
    public bool IsNullable { get; }
    public Type ValueType { get; }

    public ValueDetails(bool isTask, bool isNullable, Type valueType)
    {
        IsTask = isTask;
        IsNullable = isNullable;
        ValueType = valueType;
    }
}