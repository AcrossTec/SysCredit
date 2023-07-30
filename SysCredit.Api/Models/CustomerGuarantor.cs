namespace SysCredit.Api.Models;

public record class CustomerGuarantor : Entity
{
    public long CustomerGuarantorId { get; set; }

    public long CustomerId { get; set; }

    public long GuarantorId { get; set; }

    public long? LoanId { get; set; }

    public DateTime? LoanDate { get; set; }
}
