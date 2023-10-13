namespace SysCredit.Api.Validations.Relationships;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Relationships;

public class CreateRelationshipValidator : AbstractValidator<CreateRelationshipRequest>
{
    public CreateRelationshipValidator()
    {
        RuleFor(R => R.Name)
            .NotEmpty()
            .NotNull()
            .RelationshipUniqueNameAsync()
            .WithName("Nombre");
    }
}
