namespace SvcCom.Registries;

public sealed class TypeRegistry
{
    private readonly List<TypeRegistryEntry> _entries = new();

    public IReadOnlyCollection<TypeRegistryEntry> Entries => _entries;

    public void Add(TypeRegistryEntry entry)
    {
        if(!_entries.Any(t => t.TypeFullName == entry.TypeFullName && t.IsService == entry.IsService))
            _entries.Add(entry);
    }
}