namespace SysCredit.Api.DataTransferObject.Commons;

using System.Text.Json.Serialization;

public record class CustomerGuarantorInfo : IDataTransferObject
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public LoanInfo? Loan { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? LoanDate { get; set; }

    public GuarantorInfo Guarantor { get; set; } = new GuarantorInfo();

    public RelationshipInfo Relationship { get; set; } = new RelationshipInfo();
}
