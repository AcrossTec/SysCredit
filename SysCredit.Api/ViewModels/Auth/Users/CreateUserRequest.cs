using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Auth.Users;

namespace SysCredit.Api.ViewModels.Auth.Users;

[Validator<CreateUserValidator>]
public class CreateUserRequest : IViewModel
{
    public string UserName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;   
    
    public string Password { get; set; } = string.Empty;
    
    public string Phone { get; set; } = string.Empty;
    
    public AssignRequestType[] Roles { get; set; } = Array.Empty<AssignRequestType>();
    
    public CreateClaimRequest[] UserClaims { get; set; } = Array.Empty<CreateClaimRequest>();
}