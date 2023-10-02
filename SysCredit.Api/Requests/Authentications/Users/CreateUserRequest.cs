namespace SysCredit.Api.Requests.Authentications.Users;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Authentications.Users;

[Validator<CreateUserValidator>]
public class CreateUserRequest : IRequest
{
    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public AssignTypeRequest[] Roles { get; set; } = Array.Empty<AssignTypeRequest>();

    public CreateClaimRequest[] UserClaims { get; set; } = Array.Empty<CreateClaimRequest>();
}