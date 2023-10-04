namespace SysCredit.Api.Stores;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(nameof(PaymentPlanStore))]
[ErrorCodePrefix(PaymentPlanStorePrefix)]
public static class PaymentPlanStore
{
    /// <summary>
    ///     Obtiene información del plan de pagos por su identificador de préstamo de forma asíncrona.
    /// </summary>
    /// <param name="Store">Repositorio de datos para acceder a la información.</param>
    /// <param name="LoanId">El identificador del préstamo asociado al plan de pagos.</param>
    /// <returns>La información del plan de pagos asociado al préstamo o nulo si no se encuentra.</returns>
    [MethodId("688EFB0F-526C-4FF3-B30D-15E616B0B8AD")]
    public static async ValueTask<PaymentPlanInfo?> FetchPaymentPlanByLoanId(this IStore<PaymentPlan> Store, long LoanId)
    {
        // Llama al método que obtiene la información del plan de pagos por su identificador de préstamo.
        return await Store.ExecFirstOrDefaultAsync<PaymentPlanInfo?>("[dbo].[FetchPaymentPlanByLoanId]", new { LoanId });
    }
}