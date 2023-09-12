namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Stores;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

using System.Collections.Generic;

[Service<IPaymentFrequencyService>]
[ErrorCategory(nameof(PaymentFrequencyService))]
public class PaymentFrequencyService(IStore<PaymentFrequency> PaymentFrequencyStore, ILogger<PaymentFrequencyService> Logger) : IPaymentFrequencyService
{
    [MethodId("F454CC3B-23ED-4A4E-B27E-5E6377CA3B5D")]
    public IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync()
    {
        return PaymentFrequencyStore.FetchPaymentFrequencyAsync();
    }
}
