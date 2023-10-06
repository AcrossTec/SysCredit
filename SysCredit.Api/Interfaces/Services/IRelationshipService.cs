namespace SysCredit.Api.Interfaces.Services;

using SysCredit.DataTransferObject.Commons;

public interface IRelationshipService
{
    IAsyncEnumerable<RelationshipInfo> FetchRelationshipAsync();

    ValueTask<bool> ExistsRelationshipAsync(long RelationshipId);

    ValueTask<RelationshipInfo?> FetchRelationshipByLoanTypeIdAsync(long LoanTypeId);
}
