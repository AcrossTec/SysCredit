namespace SysCredit.Api.Requests.Authentications;

public class CreateClaimRequest : IRequest
{
    public string ClaimType { get; set; } = string.Empty;

    public string ClaimValue { get; set; } = string.Empty;
}
