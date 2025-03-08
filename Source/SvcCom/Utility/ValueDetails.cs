namespace SvcCom.Utility;

public record ValueDetails
{
    public Type ValueType { get; }
    public bool IsNullable { get; }
    public bool IsTask { get; }

    public ValueDetails(Type valueType, bool isNullable, bool isTask)
    {
        ValueType = valueType;
        IsNullable = isNullable;
        IsTask = isTask;
    }
}