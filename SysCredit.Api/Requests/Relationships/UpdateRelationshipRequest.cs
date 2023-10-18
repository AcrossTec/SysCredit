namespace SysCredit.Api.Requests.Relationships;

using Microsoft.AspNetCore.Mvc;

using SysCredit.Api.Attributes;
using SysCredit.Api.Validations.Relationships;

/// <summary>
///     Request para actualizar una relación
/// </summary>
[Validator<UpdateRelationshipValidator>]
public class UpdateRelationshipRequest : IRequest
{

    [FromRoute]
    public long RelationshipId { get; set; }


    [FromBody]
    public string Name { get; set; } = String.Empty;
}
