namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;


public class PaymentFrequencyService(IStore<PaymentFrequency> PaymentFrequencyStore, ILogger<PaymentFrequencyService> Logger) : IPaymentFrequencyService
{
    [MethodId("F454CC3B-23ED-4A4E-B27E-5E6377CA3B5D")]
    public IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync()
    {
        return PaymentFrequencyStore.FetchPaymentFrequencyAsync();
    }

    [MethodId("F2625C55-FCD9-4FEF-AAA0-3782B91A819B")]
    public async ValueTask<IServiceResult<PaymentFrequencyInfo?>> FetchPaymentFrequencyByIdAsync(long PaymentFrequencyId)
    {
        return await PaymentFrequencyStore.FetchPaymentFrequencyByIdAsync(PaymentFrequencyId).CreateResultAsync();
    }
}
