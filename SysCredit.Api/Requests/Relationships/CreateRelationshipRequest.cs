namespace SysCredit.Api.Requests.Relationships;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Relationships;

/// <summary>
///     Request para crear un parentesco entre el cliente y el fiador.
/// </summary>
[Validator<CreateRelationshipValidator>]
public class CreateRelationshipRequest : IRequest
{
    /// <summary>
    ///     Propiedad que representa el nombre del parentesco.
    /// </summary>
    public string? Name { get; set; } = string.Empty;
}
