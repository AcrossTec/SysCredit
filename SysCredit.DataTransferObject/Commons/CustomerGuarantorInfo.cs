namespace SysCredit.DataTransferObject.Commons;

using System.Text.Json.Serialization;

using static System.Text.Json.Serialization.JsonIgnoreCondition;

public record class CustomerGuarantorInfo : IDataTransferObject
{
    [JsonIgnore(Condition = WhenWritingNull)]
    public LoanInfo? Loan { get; set; }

    [JsonIgnore(Condition = WhenWritingNull)]
    public DateTime? LoanDate { get; set; }

    public GuarantorInfo Guarantor { get; set; } = new GuarantorInfo();

    public RelationshipInfo Relationship { get; set; } = new RelationshipInfo();
}
