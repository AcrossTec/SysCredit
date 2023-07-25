using SysCredit.Api.DataTransferObject;
using SysCredit.Api.ViewModels.Reference;

namespace SysCredit.Api.Helpers.Extensions;

public static class RelationshipDataTransferObjectExtension
{
    public static IAsyncEnumerable<ReferenceDataTransferObject> AsListDto(
        this IAsyncEnumerable<ReferenceOption> Relationships)
    {
        return Relationships.Select(R => R.AsDto());
    }

    public static ReferenceDataTransferObject AsDto(this ReferenceOption relationshipOption)
        => new ReferenceDataTransferObject
        {
            RelationshipId = relationshipOption.RelationshipId,
            Name = relationshipOption.Name,
        };
}