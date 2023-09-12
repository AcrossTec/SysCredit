namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Stores;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers.Delegates;
using SysCredit.Models;

using System.Collections.Generic;

using static Constants.ErrorCodePrefix;

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
}
