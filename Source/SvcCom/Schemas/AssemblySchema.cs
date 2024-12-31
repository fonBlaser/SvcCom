namespace SvcCom.Schemas;

public class AssemblySchema
{
    private List<TypeSchema> _types = new();

    public IEnumerable<TypeSchema> Types => _types;

    public AssemblySchema()
    {
    }
}