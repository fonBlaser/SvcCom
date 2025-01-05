using SvcCom.Registries;

namespace SvcCom.Schemas;

public class AssemblySchema
{
    private List<TypeRegistryEntry> _rootServices = new();

    public string AssemblyFullName { get; }
    public TypeRegistry TypeRegistry { get; } = new();
    public IReadOnlyList<TypeRegistryEntry> RootServices => _rootServices;

    public AssemblySchema(string assemblyFullName)
    {
        if(string.IsNullOrWhiteSpace(assemblyFullName))
            throw new ArgumentException("AssemblyFullName cannot be null or whitespace.", nameof(assemblyFullName));

        AssemblyFullName = assemblyFullName;
    }

    public void AddRootService(TypeRegistryEntry entry)
    {
        _rootServices.Add(entry);
    }
}