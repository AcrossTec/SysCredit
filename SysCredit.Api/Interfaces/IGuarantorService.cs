namespace SysCredit.Api.Interfaces;

using SysCredit.Api.ViewModels;
using SysCredit.Api.ViewModels.Guarantors;

using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;

public interface IGuarantorService
{
    IAsyncEnumerable<GuarantorInfo> SearchGuarantorAsync(SearchRequest Request);

    IAsyncEnumerable<FetchGuarantor> FetchGuarantorsAsync();

    ValueTask<IServiceResult<EntityId?>> InsertGuarantorAsync(CreateGuarantorRequest Request);
}
