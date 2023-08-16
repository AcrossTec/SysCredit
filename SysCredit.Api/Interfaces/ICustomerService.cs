namespace SysCredit.Api.Interfaces;

using SysCredit.Api.ViewModels;
using SysCredit.Api.ViewModels.Customers;

using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;

public interface ICustomerService
{
    IAsyncEnumerable<SearchCustomer> SearchCustomerAsync(SearchRequest Request);

    IAsyncEnumerable<CustomerInfo> FetchCustomersAsync();

    ValueTask<CustomerInfo?> FetchCustomerByIdAsync(long? CustomerId);

    ValueTask<CustomerInfo?> FetchCustomerByIdentificationAsync(string? Identification);

    ValueTask<CustomerInfo?> FetchCustomerByEmailAsync(string? Email);

    ValueTask<CustomerInfo?> FetchCustomerByPhoneAsync(string? Phone);

    ValueTask<IServiceResult<EntityId?>> InsertCustomerAsync(CreateCustomerRequest Request);
}
