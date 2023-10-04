using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Loans;
using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;
using SysCredit.Models;

namespace SysCredit.Api.Stores;

using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(nameof(LoanTypeStore))]
[ErrorCodePrefix(LoanStorePrefix)]
public static class LoanStore
{
    /// <summary>
    ///     Obtiene información de un préstamo por su identificador de préstamo de forma asíncrona.
    /// </summary>
    /// <param name="Store">La instancia del almacén que contiene los préstamos.</param>
    /// <param name="LoanId">El identificador del préstamo.</param>
    /// <returns>La información del préstamo o nulo si no se encuentra.</returns>
    [MethodId("6B50A40D-4F92-4D51-BF8B-9E43853947A4")]
    public static async ValueTask<LoanInfo?> FetchLoanByIdAsync(this IStore<Loan> Store, long? LoanId)
    {
        return await Store.ExecFirstOrDefaultAsync<LoanInfo?>("[dbo].[FetchLoanByLoanId]", new { LoanId });
    }

    /// <summary>
    ///     Obtiene información del plan de pagos de un préstamo por su identificador de préstamo de forma asíncrona.
    /// </summary>
    /// <param name="Store">La instancia del repositorio que contiene los préstamos.</param>
    /// <param name="LoanId">El identificador del préstamo.</param>
    /// <returns>La información del plan de pagos asociado al préstamo o nulo si no se encuentra.</returns>
    [MethodId("671B924D-90D3-4FFF-B3FD-A5F5404BBE6B")]
    public static async ValueTask<PaymentPlanInfo?> FetchPaymentPlanFromLoanByLoanId(this IStore<Loan> Store, LoanIdRequest Request)
    {
        // Llama al método que obtiene la información del plan de pagos de un préstamo por su identificador de préstamo.
        return await Store.ExecQuery<FetchPaymentPlan>("[dbo].[FetchPaymentPlanFromLoanByLoanId]", Request).ConverFetchPaymentPlanToPaymentPlanInfoAsync().SingleOrDefaultAsync();
    }

    private static IAsyncEnumerable<PaymentPlanInfo> ConverFetchPaymentPlanToPaymentPlanInfoAsync(this IEnumerable<FetchPaymentPlan> FetchPaymentPlans)
    {
        var Query =
            from PaymentPlan in FetchPaymentPlans
            group PaymentPlan by PaymentPlan.PaymentPlanId into PaymentPlans
            let PaymentPlan = PaymentPlans.First()
            select new PaymentPlanInfo
            {
                PaymentPlanId = PaymentPlan.PaymentPlanId,
                CustomerId = PaymentPlan.CustomerId,
                LoanId = PaymentPlan.LoanId,
                InitialBalance = PaymentPlan.InitialBalance,

                PaymentPlanDetails = from PaymentPlanDetail in PaymentPlans
                                     group PaymentPlanDetail by PaymentPlanDetail.PaymentPlanDetailId into PaymentPlanDetails
                                     let PaymentPlanDetail = PaymentPlanDetails.First()
                                     select new PaymentPlanDetailsInfo
                                     {
                                         PaymentPlanDetailId = PaymentPlanDetail.PaymentPlanDetailId,
                                         PaymentPlanId = PaymentPlanDetail.PaymentPlanId,
                                         PaymentDate = PaymentPlanDetail.PaymentDate,
                                         PaymentValue = PaymentPlanDetail.PaymentValue,
                                         Balance = PaymentPlanDetail.PaymentPlanDetailBalance
                                     }
            };

        return Query.ToAsyncEnumerable();
    }
}