namespace SysCredit.Api.Requests.Relationships;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Relationships;

/// <summary>
///     Request para borrar un parentesco que no está siendo un usado por un cliente y un fiador.
/// </summary>
[Validator<DeleteRelationshipValidator>]
public class DeleteRelationshipRequest : IRequest
{
    /// <summary>
    ///     Propiedad que representa el Id del parentesco.
    /// </summary>
    public long RelationshipId { get; set; }
}
