namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.PaymentPlans;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;

using SysCredit.Models;

using System.Collections.Generic;

using static Constants.ErrorCodePrefix;

using static SysCredit.Helpers.ContextData;

[Service<IPaymentPlanService>]
[ErrorCategory(nameof(PaymentPlanService))]
[ErrorCodePrefix(PaymentPlanServicePrefix)]
public class PaymentPlanService(IStore<PaymentPlan> PaymentPlanStore, ILogger<PaymentPlanService> Logger) : IPaymentPlanService
{
    /// <summary>
    ///     Obtiene los detalles de un plan de pago por su identificador de plan de pagos de forma asíncrona.
    /// </summary>
    /// <param name="Request">La solicitud que contiene el identificador del plan de pagos.</param>
    /// <returns>Una secuencia asíncrona de detalles del plan de pago.</returns>
    [MethodId("099003F6-C449-49E4-8C86-D455C93243AE")]
    public async IAsyncEnumerable<PaymentPlanDetailsInfo> FetchPaymentPlanDetailsByPaymentPlanIdAsync(PaymentPlanIdRequest Request)
    {
        // Valida que exista un plan de pago con el identificador solicitado y lanza excepciones en caso de fallas.
        await Request.ValidateAndThrowOnFailuresAsync(Key(nameof(PaymentPlanStore)).Value(PaymentPlanStore));

        // Llama al método del store que obtiene los detalles del plan de pago por su identificador.
        return PaymentPlanStore.FetchPaymentPlanDetailsByPaymentPlanId(Request);
    }
}