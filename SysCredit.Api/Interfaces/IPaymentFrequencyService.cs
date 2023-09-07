namespace SysCredit.Api.Interfaces;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;
public interface IPaymentFrequencyService
{
    IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync();
}
