namespace SysCredit.Models.Auth.Roles;

public record class RoleClaim : Entity
{
    public long RoleClaimId { get; set; }
    public long RoleId { get; set; }
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}
