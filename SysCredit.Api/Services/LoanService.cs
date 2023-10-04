namespace SysCredit.Api.Services;

using Microsoft.AspNetCore.Mvc;
using SysCredit.Api.Attributes;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Requests.Loans;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

using System.Threading.Tasks;

using static Constants.ErrorCodePrefix;

[Service<ILoanService>]
[ErrorCategory(nameof(LoanService))]
[ErrorCodePrefix(LoanServicePrefix)]
public class LoanService(IStore<Loan> LoanStore, ILogger<LoanService> Logger) : ILoanService
{
    /// <summary>
    ///     Obtiene información del plan de pagos de un préstamo por su identificador de préstamo de forma asíncrona.
    /// </summary>
    /// <param name="Request">La solicitud que contiene el identificador del préstamo.</param>
    /// <returns>Una tarea asincrónica que representa la operación y contiene el resultado de la solicitud.</returns>
    [MethodId("112776B3-BF85-4749-B580-DC8B7EAF2845")]
    public async ValueTask<PaymentPlanInfo?> FetchPaymentPlanFromLoanByLoanIdAsync(LoanIdRequest Request)
    {
        return await LoanStore.FetchPaymentPlanFromLoanByLoanId(Request);
    }
}