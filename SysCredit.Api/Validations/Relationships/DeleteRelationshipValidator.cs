namespace SysCredit.Api.Validations.Relationships;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Relationships;

/// <summary>
///     Clase validadora de <see cref="DeleteRelationshipRequest"/>.
/// </summary>
public class DeleteRelationshipValidator : AbstractValidator<DeleteRelationshipRequest>
{
    /// <summary>
    ///     Valida el Id de la relación de parentesco.
    /// </summary>
    public DeleteRelationshipValidator()
    {
        RuleFor(R => R.RelationshipId)
            .NotNull()
            .WithName("Parentesco");
    }
}
