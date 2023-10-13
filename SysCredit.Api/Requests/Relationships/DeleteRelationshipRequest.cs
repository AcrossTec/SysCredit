namespace SysCredit.Api.Requests.Relationships;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Relationships;

[Validator<DeleteRelationshipValidator>]
public class DeleteRelationshipRequest : IRequest
{
    public long RelationshipId { get; set; }
}
