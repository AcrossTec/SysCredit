namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.DataTransferObject;
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
using static System.Reflection.MethodBase;

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

    [ErrorCode(Prefix = GuarantorServicePrefix, Codes = new[] { _0001 })]
    public async ValueTask<IServiceResult<EntityId?>> InsertGuarantorAsync(CreateGuarantorViewModel ViewModel)
    {
        var Result = await ViewModel.ValidateAsync(Key(nameof(RelationshipStore)).Value(RelationshipStore));

        if (!Result.IsValid)
        {
            return await Result.CreateResultAsync<EntityId?>(GetCurrentMethod()!, CodeIndex0, "La solicitud de creación del fiador no es válido.");
        }

        return await GuarantorStore.InsertGuarantorAsync(ViewModel)!.CreateResultAsync();
    }

    public IAsyncEnumerable<GuarantorDataTransferObject> FetchGuarantorsAsync()
    {
        return GuarantorStore.FetchGuarantorsAsync();
    }
}
