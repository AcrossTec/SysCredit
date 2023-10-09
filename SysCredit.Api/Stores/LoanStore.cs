namespace SysCredit.Api.Stores;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Loan;
using SysCredit.DataTransferObject.Commons;
using SysCredit.DataTransferObject.StoredProcedures;
using SysCredit.Models;
using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(nameof(LoanTypeStore))]
[ErrorCodePrefix(LoanStorePrefix)]
public static class LoanStore
{
    /// <summary>
    ///     Obtiene un plan de pagos a partir de un ID de préstamo y un ID de plan de pagos dados.
    /// </summary>
    /// <param name="Store">Instancia del almacén que contiene los préstamos.</param>
    /// <param name="Request">Solicitud que contiene el ID de préstamo y el ID de plan de pagos.</param>
    /// <returns>Información del plan de pagos o nulo si no se encuentra.</returns>
    [MethodId("FFF934E0-8D38-4646-B384-42F65515617F")]
    public static async ValueTask<PaymentPlanInfo?> FetchFromLoanThePaymentPlanByIdAndLoanId(this IStore<Loan> Store, LoandIdWithPaymentPlanIdRequest Request)
    {
        return await Store.ExecuteStoredProcedureQuery<FetchPaymentPlan>("[dbo].[FetchPaymentPlanByIdAndLoanId]", Request).ConverFetchPaymentPlanToPaymentPlanInfoAsync().SingleOrDefaultAsync();
    }

    /// <summary>
    ///     Convierte un conjunto de FetchPaymentPlan a PaymentPlanInfo de forma asíncrona.
    /// </summary>
    /// <param name="FetchPaymentPlans">Conjunto de FetchPaymentPlan a convertir.</param>
    /// <returns>Información del plan de pagos.</returns>
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

    /// <summary>
    ///     Obtiene los detalles de un plan de pagos a partir de un ID de préstamo y un ID de plan de pagos dados.
    /// </summary>
    /// <param name="Store">Instancia del almacén que contiene los préstamos.</param>
    /// <param name="Request">Solicitud que contiene el ID de préstamo y el ID de plan de pagos.</param>
    /// <returns>Detalles del plan de pagos o nulo si no se encuentra.</returns>
    [MethodId("FFF934E0-8D38-4646-B384-42F65515617F")]
    public static async ValueTask<PaymentPlanDetailsInfo?> FetchFromLoanThePaymentPlanDetailsByPaymentPlanIdAndLoanId(this IStore<Loan> Store, LoandIdWithPaymentPlanIdRequest Request)
    {
        return await Store.ExecuteStoredProcedureQueryFirstOrDefaultValueAsync<PaymentPlanDetailsInfo?>("[dbo].[FetchPaymentPlanByIdAndLoanId]", Request);
    }
}
