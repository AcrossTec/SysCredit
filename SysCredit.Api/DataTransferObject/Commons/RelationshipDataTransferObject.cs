namespace SysCredit.Api.DataTransferObject.Commons;

public record class RelationshipDataTransferObject : IDataTransferObject
{
    public long RelationshipId { get; set; }

    public string Name { get; set; } = string.Empty;
}
