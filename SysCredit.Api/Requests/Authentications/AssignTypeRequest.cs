namespace SysCredit.Api.Requests.Authentications;

public class AssignTypeRequest : IRequest
{
    public string RoleName { get; set; } = string.Empty;
}
