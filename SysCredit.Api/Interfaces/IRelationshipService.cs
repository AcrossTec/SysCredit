namespace SysCredit.Api.Interfaces;

using SysCredit.Api.DataTransferObject.Commons;

public interface IRelationshipService
{
    IAsyncEnumerable<RelationshipInfo> FetchRelationshipAsync();

    ValueTask<bool> ExistsRelationshipAsync(long RelationshipId);
}
