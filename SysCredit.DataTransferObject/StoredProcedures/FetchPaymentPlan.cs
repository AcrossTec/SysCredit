namespace SysCredit.DataTransferObject.StoredProcedures;

public record class FetchPaymentPlan
{
    public long PaymentPlanId { get; set; }

    public long CustomerId {  get; set; }

    public long LoanId { get; set; }

    public decimal InitialBalance { get; set; }

    // PaymentPlanDetails
    public long PaymentPlanDetailId { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal PaymentValue { get; set; }

    public decimal PaymentPlanDetailBalance { get; set; }
}
