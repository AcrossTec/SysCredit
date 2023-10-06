namespace SysCredit.Api.Stores;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

using static Constants.ErrorCodePrefix;

[Store]
[ErrorCategory(nameof(RelationshipStore))]
[ErrorCodePrefix(RelationshipStorePrefix)]
public static class RelationshipStore
{
    [MethodId("9D9648AF-EE89-4B08-9B6E-96016C086D3F")]
    public static IAsyncEnumerable<RelationshipInfo> FetchRelationshipAsync(this IStore<Relationship> Store)
    {
        return Store.ExecQueryAsync<RelationshipInfo>("[dbo].[FetchRelationship]");
    }

    [MethodId("EF6EE502-2516-462D-9FF8-8FABF10C913C")]
    public static async ValueTask<bool> ExistsRelationshipAsync(this IStore<Relationship> Store, long RelationshipId)
    {
        var Relationship = await Store.FetchRelationshipById(RelationshipId);
        return Relationship is not null;
    }

    [MethodId("44434D9E-2ECE-4DA2-A6F6-567D915E0230")]
    public static async ValueTask<RelationshipInfo?> FetchRelationshipById(this IStore<Relationship> Store, long? RelationshipId)
    {
        return await Store.ExecFirstOrDefaultAsync<RelationshipInfo?>("[dbo].[FetchRelationshipById]", new { RelationshipId });
    }

    [MethodId("A8D714A9-0792-446D-AE24-FD6AC3A1AE23")]
    public static async ValueTask<RelationshipInfo?> FetchRelationshipByLoanTypeIdAsync(this IStore<Relationship> Store, long LoanTypeId)
    {
        return await Store.ExecFirstOrDefaultAsync<RelationshipInfo?>("[dbo].[FetchRelationshipByLoanTypeId]", new { LoanTypeId });
    }
}
