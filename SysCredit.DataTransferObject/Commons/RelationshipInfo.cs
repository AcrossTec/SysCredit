namespace SysCredit.DataTransferObject.Commons;

public record class RelationshipInfo : IDataTransferObject
{
    public long RelationshipId { get; set; }

    public string Name { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
    public DateTime? DeletedDate { get; set; }
    public bool IsEdit { get; set; }
    public bool IsDelete { get; set; }


}
