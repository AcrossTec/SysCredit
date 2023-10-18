namespace SysCredit.Api.Requests.Relationships;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Relationships;

/// <summary>
///     Request para borrar una relacion
/// </summary>
[Validator<DeleteRelationshipValidator>]
public class DeleteRelationshipRequest : IRequest
{
    /// <summary>
    ///     Propiedad que representa el Id de la relacion
    /// </summary>
    public long RelationshipId { get; set; }
}
