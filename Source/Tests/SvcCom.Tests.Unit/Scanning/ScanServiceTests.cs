using SvcCom.Config;
using SvcCom.Scanning;
using SvcCom.Schemas;
using SvcCom.Tests.Unit._TestData.SimpleCase;
using Xunit;

namespace SvcCom.Tests.Unit.Scanning;

[Trait("Category", "Unit")]
public class ScanServiceTests
{
    [Fact]
    public void AddTypeSchema_AddPreviouslyConfiguredInterface_ToRegistry()
    {
        Scanner scanner = new TestScannerBuilder()
            .AddServiceType(typeof(IRootService))
            .Build();
        
        TypeSchema schema = scanner.AddTypeSchema(typeof(IRootService));

        Assert.NotNull(schema);
        Assert.Single(scanner.Registry.Types);
        Assert.Same(schema, scanner.Registry.Types.First());
    }
    
    [Fact]
    public void AddTypeSchema_Twice_AddsSchemaOnlyOnce()
    {
        Scanner scanner = new TestScannerBuilder()
            .AddServiceType(typeof(IRootService))
            .Build();
        
        TypeSchema schema1 = scanner.AddTypeSchema(typeof(IRootService));
        TypeSchema schema2 = scanner.AddTypeSchema(typeof(IRootService));

        Assert.NotNull(schema1);
        Assert.NotNull(schema2);
        Assert.Single(scanner.Registry.Types);
        Assert.Same(schema1, schema2);
        Assert.Same(schema1, scanner.Registry.Types.First());
    }

    [Fact]
    public void AddPropertiesToTypeSchema_AddsAllPropertiesWithAppropriateTypes_ToPropertyListAndSchemaRegistry()
    {
        Scanner scanner = new TestScannerBuilder()
            .AddServiceType(typeof(IRootService))
            .AddServiceType(typeof(ISubService))
            .AddServiceType(typeof(IAnotherSubService))
            .Build();   
        
        TypeSchema schema = scanner.AddTypeSchema(typeof(IRootService));
        scanner.AddProperties(schema);

        Assert.NotNull(schema.Properties);

        NamedValueSchema? subProperty 
            = schema.Properties.FirstOrDefault(p => p.Name == nameof(IRootService.Sub));
        Assert.NotNull(subProperty);
        Assert.Equal(typeof(ISubService).FullName, subProperty.TypeSchema.Name);
        
        NamedValueSchema? anotherSubProperty 
            = schema.Properties.FirstOrDefault(p => p.Name == nameof(IRootService.AnotherSub));
        Assert.NotNull(anotherSubProperty);
        Assert.Equal(typeof(IAnotherSubService).FullName, anotherSubProperty.TypeSchema.Name);
        
        NamedValueSchema? yetAnotherSubProperty 
            = schema.Properties.FirstOrDefault(p => p.Name == nameof(IRootService.YetAnotherSub));
        Assert.NotNull(yetAnotherSubProperty);
        Assert.Equal(typeof(IAnotherSubService).FullName, yetAnotherSubProperty.TypeSchema.Name);
    }
    
    [Fact]
    public void AddPropertiesToTypeSchema_Twice_DoesNotChangePropertyList()
    {
        Scanner scanner = new TestScannerBuilder()
            .AddServiceType(typeof(IRootService))
            .AddServiceType(typeof(ISubService))
            .AddServiceType(typeof(IAnotherSubService))
            .Build();   
        
        TypeSchema schema = scanner.AddTypeSchema(typeof(IRootService));

        scanner.AddProperties(schema);
        int initialPropertyCount = schema.Properties.Count;
        int initialTypesCount = scanner.Registry.Types.Count;
        string[] initialPropertyNames = schema.Properties.Select(p => p.Name).ToArray();
        string[] initialTypeNames = scanner.Registry.Types.Select(t => t.Name).ToArray();
        
        scanner.AddProperties(schema);
        int finalPropertyCount = schema.Properties.Count;
        int finalTypesCount = scanner.Registry.Types.Count;
        string[] finalPropertyNames = schema.Properties.Select(p => p.Name).ToArray();
        string[] finalTypeNames = scanner.Registry.Types.Select(t => t.Name).ToArray();
        
        Assert.Equal(initialPropertyCount, finalPropertyCount);
        Assert.Equal(initialTypesCount, finalTypesCount);
        Assert.Equal(initialPropertyNames, finalPropertyNames);
        Assert.Equal(initialTypeNames, finalTypeNames);
    }

    private class TestScannerBuilder
    {
        private List<string> ServiceTypeFullNames { get; } = new();

        internal TestScannerBuilder AddServiceType(Type type)
        {
            if(string.IsNullOrWhiteSpace(type.FullName))
                throw new ArgumentException("Type must have a full name", nameof(type));
            
            if(!ServiceTypeFullNames.Any(t => string.Equals(t, type.FullName, StringComparison.OrdinalIgnoreCase)))
                ServiceTypeFullNames.Add(type.FullName);
                
            return this;
        }
        
        internal Scanner Build()
        {
            SchemaRegistry registry = new();
            ScannerConfig config = new ScannerConfig()
            {
                ServiceTypeFullNames = ServiceTypeFullNames.ToArray()
            };
            
            Scanner scanner = new(config, registry);
            return scanner;
        }
    }
}