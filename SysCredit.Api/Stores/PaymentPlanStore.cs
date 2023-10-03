namespace SysCredit.Api.Stores;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.PaymentPlans;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(nameof(PaymentPlanStore))]
[ErrorCodePrefix(PaymentPlanStorePrefix)]
public static class PaymentPlanStore
{
    /// <summary>
    ///     Obtiene de forma asincrónica la información de un plan de pagos por su ID.
    /// </summary>
    /// <param name="Store">Repositorio de PaymentPlan.</param>
    /// <param name="PaymentPlanId">ID del PaymentPlan.</param>
    /// <returns>Información del PaymentPlan.</returns>
    /// <exception cref="NotImplementedException">Excepción que se produce cuando no se implementa la operación.</exception>
    [MethodId("7EE5F973-B34E-4EB1-85FA-B8D7931E52E1")]
    public static async ValueTask<PaymentPlanInfo> FetchPaymentPlanByIdAsync(this IStore<PaymentPlan> Store, long? PaymentPlanId)
    {
        return await Store.ExecFirstOrDefaultAsync<PaymentPlanInfo>("[dbo].[FetchPaymentPlanById]", new { PaymentPlanId });
    }

    /// <summary>
    ///     Obtiene de forma asincrónica los detalles de un plan de pagos por su ID.
    /// </summary>
    /// <param name="Store">Repositorio de PaymentPlan.</param>
    /// <param name="PaymentPlanId">ID del PaymentPlan.</param>
    /// <returns>Colección asincrónica de detalles del PaymentPlan.</returns>
    [MethodId("85E25823-DC65-4F9B-B99B-5B7ED611BB70")]
    public static IAsyncEnumerable<PaymentPlanDetailsInfo> FetchPaymentPlanDetailsByPaymentPlanId(this IStore<PaymentPlan> Store, PaymentPlanIdRequest Request)
    {
        return Store.ExecQueryAsync<PaymentPlanDetailsInfo>("[dbo].[FetchDetailsByPaymentPlanId]", Request);
    }
}
