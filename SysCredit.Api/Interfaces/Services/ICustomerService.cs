namespace SysCredit.Api.Interfaces.Services;

using SysCredit.Api.Requests;
using SysCredit.Api.Requests.Customers;
using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;
using SysCredit.Models;

public partial interface ICustomerService : IService<Customer>
{
    IAsyncEnumerable<CustomerInfo> FetchCustomerAsync();

    IAsyncEnumerable<SearchCustomer> SearchCustomerAsync(SearchRequest Request);

    IAsyncEnumerable<ReferenceInfo> FetchReferenceByCustomerIdAsync(long? CustomerId);

    IAsyncEnumerable<LoanInfo> FetchLoanByCustomerIdAsync(long? CustomerId);

    IAsyncEnumerable<GuarantorInfo> FetchGuarantorByCustomerIdAsync(long? CustomerId);

    ValueTask<GuarantorInfo?> FetchGuarantorByCustomerIdAndGuarantorIdAsync(GuarantorAndCustomerIdsRequest Request);

    ValueTask<CustomerInfo?> FetchCustomerByIdAsync(long? CustomerId);

    ValueTask<CustomerInfo?> FetchCustomerByIdentificationAsync(string? Identification);

    ValueTask<CustomerInfo?> FetchCustomerByEmailAsync(string? Email);

    ValueTask<CustomerInfo?> FetchCustomerByPhoneAsync(string? Phone);

    ValueTask<EntityId> InsertCustomerAsync(CreateCustomerRequest Request);
}
