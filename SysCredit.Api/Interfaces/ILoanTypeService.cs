namespace SysCredit.Api.Interfaces;

using SysCredit.Api.ViewModels.LoanType;
using SysCredit.Helpers;
using SysCredit.Models;
using System.Collections.Generic;

public interface ILoanTypeService
{
    IAsyncEnumerable<LoanTypeInfo> FetchLoanTypeAsync();

    ValueTask<IServiceResult<EntityId?>> InsertLoanTypeAsync(CreateLoanTypeRequest ViewModel);

    IAsyncEnumerable<LoanType> FetchLoanTypeCompleteAsync();
}
