namespace SysCredit.Api.Validations.Relationships;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Relationships;

public class DeleteRelationshipValidator : AbstractValidator<DeleteRelationshipRequest>
{
    public DeleteRelationshipValidator()
    {
        RuleFor(R => R.RelationshipId)
            .NotNull()
            .WithName("Parentesco");
    }
}
