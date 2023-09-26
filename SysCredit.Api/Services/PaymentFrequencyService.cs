namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.LoanTypes;
using SysCredit.Api.Requests.PaymentFrequencies;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Collections.Generic;
using System.Reflection;

using static Constants.ErrorCodePrefix;
using static Constants.ErrorCodes;

using static SysCredit.Helpers.ContextData;

/// <summary>
/// 
/// </summary>
/// <param name="PaymentFrequencyStore"></param>
/// <param name="Logger"></param>
[Service<IPaymentFrequencyService>]
[ErrorCategory(nameof(PaymentFrequencyService))]
[ErrorCodePrefix(PaymentFrequencyServicePrefix)]
public class PaymentFrequencyService(IStore<PaymentFrequency> PaymentFrequencyStore, ILogger<PaymentFrequencyService> Logger) : IPaymentFrequencyService
{
    /// <summary>
    ///     Este método realiza una llamada asincrónica para obtener información de frecuencia de pago (DTO)
    /// </summary>
    /// <returns></returns>
    [MethodId("F454CC3B-23ED-4A4E-B27E-5E6377CA3B5D")]
    public IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync()
    {
        return PaymentFrequencyStore.FetchPaymentFrequencyAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="PaymentFrequencyId"></param>
    /// <returns></returns>
    [MethodId("F2625C55-FCD9-4FEF-AAA0-3782B91A819B")]
    public async ValueTask<PaymentFrequencyInfo> FetchPaymentFrequencyByIdAsync(long PaymentFrequencyId)
    {
        return await PaymentFrequencyStore.FetchPaymentFrequencyByIdAsync(PaymentFrequencyId);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="PaymentFrequencyId"></param>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("54EA25C1-FD73-4FC3-8984-DEA6ACFD74C7")]
    public async ValueTask<IServiceResult<bool>> UpdatePaymentFrequencyAsync(long PaymentFrequencyId, UpdatePaymentFrequencyRequest Request)
    {
        var Result = await Request.ValidateAsync(
            Key(nameof(PaymentFrequencyStore)).Value(PaymentFrequencyStore)
           .Key("RoutePaymentFrequencyId").Value(PaymentFrequencyId));

        if (Result.HasError())
        {
            return await Result.CreateServiceResultAsync<bool>
            (
                MethodInfo: MethodInfo.GetCurrentMethod(),
                ErrorCode: SERVPF0003
            );
        }

        return await PaymentFrequencyStore.UpdatePaymentFrequencyAsync(Request).CreateServiceResultAsync();
    }

    /// <summary>
    ///     Consulta al PaymentFrequencyStore por los datos 
    ///     y realizar el borrado lógico
    /// </summary>
    /// <param name="Request"></param>
    /// <returns></returns>
    [MethodId("FDB8109F-DF96-48B7-8B60-F233F2A8098F")]
    public async ValueTask<IServiceResult<bool>> DeletePaymentFrequencyAsync(DeletePaymentFrequencyRequest Request)
    {
        var Result = await Request.ValidateAsync(Key(nameof(PaymentFrequencyStore)).Value(PaymentFrequencyStore));

        if (Result.HasError())
        {
            return await Result.CreateServiceResultAsync<bool>
            (
                MethodInfo: MethodInfo.GetCurrentMethod(),
                 ErrorCode: SERVPF0501 // TODO: "Solicitud para eliminar la frecuencia de pago no válido"
            );
        }

        return await PaymentFrequencyStore.DeletePaymentFrequencyAsync(Request).CreateServiceResultAsync();
    }

    /// <summary>
    /// Este método realiza una llamada asincrónica para obtener información
    /// de frecuencia de pago
    /// </summary>
    /// <returns></returns>
    [MethodId("6006B8FC-E3F7-43E4-9E70-6E6CA69053B0")]
    public IAsyncEnumerable<PaymentFrequency> FetchPaymentFrequencyCompleteAsync()
    {
        return PaymentFrequencyStore.FetchPaymentFrequencyCompleteAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ViewModel"></param>
    /// <returns></returns>
    [MethodId("BC663C2B-ACE2-499B-B806-2A0BD8D77815")]
    public async ValueTask<IServiceResult<EntityId?>> InsertPaymentFrequencyAsync(CreatePaymentFrequencyRequest Request)
    {
        Logger.LogInformation("[SERVICE] {Service}.{Method}(Request: {Request})",
           nameof(LoanTypeService), nameof(InsertPaymentFrequencyAsync),
           Newtonsoft.Json.JsonConvert.SerializeObject(Request));

        var Result = await Request.ValidateAsync(Key(nameof(PaymentFrequencyStore)).Value(PaymentFrequencyStore));

        if (Result.HasError())
        {
            return await Result.CreateServiceResultAsync<EntityId?>
            (
                  MethodInfo: MethodInfo.GetCurrentMethod(),
                   ErrorCode: SERVPF0002 // TODO: "Creaciòn de la Frecuencia de pago no válido"
            );
        }

        return await PaymentFrequencyStore.InsertPaymentFrequencyAsync(Request).CreateServiceResultAsync();
    }
}
