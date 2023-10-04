namespace SysCredit.Models;

public record class PaymentPlan : Entity
{
    public long PaymentPlanId { get; set; }

    public long CustomerId { get; set; }

    public long LoanId { get; set; }

    public decimal InitialBalance { get; set; }
}