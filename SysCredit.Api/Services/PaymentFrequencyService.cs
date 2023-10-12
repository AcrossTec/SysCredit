namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.PaymentFrequencies;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Helpers;
using SysCredit.Models;

using System.Collections.Generic;

using static Constants.ErrorCodePrefix;

using static SysCredit.Helpers.ContextData;

/// <summary>
///     Realiza distintas operaciones sobre <see cref="PaymentFrequency"/> como: Crear, Borrar, Buscar, Editar, etc
/// </summary>
/// <param name="PaymentFrequencyStore">
///     Tienda de datos para <see cref="PaymentFrequency"/>
/// </param>
/// <param name="Logger"></param>
[Service<IPaymentFrequencyService>]
[ServiceModel<PaymentFrequency>]
[ErrorCategory(nameof(PaymentFrequencyService))]
[ErrorCodePrefix(PaymentFrequencyServicePrefix)]
public partial class PaymentFrequencyService(IStore<PaymentFrequency> PaymentFrequencyStore)
{
    /// <summary>
    ///     Obtiene todos los <see cref="PaymentFrequency"/>
    /// </summary>
    /// <returns>
    ///     Regresa todos los <see cref="PaymentFrequency"/>
    /// </returns>
    [MethodId("F454CC3B-23ED-4A4E-B27E-5E6377CA3B5D")]
    public IAsyncEnumerable<PaymentFrequencyInfo> FetchPaymentFrequencyAsync()
    {
        return PaymentFrequencyStore.FetchPaymentFrequencyAsync();
    }

    /// <summary>
    ///     Obtiene un registro de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <param name="PaymentFrequencyId">
    ///     Id obtenido de la ruta
    /// </param>
    /// <returns>
    ///     Regresa un registro de la tabla <see cref="Models.PaymentFrequency"/>
    /// </returns>
    [MethodId("F2625C55-FCD9-4FEF-AAA0-3782B91A819B")]
    public async ValueTask<PaymentFrequencyInfo> FetchPaymentFrequencyByIdAsync(long PaymentFrequencyId)
    {
        return await PaymentFrequencyStore.FetchPaymentFrequencyByIdAsync(PaymentFrequencyId);
    }

    /// <summary>
    ///     Validad e invoca al Store para modificar la frequencia de pago
    /// </summary>
    /// <param name="PaymentFrequencyId">
    ///     Id recibido de la ruta
    /// </param>
    /// <param name="Request">
    ///     Datos que se van a actualizar del <see cref="Models.PaymentFrequency"/>
    /// </param>
    /// <returns>
    ///     Retorna un bool
    /// </returns>
    [MethodId("54EA25C1-FD73-4FC3-8984-DEA6ACFD74C7")]
    public async ValueTask<bool> UpdatePaymentFrequencyAsync(long PaymentFrequencyId, UpdatePaymentFrequencyRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(
            Key(nameof(PaymentFrequencyStore)).Value(PaymentFrequencyStore)
           .Key("RoutePaymentFrequencyId").Value(PaymentFrequencyId));

        return await PaymentFrequencyStore.UpdatePaymentFrequencyAsync(Request);
    }

    /// <summary>
    ///     Valida e invoca al Store para eliminar la frecuencia de pago
    /// </summary>
    /// <param name="Request">
    ///      Id del <see cref="Models.PaymentFrequency"/> a eliminar 
    /// </param>
    /// <returns>
    ///     Retorna un bool
    /// </returns>
    [MethodId("FDB8109F-DF96-48B7-8B60-F233F2A8098F")]
    public async ValueTask<bool> DeletePaymentFrequencyAsync(DeletePaymentFrequencyRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(Key(nameof(PaymentFrequencyStore)).Value(PaymentFrequencyStore));
        return await PaymentFrequencyStore.DeletePaymentFrequencyAsync(Request);
    }

    /// <summary>
    ///      Obtiene todos los registros completos de la tabla <see cref="Models.PaymentFrequency"/>
    /// </summary>
    /// <returns>
    ///      Regresa todos los registros completos de la tabla <see cref="Models.PaymentFrequency"/>
    /// </returns>
    [MethodId("6006B8FC-E3F7-43E4-9E70-6E6CA69053B0")]
    public IAsyncEnumerable<PaymentFrequency> FetchPaymentFrequencyCompleteAsync()
    {
        return PaymentFrequencyStore.FetchPaymentFrequencyCompleteAsync();
    }

    /// <summary>
    ///     Valida y crea una nueva frecuencia de pago en la base de datos
    /// </summary>
    /// <param name="Request">
    ///     Datos usado para crear la frecuencia de pago
    /// </param>
    /// <returns>
    ///     Regresa el nuevo Id de la frecuencia de pago creado
    /// </returns>
    [MethodId("BC663C2B-ACE2-499B-B806-2A0BD8D77815")]
    public async ValueTask<EntityId> InsertPaymentFrequencyAsync(CreatePaymentFrequencyRequest Request)
    {
        await Request.ValidateAndThrowOnFailuresAsync(Key(nameof(PaymentFrequencyStore)).Value(PaymentFrequencyStore));
        return await PaymentFrequencyStore.InsertPaymentFrequencyAsync(Request);
    }
}
