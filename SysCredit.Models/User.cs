namespace SysCredit.Models;

public record class User : Entity
{
    public long UserId { get; set; }

    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;
}
