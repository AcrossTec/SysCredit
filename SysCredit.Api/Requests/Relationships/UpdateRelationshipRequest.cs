namespace SysCredit.Api.Requests.Relationships;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Relationships;

/// <summary>
///     Request para actualizar una relacion
/// </summary>
[Validator<UpdateRelationshipValidator>]
public class UpdateRelationshipRequest : IRequest
{
    /// <summary>
    ///     Propiedad que representa el Id de la relacion
    /// </summary>
    [FromRoute]
    public long RelationshipId { get; set; }

    /// <summary>
    ///     Propiedad que representa el nombre de la relacion
    /// </summary>
    [FromBody]
    public string Name { get; set; } = String.Empty;
}
