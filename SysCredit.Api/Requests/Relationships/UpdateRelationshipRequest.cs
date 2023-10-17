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
    /// 
    /// </summary>
    [FromRoute]
    public long RelationshipId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [FromBody]
    public string Name { get; set; } = String.Empty;
}
