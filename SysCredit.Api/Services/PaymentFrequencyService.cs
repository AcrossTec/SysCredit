namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Stores;
using SysCredit.Api.ViewModels.PaymentFrequencys;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Collections.Generic;
using System.Reflection;
using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;
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
    /// Este método realiza una llamada asincrónica para obtener información
    /// de frecuencia de pago (DTO)
    /// </summary>
    /// <returns></returns>
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodId("F454CC3B-23ED-4A4E-B27E-5E6377CA3B5D")]
    public IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync()
    {
        Logger.LogInformation("[SERVICE] {Service}.{Method}()", nameof(PaymentFrequencyService), nameof(FetchPaymentFrequencyAsync));
        return PaymentFrequencyStore.FetchPaymentFrequencyAsync();
    }

    [MethodId("F2625C55-FCD9-4FEF-AAA0-3782B91A819B")]
    public async ValueTask<PaymentFrequencyInfo> FetchPaymentFrequencyByIdAsync(long PaymentFrequencyId)
    {
        Logger.LogInformation($"CALL: {nameof(PaymentFrequencyService)}.{nameof(FetchPaymentFrequencyByIdAsync)}");
        return await PaymentFrequencyStore.FetchPaymentFrequencyByIdAsync(PaymentFrequencyId);
    }

    /// <summary>
    /// Este método realiza una llamada asincrónica para obtener información
    /// de frecuencia de pago
    /// </summary>
    /// <returns></returns>
    [MethodId("6006B8FC-E3F7-43E4-9E70-6E6CA69053B0")]
    public IAsyncEnumerable<PaymentFrequency> FetchPaymentFrequencyCompleteAsync()
    {
        Logger.LogInformation($"CALL: {nameof(PaymentFrequencyService)}.{nameof(FetchPaymentFrequencyCompleteAsync)}");
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
                   ErrorCode: $"{PaymentFrequencyServicePrefix}{_0002}"// TODO : "Creaciòn de la Frecuencia de pago no vàlido"

            );
        }

        return await PaymentFrequencyStore.InsertPaymentFrequencyAsync(Request)!.CreateServiceResultAsync();
    }
}