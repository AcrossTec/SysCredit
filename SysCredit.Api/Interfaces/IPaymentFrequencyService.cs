namespace SysCredit.Api.Interfaces;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;
using SysCredit.Helpers;

public interface IPaymentFrequencyService
{
    IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync();

    ValueTask<PaymentFrequencyInfo> FetchPaymentFrequencyByIdAsync(long PaymentFrequencyId);

    IAsyncEnumerable<PaymentFrequency> FetchPaymentFrequencyCompleteAsync();
}
