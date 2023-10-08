namespace SysCredit.Api.Interfaces.Services;

using SysCredit.Api.Requests;
using SysCredit.Api.Requests.Guarantors;

using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Collections.Generic;

public partial interface IGuarantorService : IService<Guarantor>
{
    IAsyncEnumerable<FetchGuarantor> FetchGuarantorAsync();

    IAsyncEnumerable<GuarantorInfo> SearchGuarantorAsync(SearchRequest Request);

    IAsyncEnumerable<FetchGuarantor> FetchGuarantorAsync(PaginationRequest Request);

    ValueTask<EntityId> InsertGuarantorAsync(CreateGuarantorRequest Request);
}
