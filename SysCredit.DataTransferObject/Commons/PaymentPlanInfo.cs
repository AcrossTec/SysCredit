namespace SysCredit.DataTransferObject.Commons;

public record class PaymentPlanInfo
{
    public long PaymentPlanId { get; set; }

    public long CustomerId { get; set; }

    public long LoanId { get; set; }

    public decimal InitialBalance { get; set; }

    public IEnumerable<PaymentPlanDetailsInfo> PaymentPlanDetails { get; set; } = Array.Empty<PaymentPlanDetailsInfo>();
}