namespace SysCredit.Api.Requests.References;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.References;

using SysCredit.Enums;

[Validator<CreateReferenceValidator>]
public class CreateReferenceRequest : IRequest
{
    public string? Identification { get; set; }

    public string Name { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public Gender Gender { get; set; }

    public string Phone { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Address { get; set; }
}
