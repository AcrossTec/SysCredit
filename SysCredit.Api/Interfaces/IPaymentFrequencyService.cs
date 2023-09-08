namespace SysCredit.Api.Interfaces;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;

public interface IPaymentFrequencyService
{
    IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync();

    ValueTask<IServiceResult<PaymentFrequencyInfo?>> FetchPaymentFrequencyByIdAsync(long PaymentFrequencyId);
}
