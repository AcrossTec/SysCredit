using SysCredit.DataTransferObject;

public record class LoanTypeInfo : IDataTransferObject
{
    public long LoanTypeId { get; set; }

    public string Name { get; set; } = string.Empty;
}