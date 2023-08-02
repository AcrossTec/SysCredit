namespace SysCredit.Api.Interfaces;

using SysCredit.Api.DataTransferObject.StoredProcedures;
using SysCredit.Api.Helpers;
using SysCredit.Api.ViewModels.Guarantors;

public interface IGuarantorService
{
    IAsyncEnumerable<FetchGuarantor> FetchGuarantorsAsync();

    ValueTask<IServiceResult<EntityId?>> InsertGuarantorAsync(CreateGuarantorRequest Request);
}
