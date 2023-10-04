namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;

using SysCredit.Models;
using System.Threading.Tasks;
using static Constants.ErrorCodePrefix;

[Service<IPaymentPlanService>]
[ErrorCategory(nameof(PaymentPlanService))]
[ErrorCodePrefix(PaymentPlanServicePrefix)]
public class PaymentPlanService(IStore<PaymentPlan> PaymentPlanStore, ILogger<PaymentPlanService> Logger) : IPaymentPlanService
{
    /// <summary>
    ///     Obtiene información del plan de pagos por su identificador de préstamo de forma asíncrona.
    /// </summary>
    /// <param name="LoanId">El identificador del préstamo asociado al plan de pagos.</param>
    /// <returns>La información del plan de pagos asociado al préstamo o nulo si no se encuentra.</returns>
    public ValueTask<PaymentPlanInfo?> FetchPaymentPlanByLoanId(long LoanId)
    {
        return PaymentPlanStore.FetchPaymentPlanByLoanId(LoanId);
    }
}