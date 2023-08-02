namespace SysCredit.Api.Interfaces;

using SysCredit.Api.DataTransferObject.StoredProcedures;
using SysCredit.Api.Helpers;
using SysCredit.Api.ViewModels.Customers;

public interface ICustomerService
{
    IAsyncEnumerable<FetchCustomer> FetchCustomersAsync();

    ValueTask<IServiceResult<EntityId?>> InsertCustomerAsync(CreateCustomerRequest Request);
}
