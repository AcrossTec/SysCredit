namespace SysCredit.Api.Interfaces;

using SysCredit.Api.ViewModels.PaymentFrequencys;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

public interface IPaymentFrequencyService
{
    IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync();

    ValueTask<PaymentFrequencyInfo> FetchPaymentFrequencyByIdAsync(long PaymentFrequencyId);

    IAsyncEnumerable<PaymentFrequency> FetchPaymentFrequencyCompleteAsync();

    ValueTask<IServiceResult<EntityId?>> InsertPaymentFrequencyAsync(CreatePaymentFrequencyRequest ViewModel);
}