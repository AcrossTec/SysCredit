namespace SysCredit.Api.Services;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests;
using SysCredit.Api.Requests.Guarantors;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

using static Constants.ErrorCodePrefix;
using static SysCredit.Helpers.ContextData;

[Service<IGuarantorService>]
[ServiceModel<Guarantor>]
[ErrorCategory(nameof(GuarantorService))]
[ErrorCodePrefix(GuarantorServicePrefix)]
public partial class GuarantorService(IStore Store)
{
    private readonly IStore<Customer> CustomerStore = Store.GetStore<Customer>();
    private readonly IStore<Guarantor> GuarantorStore = Store.GetStore<Guarantor>();
    private readonly IStore<Relationship> RelationshipStore = Store.GetStore<Relationship>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("9FE9602F-7011-435F-83BE-F573704A932D")]
    public async ValueTask<EntityId> InsertGuarantorAsync(CreateGuarantorRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(
            Key(nameof(RelationshipStore)).Value(RelationshipStore)
           .Key(nameof(GuarantorStore)).Value(GuarantorStore));

        return await GuarantorStore.InsertGuarantorAsync(Request);
    }

    [MethodId("6C5CFD99-1940-487F-8637-88B6033A6216")]
    public async ValueTask<bool> DeleteGuarantorByIdAsync(DeleteGuarantorRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(Key(nameof(CustomerStore)).Value(CustomerStore));
        return await GuarantorStore.DeleteGuarantorByIdAsync(Request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodId("F156EE14-0CB8-477E-B9BF-0B864E26BF25")]
    public IAsyncEnumerable<FetchGuarantor> FetchGuarantorAsync()
    {
        return GuarantorStore.FetchGuarantorsAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="GuarantorId"></param>
    /// <returns></returns>
    [MethodId("E1C42C3D-681C-4AEA-A412-68D02482DC6D")]
    [ProducesResponseType(typeof(IResponse), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(IResponse<GuarantorInfo?>), StatusCodes.Status200OK)]
    public ValueTask<GuarantorInfo?> FetchGuarantorByIdAsync(long? GuarantorId)
    {

        return GuarantorStore.FetchGuarantorByIdAsync(GuarantorId);

    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("4007331B-2C71-4DAF-8B00-15CBB3B3328C")]
    public IAsyncEnumerable<FetchGuarantor> FetchGuarantorAsync(PaginationRequest Request)
    {
        return GuarantorStore.FetchGuarantorsAsync(Request);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("543DDE99-6927-4D4D-928F-A47CD6695114")]
    public IAsyncEnumerable<GuarantorInfo> SearchGuarantorAsync(SearchRequest Request)
    {
        return GuarantorStore.SearchGuarantorAsync(Request);
    }
}
