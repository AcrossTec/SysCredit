namespace SysCredit.Api.Validations.Relationships;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;

using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

public class AsyncExistsRelationshipValidator<T> : AsyncPropertyValidator<T, long>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, long RelationshipId, CancellationToken Cancellation)
    {
        return await Context.RootContextData[nameof(RelationshipStore)].AsStore<Relationship>().ExistsRelationshipAsync(RelationshipId);
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' no está registrado.";
    }

    public override string Name => "AsyncExistsRelationshipValidator";
}
