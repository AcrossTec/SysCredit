namespace SysCredit.Models;

public record class Relationship : Entity
{
    public long RelationshipId { get; set; }

    public string Name { get; set; } = string.Empty;
}
