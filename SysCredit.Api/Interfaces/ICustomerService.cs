namespace SysCredit.Api.Interfaces;

using SysCredit.Api.DataTransferObject.Commons;
using SysCredit.Api.DataTransferObject.StoredProcedures;
using SysCredit.Api.Helpers;
using SysCredit.Api.ViewModels;
using SysCredit.Api.ViewModels.Customers;

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
