using System.Net;

namespace SvcCom.Tests.Unit._TestData.CasesWithMethods;

public interface IServiceWithMethods
{
    public void PublicVoidMethodWithoutParameters();
    public void PublicVoidMethodWithParameters(Guid id, string name, IPAddress address, Version version);
    public Task PublicAsyncMethodWithoutParameters();
    public Task PublicAsyncMethodWithParameters(Guid id, string name, IPAddress address, Version version);

    public ISubServiceWithMethods PublicMethodReturningSubService();
    public Task<ISubServiceWithMethods> PublicAsyncMethodReturningSubService();
    
    internal void InternalVoidMethodWithoutParameters();
}