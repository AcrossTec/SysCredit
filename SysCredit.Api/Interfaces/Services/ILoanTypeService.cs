namespace SysCredit.Api.Interfaces.Services;

using SysCredit.Api.Requests.LoanType;
using SysCredit.Api.Requests.LoanTypes;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Collections.Generic;

public partial interface ILoanTypeService : IService<LoanType>
{
    IAsyncEnumerable<LoanTypeInfo> FetchLoanTypeAsync();

    ValueTask<bool> DeleteLoanTypeAsync(DeleteLoanTypeRequest Request);

    ValueTask<EntityId> InsertLoanTypeAsync(CreateLoanTypeRequest ViewModel);

    IAsyncEnumerable<LoanType> FetchLoanTypeCompleteAsync();

    ValueTask<bool> UpdateLoanTypeAsync(long LoanTypeId, UpdateLoanTypeRequest Request);

    ValueTask<LoanTypeInfo?> FetchLoanTypeByIdAsync(long? LoanTypeId);
}
