namespace SysCredit.Api.Requests.Relationships;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Relationships;

/// <summary>
///     Request para actualizar un parentesco entre el cliente y el fiador.
/// </summary>
[Validator<UpdateRelationshipValidator>]
public class UpdateRelationshipRequest : IRequest
{
    /// <summary>
    ///     Propiedad que representa el Id del parentesco.
    /// </summary>
    [FromRoute]
    public long RelationshipId { get; set; }

    /// <summary>
    ///     Propiedad que representa el nombre del parentesco.
    /// </summary>
    [FromBody]
    public string Name { get; set; } = String.Empty;
}
