namespace SysCredit.Api.Requests.Relationships;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Relationships;

/// <summary>
///     Request para crear una relacion
/// </summary>
[Validator<CreateRelationshipValidator>]
public class CreateRelationshipRequest : IRequest
{
    /// <summary>
    ///     Propiedad que representa el nombre de la relacion
    /// </summary>
    public string? Name { get; set; } = string.Empty;
}
