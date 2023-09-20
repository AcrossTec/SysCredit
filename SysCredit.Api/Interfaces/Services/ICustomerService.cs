namespace SysCredit.Api.Interfaces.Services;

using SysCredit.Api.Requests;
using SysCredit.Api.Requests.Customers;

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

    ValueTask<EntityId> InsertCustomerAsync(CreateCustomerRequest Request);

    ValueTask<IServiceResult<IAsyncEnumerable<ReferenceInfo>?>> FetchReferencesByCustomerIdAsync(CustomerIdRequest Request);

    ValueTask<IServiceResult<IAsyncEnumerable<LoanInfo>?>> FetchLoansByCustomerIdAsync(CustomerIdRequest Request);

    ValueTask<IServiceResult<IAsyncEnumerable<GuarantorInfo>?>> FetchGuarantorsByCustomerIdAsync(CustomerIdRequest Request);
}