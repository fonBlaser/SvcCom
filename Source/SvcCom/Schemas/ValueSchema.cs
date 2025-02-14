namespace SvcCom.Schemas;

public class ValueSchema
{
    public TypeSchema Schema { get; }
    public bool IsNullable { get; }

    public ValueSchema(TypeSchema schema, bool isNullable)
    {
        Schema = schema;
        IsNullable = isNullable;
    }
}