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
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("B4850869-6F13-4BAB-87C6-FF8F08B31A95")]
    [ErrorCode(LoanTypeServicePrefix, Codes: new[] { _0001 })]
    public async ValueTask<IServiceResult<bool>> DeleteLoanTypeAsync(DeleteLoanTypeRequest Request)
    {
        var Result = await Request.ValidateAsync(Key(nameof(LoanTypeStore)).Value(LoanTypeStore));

        if (!Result.IsValid)
            return await Result.CreateResultAsync<bool>(typeof(LoanTypeService), "B4850869-6F13-4BAB-87C6-FF8F08B31A95", CodeIndex0, "La solicitud para eliminar el Tipo de Prestamo no es valida.");

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
            return await Result.CreateResultAsync<EntityId?>(typeof(LoanTypeService), "09F1FC4B-5456-47CF-9F46-41F96683E7E1", CodeIndex0, "La creación del Tipo de Prestamo no es correcta");

        return await LoanTypeStore.InsertLoanTypeAsync(ViewModel)!.CreateResultAsync();
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
    ///     Primero Verifica si es valida la petición
    ///     Segundo Verifica si existe algun registro con el id en el request
    ///     Y por ultimo hace el update
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("84D2F863-8AB9-4232-AD62-C0BA5DF28ECE")]
    [ErrorCode(LoanTypeServicePrefix, Codes: new[] { _0003, _0004 })]
    public async ValueTask<IServiceResult<EntityId?>> UpdateLoanTypeAsync(UpdateLoanTypeRequest Request)
    {
        var Result = await Request.ValidateAsync(Key(nameof(LoanTypeStore)).Value(LoanTypeStore));

        if (!Result.IsValid)
            return await Result.CreateResultAsync<EntityId?>(typeof(LoanTypeService), "84D2F863-8AB9-4232-AD62-C0BA5DF28ECE", CodeIndex0, "La modificación del Tipo de Prestamo no es correcta");

        var IsExist = await FetchLoanTypeByIdAsync(Request.LoanTypeId);

        if (IsExist.Data!.Id == null)
            return await Result.CreateResultAsync<EntityId?>(typeof(LoanTypeService), "84D2F863-8AB9-4232-AD62-C0BA5DF28ECE", CodeIndex1, "El tipo de prestamo con el id no existe");

        return await LoanTypeStore.UpdateLoanTypeAsync(Request)!.CreateResultAsync();
    }

    [MethodId("1C28B6F0-9A95-4B51-9662-5A23820A2233")]
    public async ValueTask<IServiceResult<EntityId?>> FetchLoanTypeByIdAsync(long LoanTypeId)
    {
        return await LoanTypeStore.FetchLoanTypeByIdAsync(LoanTypeId).CreateResultAsync();
    }
}
