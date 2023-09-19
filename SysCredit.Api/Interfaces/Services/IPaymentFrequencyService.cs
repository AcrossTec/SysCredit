namespace SysCredit.Api.Interfaces.Services;

using SysCredit.Api.Requests.PaymentFrequencies;

using SysCredit.DataTransferObject.Commons;

using SysCredit.Helpers;
using SysCredit.Models;

public interface IPaymentFrequencyService
{
    IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync();

    ValueTask<PaymentFrequencyInfo> FetchPaymentFrequencyByIdAsync(long PaymentFrequencyId);

    ValueTask<IServiceResult<bool>> UpdatePaymentFrequencyAsync(long PaymentFrequencyId, UpdatePaymentFrequencyRequest Request);

    IAsyncEnumerable<PaymentFrequency> FetchPaymentFrequencyCompleteAsync();

    ValueTask<IServiceResult<EntityId?>> InsertPaymentFrequencyAsync(CreatePaymentFrequencyRequest ViewModel);
}