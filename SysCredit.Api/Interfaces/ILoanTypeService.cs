﻿namespace SysCredit.Api.Interfaces;

using SysCredit.Api.Stores;
using SysCredit.Api.ViewModels.LoanType;
using SysCredit.Api.ViewModels.LoanTypes;
using SysCredit.Helpers;
using SysCredit.Models;
using System.Collections.Generic;

public interface ILoanTypeService
{
    IAsyncEnumerable<LoanTypeInfo> FetchLoanTypeAsync();

    ValueTask<IServiceResult<bool>> DeleteLoanTypeAsync(DeleteLoanTypeRequest Request);

    ValueTask<IServiceResult<EntityId?>> InsertLoanTypeAsync(CreateLoanTypeRequest ViewModel);

    IAsyncEnumerable<LoanType> FetchLoanTypeCompleteAsync();

    ValueTask<IServiceResult<EntityId?>> UpdateLoanTypeAsync(UpdateLoanTypeRequest Request);

    ValueTask<IServiceResult<EntityId?>> FetchLoanTypeByIdAsync(long LoanTypeId);
}
