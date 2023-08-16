namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Stores;
using SysCredit.Api.ViewModels;
using SysCredit.Api.ViewModels.Customers;

using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

using static Constants.ErrorCodeIndex;
using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;
using static SysCredit.Helpers.ContextData;

[Service<ICustomerService>]
[ErrorCategory(ErrorCategories.CustomerService)]
public class CustomerService : ICustomerService
{
    private readonly IStore<Customer> CustomerStore;
    private readonly IStore<Guarantor> GuarantorStore;
    private readonly IStore<Relationship> RelationshipStore;
    private readonly IStore<Reference> ReferenceStore;

    public CustomerService(IStore Store)
    {
        CustomerStore = Store.GetStore<Customer>();
        GuarantorStore = Store.GetStore<Guarantor>();
        RelationshipStore = Store.GetStore<Relationship>();
        ReferenceStore = Store.GetStore<Reference>();
    }

    [MethodId("62D2191D-EF87-4A97-BFF5-4F16A5B09411")]
    public ValueTask<CustomerInfo?> FetchCustomerByIdAsync(long? CustomerId)
    {
        return CustomerStore.FetchCustomerByIdAsync(CustomerId);
    }

    [MethodId("7D0C770B-A53A-4E82-8752-4BECB5F844F0")]
    public ValueTask<CustomerInfo?> FetchCustomerByIdentificationAsync(string? Identification)
    {
        return CustomerStore.FetchCustomerByIdentificationAsync(Identification);
    }

    [MethodId("DB423C0D-828E-4BCB-8A71-52B339C5E1C6")]
    public ValueTask<CustomerInfo?> FetchCustomerByEmailAsync(string? Email)
    {
        return CustomerStore.FetchCustomerByEmailAsync(Email);
    }

    [MethodId("0167F562-8E3E-4B3B-9039-AA4EDEF95692")]
    public ValueTask<CustomerInfo?> FetchCustomerByPhoneAsync(string? Phone)
    {
        return CustomerStore.FetchCustomerByPhoneAsync(Phone);
    }

    [MethodId("A0A481C3-6D69-4B53-943E-3F5D10EE3B94")]
    public IAsyncEnumerable<CustomerInfo> FetchCustomersAsync()
    {
        return CustomerStore.FetchCustomersAsync();
    }

    [MethodId("5DD66154-8EA8-4709-94BA-D892E56873EC")]
    public IAsyncEnumerable<SearchCustomer> SearchCustomerAsync(SearchRequest Request)
    {
        return CustomerStore.SearchCustomerAsync(Request);
    }

    [MethodId("C59A79E3-CDAD-44AF-B512-B4D58E8B1430")]
    [ErrorCode(Prefix: CustomerServicePrefix, Codes: new[] { _0001, _0002, _0003 })]
    public async ValueTask<IServiceResult<EntityId?>> InsertCustomerAsync(CreateCustomerRequest ViewModel)
    {
        var Result = await ViewModel.ValidateAsync(
            Key(nameof(CustomerStore)).Value(CustomerStore)
           .Key(nameof(GuarantorStore)).Value(GuarantorStore)
           .Key(nameof(RelationshipStore)).Value(RelationshipStore));

        if (!Result.IsValid)
            return await Result.CreateResultAsync<EntityId?>(typeof(CustomerService), "C59A79E3-CDAD-44AF-B512-B4D58E8B1430", CodeIndex0, "La solicitud de creación del cliente no es válido.");

        return await CustomerStore.InsertCustomerAsync(ViewModel)!.CreateResultAsync();
    }
}
