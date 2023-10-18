namespace SysCredit.Api.Requests.Relationships;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Relationships;

/// <summary>
///     Request para borrar una relación
/// </summary>
[Validator<DeleteRelationshipValidator>]
public class DeleteRelationshipRequest : IRequest
{
    public long RelationshipId { get; set; }
}
