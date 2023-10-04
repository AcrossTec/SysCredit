namespace SysCredit.DataTransferObject.Commons;

public record class PaymentPlanDetailsInfo
{
    public long PaymentPlanDetailId { get; set; }

    public long PaymentPlanId { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal PaymentValue { get; set; }

    public decimal Balance { get; set; }
}
