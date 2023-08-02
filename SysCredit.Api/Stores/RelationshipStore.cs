namespace SysCredit.Api.Stores;

using SysCredit.Api.Attributes;
using SysCredit.Api.DataTransferObject.Commons;
using SysCredit.Api.Extensions;
using SysCredit.Api.Models;

[Store]
public static class RelationshipStore
{
    public static IAsyncEnumerable<RelationshipInfo> FetchRelationshipAsync(this IStore<Relationship> Store)
    {
        return Store.ExecQueryAsync<RelationshipInfo>("[dbo].[FetchRelationship]");
    }

    public static async ValueTask<bool> ExistsRelationshipAsync(this IStore<Relationship> Store, long RelationshipId)
    {
        var Relationship = await Store.ExecFirstOrDefaultAsync<RelationshipInfo>("[dbo].[FetchRelationshipById]", new { RelationshipId });
        return Relationship is not null;
    }
}
