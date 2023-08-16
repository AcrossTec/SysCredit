namespace SysCredit.DataTransferObject.Commons;

public record class RelationshipInfo : IDataTransferObject
{
    public long RelationshipId { get; set; }

    public string Name { get; set; } = string.Empty;
}
