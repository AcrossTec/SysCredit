namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Constants;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

using static Constants.ErrorCodePrefix;

[Service<IRelationshipService>]
[ErrorCategory(nameof(RelationshipService))]
[ErrorCodePrefix(RelationshipServicePrefix)]
public class RelationshipService(IStore<Relationship> RelationshipStore, ILogger<RelationshipService> Logger) : IRelationshipService
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public IAsyncEnumerable<RelationshipInfo> FetchRelationshipAsync()
    {
        Logger.LogInformation($"CALL: {nameof(RelationshipService)}.{nameof(FetchRelationshipAsync)}");
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
