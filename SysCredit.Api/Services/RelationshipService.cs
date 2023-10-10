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
[ServiceModel<Relationship>]
[ErrorCategory(nameof(RelationshipService))]
[ErrorCodePrefix(RelationshipServicePrefix)]
public partial class RelationshipService(IStore<Relationship> RelationshipStore)
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [MethodId("51EC53A1-3F6E-4BBB-BC32-909862FD119B")]
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
