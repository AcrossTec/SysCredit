namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Stores;
using SysCredit.Api.ViewModels;
using SysCredit.Api.ViewModels.Guarantors;

using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;
using static SysCredit.Helpers.ContextData;

[Service<IGuarantorService>]
[ErrorCategory(nameof(GuarantorService))]
[ErrorCodePrefix(GuarantorServicePrefix)]
public class GuarantorService(IStore Store, ILogger<GuarantorService> Logger) : IGuarantorService
{
    private readonly IStore<Guarantor> GuarantorStore = Store.GetStore<Guarantor>();
    private readonly IStore<Relationship> RelationshipStore = Store.GetStore<Relationship>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ViewModel"></param>
    /// <returns></returns>
    [MethodId("9FE9602F-7011-435F-83BE-F573704A932D")]
    public async ValueTask<IServiceResult<EntityId?>> InsertGuarantorAsync(CreateGuarantorRequest ViewModel)
    {
        Logger.LogInformation($"CALL: {nameof(GuarantorService)}.{nameof(InsertGuarantorAsync)}");

        var Result = await ViewModel.ValidateAsync(
            Key(nameof(RelationshipStore)).Value(RelationshipStore)
           .Key(nameof(GuarantorStore)).Value(GuarantorStore));

        if (Result.HasError())
        {
            return await Result.CreateServiceResultAsync<EntityId?>
            (
                CategoryType: typeof(GuarantorService),
                    MethodId: "9FE9602F-7011-435F-83BE-F573704A932D",
                   ErrorCode: $"{GuarantorServicePrefix}{_0001}",
                ErrorMessage: "La solicitud de creación del fiador no es válido."
            );
        }

        return await GuarantorStore.InsertGuarantorAsync(ViewModel)!.CreateServiceResultAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodId("F156EE14-0CB8-477E-B9BF-0B864E26BF25")]
    public IAsyncEnumerable<FetchGuarantor> FetchGuarantorsAsync()
    {
        return GuarantorStore.FetchGuarantorsAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("4007331B-2C71-4DAF-8B00-15CBB3B3328C")]
    public IAsyncEnumerable<FetchGuarantor> FetchGuarantorsAsync(PaginationRequest Request)
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
