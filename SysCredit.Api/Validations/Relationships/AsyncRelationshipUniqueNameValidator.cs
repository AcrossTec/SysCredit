namespace SysCredit.Api.Validations.Relationships;

using FluentValidation;
using FluentValidation.Validators;

using SysCredit.Api.Extensions;
using SysCredit.Api.Stores;
using SysCredit.Models;

using System.Threading;
using System.Threading.Tasks;

public class AsyncRelationshipUniqueNameValidator<T> : AsyncPropertyValidator<T, string?>
{
    public override async Task<bool> IsValidAsync(ValidationContext<T> Context, string? Name, CancellationToken Cancellation)
    {
        var Relationship = await Context.RootContextData[nameof(RelationshipStore)].AsStore<Relationship>().FetchRelationshipByNameAsync(Name);
        return Relationship is null;
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' Ya existe un registro con este valor";
    }

    public override string Name => "AsyncRelationshipUniqueNameValidator";
}

