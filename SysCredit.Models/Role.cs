namespace SysCredit.Models;

public record class Role : Entity
{
    public long RoleId { get; set; }

    public string Name { get; set; } = string.Empty;
}
