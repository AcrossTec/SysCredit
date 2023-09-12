namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Stores;
using SysCredit.Api.ViewModels.LoanType;
using SysCredit.Api.ViewModels.LoanTypes;
using SysCredit.Helpers;

using SysCredit.Models;

using System.Collections.Generic;

using static Constants.ErrorCodeIndex;
using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;
using static SysCredit.Helpers.ContextData;

[Service<ILoanTypeService>]
[ErrorCategory(ErrorCategories.LoanTypeService)]
public class LoanTypeService(IStore<LoanType> LoanTypeStore, ILogger<LoanTypeService> Logger) : ILoanTypeService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodId("F0D2105D-BDEB-4CC9-BD61-3CA6DF382C03")]
    public IAsyncEnumerable<LoanTypeInfo> FetchLoanTypeAsync()
    {
        return LoanTypeStore.FetchLoanTypeAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodId("702F277C-9B52-4CD2-84E2-85B9B8352E36")]
    public IAsyncEnumerable<LoanType> FetchLoanTypeCompleteAsync()
    {
        return LoanTypeStore.FetchLoanTypeCompleteAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="LoanTypeId"></param>
    /// <returns></returns>
    [MethodId("53143E7A-CB8B-45ED-AE3A-F7DD55AD907E")]
    public ValueTask<LoanTypeInfo?> FetchLoanTypeByIdAsync(long? LoanTypeId)
    {
        return LoanTypeStore.FetchLoanTypeByIdAsync(LoanTypeId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("B4850869-6F13-4BAB-87C6-FF8F08B31A95")]
    [ErrorCode(LoanTypeServicePrefix, Codes: new[] { _0001 })]
    public async ValueTask<IServiceResult<bool>> DeleteLoanTypeAsync(DeleteLoanTypeRequest Request)
    {
        var Result = await Request.ValidateAsync(Key(nameof(LoanTypeStore)).Value(LoanTypeStore));

        if (!Result.IsValid)
        {
            return await Result.CreateResultAsync<bool>(
                CategoryType: typeof(LoanTypeService),
                    MethodId: "B4850869-6F13-4BAB-87C6-FF8F08B31A95",
                   CodeIndex: CodeIndex0,
                ErrorMessage: "La solicitud para eliminar el Tipo de Prestamo no es valida.");
        }

        return await LoanTypeStore.DeleteLoanTypeAsync(Request)!.CreateResultAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ViewModel"></param>
    /// <returns></returns>
    [MethodId("09F1FC4B-5456-47CF-9F46-41F96683E7E1")]
    [ErrorCode(Prefix: LoanTypeServicePrefix, Codes: new[] { _0002 })]
    public async ValueTask<IServiceResult<EntityId?>> InsertLoanTypeAsync(CreateLoanTypeRequest ViewModel)
    {
        var Result = await ViewModel.ValidateAsync(Key(nameof(LoanTypeStore)).Value(LoanTypeStore));

        if (!Result.IsValid)
        {
            return await Result.CreateResultAsync<EntityId?>
            (
                CategoryType: typeof(LoanTypeService),
                    MethodId: "09F1FC4B-5456-47CF-9F46-41F96683E7E1",
                   CodeIndex: CodeIndex0,
                ErrorMessage: "La creación del Tipo de Prestamo no es correcta"
            );
        }

        return await LoanTypeStore.InsertLoanTypeAsync(ViewModel)!.CreateResultAsync();
    }

    [MethodId("11531707-8C0B-45FC-B9F1-4418897AC8A7")]
    [ErrorCode(Prefix: LoanTypeServicePrefix, Codes: new[] { _0002 })]
    public async ValueTask<IServiceResult<EntityId?>> UpdateLoanTypeAsync(UpdateLoanTypeRequest Request)
    {
        var Result = await Request.ValidateAsync(Key(nameof(LoanTypeStore)).Value(LoanTypeStore));

        if (!Result.IsValid)
        {
            return await Result.CreateResultAsync<EntityId?>
            (
                CategoryType: typeof(LoanTypeService),
                    MethodId: "11531707-8C0B-45FC-B9F1-4418897AC8A7",
                   CodeIndex: CodeIndex0,
                ErrorMessage: "La modificación del Tipo de Prestamo no es correcta"
            );
        }

        return await LoanTypeStore.UpdateLoanTypeAsync(Request)!.CreateResultAsync();
    }
}
