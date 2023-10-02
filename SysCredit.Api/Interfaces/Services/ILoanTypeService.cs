namespace SysCredit.Api.Interfaces.Services;

using SysCredit.Api.Requests.LoanType;
using SysCredit.Api.Requests.LoanTypes;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Collections.Generic;

public interface ILoanTypeService
{
    IAsyncEnumerable<LoanTypeInfo> FetchLoanTypeAsync();

    ValueTask<IServiceResult<bool>> DeleteLoanTypeAsync(DeleteLoanTypeRequest Request);

    ValueTask<IServiceResult<EntityId>> InsertLoanTypeAsync(CreateLoanTypeRequest ViewModel);

    IAsyncEnumerable<LoanType> FetchLoanTypeCompleteAsync();

    ValueTask<IServiceResult<bool>> UpdateLoanTypeAsync(long LoanTypeId, UpdateLoanTypeRequest Request);

    ValueTask<LoanTypeInfo?> FetchLoanTypeByIdAsync(long? LoanTypeId);
}
