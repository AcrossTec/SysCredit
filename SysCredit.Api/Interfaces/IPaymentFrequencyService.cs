namespace SysCredit.Api.Interfaces;

using SysCredit.DataTransferObject.Commons;

public interface IPaymentFrequencyService
{
    IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync();
}
