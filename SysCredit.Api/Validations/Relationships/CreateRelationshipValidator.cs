namespace SysCredit.Api.Validations.Relationships;

using FluentValidation;

using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Relationships;

/// <summary>
///     Clase validadora de <see cref="CreateRelationshipRequest"/>.
/// </summary>
public class CreateRelationshipValidator : AbstractValidator<CreateRelationshipRequest>
{
    /// <summary>
    ///     Valida el nombre de la relación de parentesco.
    /// </summary>
    public CreateRelationshipValidator()
    {
        RuleFor(R => R.Name)
            .NotEmpty()
            .NotNull()
            .RelationshipUniqueNameAsync().WithErrorCode(ErrorCodes.SERVRS0101)
            .WithName("Nombre");
    }
}
