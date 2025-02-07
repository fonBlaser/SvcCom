using SvcCom.Config;
using SvcCom.Samples.SampleWiki;
using SvcCom.Samples.SampleWiki.Authentication;
using SvcCom.Samples.SampleWiki.Content;
using SvcCom.Scanning;
using SvcCom.Schemas;
using Xunit;

namespace SvcCom.Tests.Unit.Scanning;

[Trait("Category", "Unit")]
public class ScanServiceTests
{
    [Fact]
    public void AddTypeSchema_AddPreviouslyConfiguredInterface_ToRegistry()
    {
        Scanner scanner = new TestScannerBuilder()
            .AddServiceType(typeof(IWiki))
            .Build();
        
        TypeSchema schema = scanner.AddTypeSchema(typeof(IWiki));

        Assert.NotNull(schema);
        Assert.Single(scanner.Registry.Types);
        Assert.Same(schema, scanner.Registry.Types.First());
    }
    
    [Fact]
    public void AddTypeSchema_Twice_AddsSchemaOnlyOnce()
    {
        Scanner scanner = new TestScannerBuilder()
            .AddServiceType(typeof(IWiki))
            .Build();
        
        TypeSchema schema1 = scanner.AddTypeSchema(typeof(IWiki));
        TypeSchema schema2 = scanner.AddTypeSchema(typeof(IWiki));

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
            .AddServiceType(typeof(IWiki))
            .AddServiceType(typeof(IAuthentication))
            .AddServiceType(typeof(IContent))
            .Build();   
        
        TypeSchema schema = scanner.AddTypeSchema(typeof(IWiki));
        scanner.AddProperties(schema);

        Assert.NotNull(schema.Properties);

        NamedValueSchema? authProperty 
            = schema.Properties.FirstOrDefault(p => p.Name == nameof(IWiki.Auth));
        Assert.NotNull(authProperty);
        Assert.Equal(typeof(IAuthentication).FullName, authProperty.TypeSchema.Name);
        
        NamedValueSchema? contentProperty 
            = schema.Properties.FirstOrDefault(p => p.Name == nameof(IWiki.Content));
        Assert.NotNull(contentProperty);
        Assert.Equal(typeof(IContent).FullName, contentProperty.TypeSchema.Name);
    }
    
    [Fact]
    public void AddPropertiesToTypeSchema_Twice_DoesNotChangePropertyList()
    {
        Scanner scanner = new TestScannerBuilder()
            .AddServiceType(typeof(IWiki))
            .AddServiceType(typeof(IAuthentication))
            .AddServiceType(typeof(IContent))
            .Build();   
        
        TypeSchema schema = scanner.AddTypeSchema(typeof(IWiki));

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