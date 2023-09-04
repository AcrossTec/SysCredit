namespace SysCredit.Models;

public record class LoanType : Entity
{
    public long LoanTypeId { get; set; }

    public String Name { get; set; } = String.Empty;
}

