namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests;
using SysCredit.Api.Requests.Customers;
using SysCredit.Api.Requests.Guarantors;
using SysCredit.Api.Stores;
using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

using static Constants.ErrorCodePrefix;
using static Constants.ErrorCodes;
using static SysCredit.Helpers.ContextData;

/// <summary>
/// 
/// </summary>
/// <param name="Store"></param>
[Service<ICustomerService>]
[ErrorCategory(nameof(CustomerService))]
[ErrorCodePrefix(CustomerServicePrefix)]
public class CustomerService(IStore Store) : ICustomerService
{
    private readonly IStore<Customer> CustomerStore = Store.GetStore<Customer>();
    private readonly IStore<Guarantor> GuarantorStore = Store.GetStore<Guarantor>();
    private readonly IStore<Relationship> RelationshipStore = Store.GetStore<Relationship>();
    private readonly IStore<Reference> ReferenceStore = Store.GetStore<Reference>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CustomerId"></param>
    /// <returns></returns>
    [MethodId("62D2191D-EF87-4A97-BFF5-4F16A5B09411")]
    public ValueTask<CustomerInfo?> FetchCustomerByIdAsync(long? CustomerId)
    {
        return CustomerStore.FetchCustomerByIdAsync(CustomerId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Identification"></param>
    /// <returns></returns>
    [MethodId("7D0C770B-A53A-4E82-8752-4BECB5F844F0")]
    public ValueTask<CustomerInfo?> FetchCustomerByIdentificationAsync(string? Identification)
    {
        return CustomerStore.FetchCustomerByIdentificationAsync(Identification);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Email"></param>
    /// <returns></returns>
    [MethodId("DB423C0D-828E-4BCB-8A71-52B339C5E1C6")]
    public ValueTask<CustomerInfo?> FetchCustomerByEmailAsync(string? Email)
    {
        return CustomerStore.FetchCustomerByEmailAsync(Email);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Phone"></param>
    /// <returns></returns>
    [MethodId("0167F562-8E3E-4B3B-9039-AA4EDEF95692")]
    public ValueTask<CustomerInfo?> FetchCustomerByPhoneAsync(string? Phone)
    {
        return CustomerStore.FetchCustomerByPhoneAsync(Phone);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodId("A0A481C3-6D69-4B53-943E-3F5D10EE3B94")]
    public IAsyncEnumerable<CustomerInfo> FetchCustomerAsync()
    {
        return CustomerStore.FetchCustomerAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("5DD66154-8EA8-4709-94BA-D892E56873EC")]
    public IAsyncEnumerable<SearchCustomer> SearchCustomerAsync(SearchRequest Request)
    {
        return CustomerStore.SearchCustomerAsync(Request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("C59A79E3-CDAD-44AF-B512-B4D58E8B1430")]
    public async ValueTask<EntityId> InsertCustomerAsync(CreateCustomerRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(
             Key(nameof(CustomerStore)).Value(CustomerStore)
            .Key(nameof(GuarantorStore)).Value(GuarantorStore)
            .Key(nameof(RelationshipStore)).Value(RelationshipStore));

        return await CustomerStore.InsertCustomerAsync(Request);
    }

    /// <summary>
    ///     Obtiene todas las referencias de un cliente
    /// </summary>
    /// <param name="Request">Requiere un id</param>
    /// <returns>Retorna una lista de referencias de un cliente</returns>
    [MethodId("AE8B99A2-9CD0-4814-8741-C4329C746735")]
    public IAsyncEnumerable<ReferenceInfo> FetchReferenceByCustomerIdAsync(long? CustomerId)
    {
        return CustomerStore.FetchReferenceByCustomerIdAsync(CustomerId);
    }

    /// <summary>
    ///     Obtiene todos las fiadores de un cliente
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("B8AD9D4D-7129-46F2-95CB-7AED1073E070")]
    public IAsyncEnumerable<GuarantorInfo> FetchGuarantorByCustomerIdAsync(long? CustomerId)
    {
        return CustomerStore.FetchGuarantorByCustomerIdAsync(CustomerId);
    }

    public IAsyncEnumerable<LoanInfo> FetchLoanByCustomerIdAsync(long? CustomerId)
    {
        return CustomerStore.FetchLoanByCustomerIdAsync(CustomerId);
    }

    /// <summary>
    ///     Hace llamado al Store del CustomerStore
    /// </summary>
    /// <param name="Request">Los Ids que vienen de la URL</param>
    /// <returns></returns>
    [MethodId("83E60601-E1AA-4418-ADFF-663588AD58F7")]
    public ValueTask<GuarantorInfo?> FetchGuarantorByCustomerIdAndGuarantorIdAsync(GuarantorAndCustomerIdsRequest Request)
    {
        return CustomerStore.FetchGuarantorByCustomerIdAndGuarantorIdAsync(Request);
    }
}
