namespace SysCredit.Api.Validations.Relationships;

using FluentValidation;

using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Requests.Relationships;

/// <summary>
///     Clase validadora de <see cref="UpdateRelationshipRequest"/>.
/// </summary>
public class UpdateRelationshipValidator : AbstractValidator<UpdateRelationshipRequest>
{
    /// <summary>
    ///     Valida el nombre de la relación de parentesco.
    /// </summary>
    public UpdateRelationshipValidator()
    {
        // TODO: Establecer los código de errores y mensajes.

        RuleFor(R => R.Name)
            .NotNull()  // .WithErrorCode(string.Empty)                     // .WithMessage()
            .NotEmpty() // .WithErrorCode(string.Empty)                    // .WithMessage()
            .RelationshipUniqueNameAsync().WithErrorCode(ErrorCodes.SERVRS0101)
            .WithName(SysCreditMessages.GetMessage("Name"));
    }
}
