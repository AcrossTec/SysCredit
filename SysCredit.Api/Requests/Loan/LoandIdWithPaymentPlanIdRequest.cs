namespace SysCredit.Api.Requests.Loan;

public class LoandIdWithPaymentPlanIdRequest : IRequest
{
    public long PaymentPlanId { get; set; }

    public long LoanId { get; set; }
}
