using System.Reflection;
using SvcCom.Config;
using SvcCom.Scanning;
using SvcCom.Schemas;
using SvcCom.Tests.Unit._TestData.CasesWithMethods;
using SvcCom.Tests.Unit._TestData.CasesWithProperties;
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
        
        TypeSchema schema = scanner.GetOrCreateTypeSchema(typeof(IRootService));

        Assert.NotNull(schema);
        Assert.Single(scanner.Registry.Schemas);
        Assert.Same(schema, scanner.Registry.Schemas.First());
    }
    
    [Fact]
    public void AddTypeSchema_Twice_AddsSchemaOnlyOnce()
    {
        Scanner scanner = new TestScannerBuilder()
            .AddServiceType(typeof(IRootService))
            .Build();
        
        TypeSchema schema1 = scanner.GetOrCreateTypeSchema(typeof(IRootService));
        TypeSchema schema2 = scanner.GetOrCreateTypeSchema(typeof(IRootService));

        Assert.NotNull(schema1);
        Assert.NotNull(schema2);
        Assert.Single(scanner.Registry.Schemas);
        Assert.Same(schema1, schema2);
        Assert.Same(schema1, scanner.Registry.Schemas.First());
    }

    [Fact]
    public void IsPropertySuitable_ForPublicPropertyWithInternalGetter_ReturnsFalse()
    {
        Scanner scanner = new TestScannerBuilder()
            .Build();
        
        PropertyInfo? p = typeof(IServiceWithProperties)
            .GetProperty(nameof(IServiceWithProperties.PropertyWithInternalGetter));
        
        bool isSuitable = scanner.IsPropertySuitable(p!);
        
        Assert.False(isSuitable);
    }

    [Fact]
    public void IsPropertySuitable_ForPublicPropertyWithPublicAccessor_ReturnsTrue()
    {
        Scanner scanner = new TestScannerBuilder()
            .Build();
        
        PropertyInfo? p = typeof(IServiceWithProperties)
            .GetProperty(nameof(IServiceWithProperties.Ver));
        
        bool isSuitable = scanner.IsPropertySuitable(p!);
        
        Assert.True(isSuitable);
    }
    
    [Fact]
    public void IsPropertySuitable_ForInternalProperty_ReturnsFalse()
    {
        Scanner scanner = new TestScannerBuilder()
            .Build();
        
        PropertyInfo? p = typeof(IServiceWithProperties)
            .GetProperty(nameof(IServiceWithProperties.InternalProperty));
        
        bool isSuitable = scanner.IsPropertySuitable(p!);
        
        Assert.False(isSuitable);
    }
    
    [Fact]
    public void GetSuitableProperties_ReturnsOnlyPublicPropertiesWithPublicAccessors()
    {
        Scanner scanner = new TestScannerBuilder()
            .Build();
        
        PropertyInfo[] properties = scanner.GetSuitableProperties(typeof(IServiceWithProperties))
            .ToArray();

        Assert.Single(properties);
        Assert.Equal(nameof(IServiceWithProperties.Ver), properties.First().Name);
    }

    [Fact]
    public void AddPropertiesToTypeSchema_AddsAllPropertiesWithAppropriateTypes_ToPropertyListAndSchemaRegistry()
    {
        Scanner scanner = new TestScannerBuilder()
            .AddServiceType(typeof(IRootService))
            .AddServiceType(typeof(ISubService))
            .AddServiceType(typeof(IAnotherSubService))
            .Build();   
        
        TypeSchema schema = scanner.GetOrCreateTypeSchema(typeof(IRootService));
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
        
        TypeSchema schema = scanner.GetOrCreateTypeSchema(typeof(IRootService));

        scanner.AddProperties(schema);
        int initialPropertyCount = schema.Properties.Count;
        int initialTypesCount = scanner.Registry.Schemas.Count;
        string[] initialPropertyNames = schema.Properties.Select(p => p.Name).ToArray();
        string[] initialTypeNames = scanner.Registry.Schemas.Select(t => t.Name).ToArray();
        
        scanner.AddProperties(schema);
        int finalPropertyCount = schema.Properties.Count;
        int finalTypesCount = scanner.Registry.Schemas.Count;
        string[] finalPropertyNames = schema.Properties.Select(p => p.Name).ToArray();
        string[] finalTypeNames = scanner.Registry.Schemas.Select(t => t.Name).ToArray();
        
        Assert.Equal(initialPropertyCount, finalPropertyCount);
        Assert.Equal(initialTypesCount, finalTypesCount);
        Assert.Equal(initialPropertyNames, finalPropertyNames);
        Assert.Equal(initialTypeNames, finalTypeNames);
    }
    
    [Fact]
    public void IsMethodSuitable_ForInternalMethod_ReturnsFalse()
    {
        Scanner scanner = new TestScannerBuilder()
            .Build();
        
        MethodInfo? m = typeof(IServiceWithMethods)
            .GetMethod(nameof(IServiceWithMethods.InternalVoidMethodWithoutParameters));
        
        bool isSuitable = scanner.IsMethodSuitable(m!);
        
        Assert.False(isSuitable);
    }

    [Fact]
    public void IsMethodSuitable_ForPublicMethod_ReturnsTrue()
    {
        Scanner scanner = new TestScannerBuilder()
            .Build();
        
        MethodInfo? m = typeof(IServiceWithMethods)
            .GetMethod(nameof(IServiceWithMethods.PublicAsyncMethodWithParameters));
        
        bool isSuitable = scanner.IsMethodSuitable(m!);
        
        Assert.True(isSuitable);
    }

    [Fact]
    public void IsMethodSuitable_ForPublicMethodWithServiceReturnType_ReturnsFalse()
    {
        Scanner scanner = new TestScannerBuilder()
            .AddServiceType(typeof(IServiceWithMethods))
            .AddServiceType(typeof(ISubServiceWithMethods))
            .Build();
        
        MethodInfo? m = typeof(IServiceWithMethods)
            .GetMethod(nameof(IServiceWithMethods.PublicMethodReturningSubService));
        
        bool isSuitable = scanner.IsMethodSuitable(m!);
        
        Assert.False(isSuitable);
    }
    
    [Fact]
    public void IsMethodSuitable_ForPublicAsyncMethodWithServiceReturnType_ReturnsFalse()
    {
        Scanner scanner = new TestScannerBuilder()
            .AddServiceType(typeof(IServiceWithMethods))
            .AddServiceType(typeof(ISubServiceWithMethods))
            .Build();
        
        MethodInfo? m = typeof(IServiceWithMethods)
            .GetMethod(nameof(IServiceWithMethods.PublicAsyncMethodReturningSubService));
        
        bool isSuitable = scanner.IsMethodSuitable(m!);
        
        Assert.False(isSuitable);
    }

    [Fact]
    public void GetSuitableMethods_ReturnsOnlyPublicMethods_WithoutServiceReturningMethods()
    {
        Scanner scanner = new TestScannerBuilder()
            .AddServiceType(typeof(IServiceWithMethods))
            .AddServiceType(typeof(ISubServiceWithMethods))
            .Build();
        
        MethodInfo[] methods = scanner.GetSuitableMethods(typeof(IServiceWithMethods))
            .ToArray();

        Assert.Equal(4, methods.Length);
        Assert.Contains(methods, m => m.Name == nameof(IServiceWithMethods.PublicVoidMethodWithoutParameters));
        Assert.Contains(methods, m => m.Name == nameof(IServiceWithMethods.PublicVoidMethodWithParameters));
        Assert.Contains(methods, m => m.Name == nameof(IServiceWithMethods.PublicAsyncMethodWithoutParameters));
        Assert.Contains(methods, m => m.Name == nameof(IServiceWithMethods.PublicAsyncMethodWithParameters));
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
            TypeSchemaRegistry registry = new();
            ScannerConfig config = new ScannerConfig()
            {
                ServiceTypeFullNames = ServiceTypeFullNames.ToArray()
            };
            
            Scanner scanner = new(config, registry);
            return scanner;
        }
    }
}