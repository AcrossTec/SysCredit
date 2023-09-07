namespace SysCredit.Models;

public record class PaymentFrequency : Entity
{
    public long PaymentFrequencyId { get; set; }

    public String Name { get; set; } = String.Empty;
}
