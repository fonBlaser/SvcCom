namespace SvcCom.Tests.Unit._TestData.SimpleCases;

public interface IInterfaceWithProperties
{
    public bool PropertyWithPublicGetterAndSetter { get; set; }
    public bool PropertyWithInternalSetter { get; internal set; }
    public bool PropertyWithInternalGetter { internal get; set; }

    public bool? NullablePropertyWithPublicGetter { get; set; }
    
    public Task TaskPropertyWithPublicGetter { get; set; }
    
    public Task<string> TaskValuePropertyWithPublicGetter { get; set; }
    public Task<string?> TaskNullablePropertyWithPublicGetter { get; set; }

    public Version VersionPropertyWithPublicGetter { get; set; }
    
    internal bool InternalBoolProperty { get; set; }
    internal Task InternalTaskProperty { get; set; }
    internal Task<bool> InternalGenericTaskProperty { get; set; }
}