namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
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

using static Constants.ErrorCodeIndex;
using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;
using static SysCredit.Helpers.ContextData;

[Service<IGuarantorService>]
[ErrorCategory(ErrorCategories.GuarantorService)]
public class GuarantorService : IGuarantorService
{
    private readonly IStore<Guarantor> GuarantorStore;
    private readonly IStore<Relationship> RelationshipStore;

    public GuarantorService(IStore Store)
    {
        GuarantorStore = Store.GetStore<Guarantor>();
        RelationshipStore = Store.GetStore<Relationship>();
    }

    [MethodId("9FE9602F-7011-435F-83BE-F573704A932D")]
    [ErrorCode(GuarantorServicePrefix, Codes: new[] { _0001 })]
    public async ValueTask<IServiceResult<EntityId?>> InsertGuarantorAsync(CreateGuarantorRequest ViewModel)
    {
        var Result = await ViewModel.ValidateAsync(Key(nameof(RelationshipStore)).Value(RelationshipStore).Key(nameof(GuarantorStore)).Value(GuarantorStore));

        if (!Result.IsValid)
            return await Result.CreateResultAsync<EntityId?>(typeof(GuarantorService), "9FE9602F-7011-435F-83BE-F573704A932D", CodeIndex0, "La solicitud de creación del fiador no es válido.");

        return await GuarantorStore.InsertGuarantorAsync(ViewModel)!.CreateResultAsync();
    }

    [MethodId("F156EE14-0CB8-477E-B9BF-0B864E26BF25")]
    public IAsyncEnumerable<FetchGuarantor> FetchGuarantorsAsync()
    {
        return GuarantorStore.FetchGuarantorsAsync();
    }

    [MethodId("543DDE99-6927-4D4D-928F-A47CD6695114")]
    public IAsyncEnumerable<GuarantorInfo> SearchGuarantorAsync(SearchRequest Request)
    {
        return GuarantorStore.SearchGuarantorAsync(Request);
    }
}
