namespace SysCredit.Api.Interfaces.Services;

using SysCredit.Api.Requests.PaymentFrequencies;

using SysCredit.DataTransferObject.Commons;

using SysCredit.Helpers;
using SysCredit.Models;

/// <summary>
///     Servicio para las distintas operaciones de las frecuencias de pago del prestamo.
/// </summary>
public partial interface IPaymentFrequencyService : IService<PaymentFrequency>
{
    IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync();

    ValueTask<PaymentFrequencyInfo> FetchPaymentFrequencyByIdAsync(long PaymentFrequencyId);

    ValueTask<bool> UpdatePaymentFrequencyAsync(long PaymentFrequencyId, UpdatePaymentFrequencyRequest Request);

    ValueTask<bool> DeletePaymentFrequencyAsync(DeletePaymentFrequencyRequest Request);

    IAsyncEnumerable<PaymentFrequency> FetchPaymentFrequencyCompleteAsync();

    ValueTask<EntityId> InsertPaymentFrequencyAsync(CreatePaymentFrequencyRequest ViewModel);
}
