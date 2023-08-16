namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Stores;

using SysCredit.DataTransferObject.Commons;
using SysCredit.Models;

using System.Collections.Generic;
using System.Threading.Tasks;

[Service<IRelationshipService>]
public class RelationshipService : IRelationshipService
{
    private readonly IStore<Relationship> RelationshipStore;

    public RelationshipService(IStore<Relationship> RelationshipStore)
    {
        this.RelationshipStore = RelationshipStore;
    }

    public IAsyncEnumerable<RelationshipInfo> FetchRelationshipAsync()
    {
        return RelationshipStore.FetchRelationshipAsync();
    }

    public ValueTask<bool> ExistsRelationshipAsync(long RelationshipId)
    {
        return RelationshipStore.ExistsRelationshipAsync(RelationshipId);
    }
}
