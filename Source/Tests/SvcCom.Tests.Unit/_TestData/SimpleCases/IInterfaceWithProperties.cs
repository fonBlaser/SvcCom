namespace SvcCom.Tests.Unit._TestData.SimpleCases;

public interface IInterfaceWithProperties
{
    public bool PropertyWithPublicGetter { get; set; }
    public bool PropertyWithInternalGetter { internal get; set; }
    
    public bool? NullablePropertyWithPublicGetter { get; set; }
    public Version VersionPropertyWithPublicGetter { get; set; }
    
    public Task<string> TaskValuePropertyWithPublicGetter { get; set; }
    public Task<string?> TaskNullablePropertyWithPublicGetter { get; set; }
    
    public Task TaskPropertyWithPublicGetter { get; set; }
}