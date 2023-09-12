namespace SysCredit.Api.ViewModels.Auth;

public class CreateClaimRequest : IViewModel
{
    public string ClaimType { get; set; } = string.Empty;
    public string ClaimValue { get; set; } = string.Empty;
}
