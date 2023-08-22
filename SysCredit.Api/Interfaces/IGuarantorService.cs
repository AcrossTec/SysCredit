namespace SysCredit.Api.Interfaces;

using SysCredit.Api.ViewModels;
using SysCredit.Api.ViewModels.Guarantors;

using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;

using System.Collections.Generic;

public interface IGuarantorService
{
    IAsyncEnumerable<GuarantorInfo> SearchGuarantorAsync(SearchRequest Request);

    IAsyncEnumerable<FetchGuarantor> FetchGuarantorsAsync();

    IAsyncEnumerable<FetchGuarantor> FetchGuarantorsAsync(PaginationRequest Request);

    ValueTask<IServiceResult<EntityId?>> InsertGuarantorAsync(CreateGuarantorRequest Request);
}
