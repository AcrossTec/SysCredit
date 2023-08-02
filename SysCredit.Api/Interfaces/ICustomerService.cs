namespace SysCredit.Api.Interfaces;

using SysCredit.Api.DataTransferObject.Commons;
using SysCredit.Api.Helpers;
using SysCredit.Api.ViewModels.Customers;

public interface ICustomerService
{
    IAsyncEnumerable<CustomerInfo> FetchCustomersAsync();

    ValueTask<IServiceResult<EntityId?>> InsertCustomerAsync(CreateCustomerRequest Request);
}
