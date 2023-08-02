namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.DataTransferObject.StoredProcedures;
using SysCredit.Api.Extensions;
using SysCredit.Api.Helpers;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Models;
using SysCredit.Api.Stores;
using SysCredit.Api.ViewModels.Customers;

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

    public IAsyncEnumerable<FetchCustomer> FetchCustomersAsync()
    {
        return CustomerStore.FetchCustomersAsync();
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
