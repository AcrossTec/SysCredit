namespace SysCredit.Api.DataTransferObject.Commons;

public record class CustomerGuarantorInfo : IDataTransferObject
{
    public LoanInfo? Loan { get; set; }

    public DateTime? LoanDate { get; set; }

    public GuarantorInfo Guarantor { get; set; } = new GuarantorInfo();

    public RelationshipInfo Relationship { get; set; } = new RelationshipInfo();
}
