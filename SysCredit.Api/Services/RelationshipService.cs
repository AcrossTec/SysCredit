namespace SysCredit.Api.Services;

using SysCredit.Api.Attributes;
using SysCredit.Api.DataTransferObject.Commons;
using SysCredit.Api.Interfaces;
using SysCredit.Api.Models;
using SysCredit.Api.Stores;

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

    public IAsyncEnumerable<RelationshipDataTransferObject> FetchRelationshipAsync()
    {
        return RelationshipStore.FetchRelationshipAsync();
    }

    public ValueTask<bool> ExistsRelationshipAsync(long RelationshipId)
    {
        return RelationshipStore.ExistsRelationshipAsync(RelationshipId);
    }
}
