namespace SvcCom.Schemas;

public class ValueSchema
{
    public bool IsNullable { get; }
    public TypeSchema TypeSchema { get; }

    public ValueSchema(TypeSchema typeSchema, bool isNullable = false)
    {
        IsNullable = isNullable;
        TypeSchema = typeSchema;
    }
}