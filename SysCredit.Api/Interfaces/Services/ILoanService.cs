namespace SysCredit.Api.Interfaces.Services;

using SysCredit.Api.Requests.Loan;
using SysCredit.DataTransferObject.Commons;

public interface ILoanService
{
    ValueTask<PaymentPlanInfo?> FetchFromLoanThePaymentPlanByIdAndLoanId(LoandIdWithPaymentPlanIdRequest Request);

    ValueTask<PaymentPlanDetailsInfo?> FetchFromLoanThePaymentPlanDetailsByPaymentPlanIdAndLoanId(LoandIdWithPaymentPlanIdRequest Request);
}