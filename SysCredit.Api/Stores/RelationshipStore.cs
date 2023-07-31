namespace SysCredit.Api.Stores;

using SysCredit.Api.Attributes;
using SysCredit.Api.DataTransferObject;
using SysCredit.Api.Extensions;
using SysCredit.Api.Models;

[Store]
public static class RelationshipStore
{
    public static IAsyncEnumerable<RelationshipDataTransferObject> FetchRelationshipAsync(this IStore<Relationship> Store)
    {
        return Store.ExecQueryAsync<RelationshipDataTransferObject>("[dbo].[FetchRelationship]");
    }

    public static async ValueTask<bool> ExistsRelationshipAsync(this IStore<Relationship> Store, long RelationshipId)
    {
        var Relationship = await Store.ExecFirstOrDefaultAsync<RelationshipDataTransferObject>("[dbo].[FetchRelationshipById]", new { RelationshipId });
        return Relationship is not null;
    }
}
