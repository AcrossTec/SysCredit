using SysCredit.DataTransferObject.Commons;

namespace SysCredit.Api.Interfaces.Services;

public interface IPaymentPlanService
{
    ValueTask<PaymentPlanInfo?> FetchPaymentPlanByLoanId(long LoanId);
}