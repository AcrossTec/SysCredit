namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.LoanType;
using SysCredit.Api.Requests.LoanTypes;
using SysCredit.Api.Stores;

using SysCredit.Helpers;
using SysCredit.Models;

using System.Collections.Generic;
using System.Reflection;

using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;
using static SysCredit.Helpers.ContextData;

[Service<ILoanTypeService>]
[ErrorCategory(nameof(LoanTypeService))]
[ErrorCodePrefix(LoanTypeServicePrefix)]
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
    public async ValueTask<IServiceResult<bool>> DeleteLoanTypeAsync(DeleteLoanTypeRequest Request)
    {
        var Result = await Request.ValidateAsync(Key(nameof(LoanTypeStore)).Value(LoanTypeStore));

        if (Result.HasError())
        {
            return await Result.CreateServiceResultAsync<bool>
            (
                MethodInfo: MethodInfo.GetCurrentMethod(),
                 ErrorCode: $"{LoanTypeServicePrefix}{_0001}" // TODO: "Solicitud para eliminar el Tipo de Prestamo no válido"
            );
        }

        return await LoanTypeStore.DeleteLoanTypeAsync(Request).CreateServiceResultAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("09F1FC4B-5456-47CF-9F46-41F96683E7E1")]
    public async ValueTask<IServiceResult<EntityId?>> InsertLoanTypeAsync(CreateLoanTypeRequest Request)
    {
        Logger.LogInformation("[SERVICE] {Service}.{Method}(Request: {Request})",
            nameof(LoanTypeService), nameof(InsertLoanTypeAsync),
            Newtonsoft.Json.JsonConvert.SerializeObject(Request));

        var Result = await Request.ValidateAsync(Key(nameof(LoanTypeStore)).Value(LoanTypeStore));

        if (Result.HasError())
        {
            return await Result.CreateServiceResultAsync<EntityId?>
            (
                MethodInfo: MethodInfo.GetCurrentMethod(),
                 ErrorCode: $"{LoanTypeServicePrefix}{_0002}" // TODO: "Creación del Tipo de Prestamo no válido"
            );
        }

        return await LoanTypeStore.InsertLoanTypeAsync(Request).CreateServiceResultAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="LoanTypeId"></param>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("11531707-8C0B-45FC-B9F1-4418897AC8A7")]
    public async ValueTask<IServiceResult<bool>> UpdateLoanTypeAsync(long LoanTypeId, UpdateLoanTypeRequest Request)
    {
        var Result = await Request.ValidateAsync(Key(nameof(LoanTypeStore)).Value(LoanTypeStore));

        if (Result.HasError())
        {
            return await Result.CreateServiceResultAsync<bool>
            (
                MethodInfo: MethodInfo.GetCurrentMethod(),
                 ErrorCode: $"{LoanTypeServicePrefix}{_0003}" // TODO: "Modificación del Tipo de Prestamo no válido"
            );
        }

        return await LoanTypeStore.UpdateLoanTypeAsync(Request).CreateServiceResultAsync();
    }
}
