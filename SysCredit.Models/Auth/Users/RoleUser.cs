namespace SysCredit.Models.Auth.Users;

public record class RoleUser : Entity
{
    public long RoleId { get; set; }
    public long UserId { get; set; }
}