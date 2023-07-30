namespace SysCredit.Api.DataTransferObject;

public record class RelationshipDataTransferObject
{
    public long RelationshipId { get; set; }

    public string Name { get; set; } = string.Empty;
}
