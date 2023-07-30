namespace SysCredit.Api.Interfaces;

using SysCredit.Api.DataTransferObject;

public interface IRelationshipService
{
    IAsyncEnumerable<RelationshipDataTransferObject> FetchRelationshipAsync();

    ValueTask<bool> ExistsRelationshipAsync(long RelationshipId);
}
