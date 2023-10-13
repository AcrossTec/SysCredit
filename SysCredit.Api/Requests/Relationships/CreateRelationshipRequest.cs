namespace SysCredit.Api.Requests.Relationships;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Relationships;

[Validator<CreateRelationshipValidator>]
public class CreateRelationshipRequest : IRequest
{
    public string? Name { get; set; } = string.Empty;
}
