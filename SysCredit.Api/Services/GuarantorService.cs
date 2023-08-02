namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.DataTransferObject.StoredProcedures;
using SysCredit.Api.Extensions;
using SysCredit.Api.Helpers;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Models;
using SysCredit.Api.Stores;
using SysCredit.Api.ViewModels.Guarantors;

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

    public IAsyncEnumerable<FetchGuarantor> FetchGuarantorsAsync()
    {
        return GuarantorStore.FetchGuarantorsAsync();
    }
}
