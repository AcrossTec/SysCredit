namespace SysCredit.Models;

public record class UserRole : Entity
{
    public long RoleId { get; set; }

    public long UserId { get; set; }
}
