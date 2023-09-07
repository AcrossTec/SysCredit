namespace SysCredit.DataTransferObject.Commons;

public record class PaymentFrequencyInfo : IDataTransferObject
{
    public long PaymentFrequencyId { get; set; }

    public String Name { get; set; } = String.Empty;
}
