namespace SysCredit.Models.Auth.Users;

public record class UserClaim : Entity
{
    public long UserClaimId { get; set; }
    public long UserId { get; set; }
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}