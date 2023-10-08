namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Interfaces.Services;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

using static Constants.ErrorCodePrefix;

/// <summary>
/// 
/// </summary>
/// <param name="RelationshipStore"></param>
/// <param name="Logger"></param>
[Service<IRelationshipService>]
[ErrorCategory(nameof(RelationshipService))]
[ErrorCodePrefix(RelationshipServicePrefix)]
public class RelationshipService(IStore<Relationship> RelationshipStore) : IRelationshipService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IAsyncEnumerable<RelationshipInfo> FetchRelationshipAsync()
    {
        return RelationshipStore.FetchRelationshipAsync();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="RelationshipId"></param>
    /// <returns></returns>
    public ValueTask<bool> ExistsRelationshipAsync(long RelationshipId)
    {
        return RelationshipStore.ExistsRelationshipAsync(RelationshipId);
    }
}
