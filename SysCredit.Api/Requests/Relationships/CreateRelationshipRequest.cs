namespace SysCredit.Api.Requests.Relationships;

public class CreateRelationshipRequest : IRequest
{
    public string Name { get; set; } = string.Empty;
}
