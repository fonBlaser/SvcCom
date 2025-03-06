using System.Collections;
using SvcCom.Schemas.Values;

namespace SvcCom.Schemas.Types;

public class EnumTypeSchema : TypeSchema, IEnumerable<EnumValueSchema>
{
    private readonly HashSet<EnumValueSchema> _values = new(new EnumValueSchemaEqualityComparer());
    public IEnumerable<EnumValueSchema> Values => _values;

    public EnumTypeSchema(Type type) 
        : base(type)
    {
        if(!type.IsEnum)
            throw new ArgumentException("Type is not Enum.", nameof(type));
    }

    #region Enumerators

    public IEnumerator<EnumValueSchema> GetEnumerator()
        => _values.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
        => GetEnumerator();

    #endregion

    private class EnumValueSchemaEqualityComparer : IEqualityComparer<EnumValueSchema>
    {
        public bool Equals(EnumValueSchema? x, EnumValueSchema? y)
            => x?.Name == y?.Name;

        public int GetHashCode(EnumValueSchema obj)
            => obj.Name.GetHashCode();
    }
}