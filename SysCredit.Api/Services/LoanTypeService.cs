﻿namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Stores;
using SysCredit.Api.ViewModels.LoanType;

using SysCredit.Helpers;

using SysCredit.Models;

using System.Collections.Generic;

using static Constants.ErrorCodeIndex;
using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;

using static SysCredit.Helpers.ContextData;

[Service<ILoanTypeService>]
[ErrorCategory(ErrorCategories.LoanTypeService)]
public class LoanTypeService : ILoanTypeService
{
    private readonly IStore<LoanType> LoanTypeStore;

    public LoanTypeService(IStore<LoanType> LoanTypeStore)
    {
        this.LoanTypeStore = LoanTypeStore;
    }

    [MethodId("F0D2105D-BDEB-4CC9-BD61-3CA6DF382C03")]
    public IAsyncEnumerable<LoanTypeInfo> FetchLoanTypeAsync()
    {
        return LoanTypeStore.FetchLoanTypeAsync();
    }

    [MethodId("09f1fc4b-5456-47cf-9f46-41f96683e7e1")]
    [ErrorCode(Prefix: LoanTypeServicePrefix, Codes: new[] { _0001, _0002, _0003 })]
    public async ValueTask<IServiceResult<EntityId?>> InsertLoanTypeAsync(CreateLoanTypeRequest ViewModel)
    {
        var Result = await ViewModel.ValidateAsync(Key(nameof(LoanTypeStore)).Value(LoanTypeStore));

        if (!Result.IsValid)
            return await Result.CreateResultAsync<EntityId?>(typeof(LoanTypeService), "09f1fc4b-5456-47cf-9f46-41f96683e7e1", CodeIndex0, "La creación del Tipo de Prestamo no es correcta");

        return await LoanTypeStore.InsertLoanTypeAsync(ViewModel)!.CreateResultAsync();
    }

    [MethodId("702f277c-9b52-4cd2-84e2-85b9b8352e36")]
    public IAsyncEnumerable<LoanType> FetchLoanTypeCompleteAsync()
    {
        return LoanTypeStore.FetchLoanTypeCompleteAsync();
    }
}