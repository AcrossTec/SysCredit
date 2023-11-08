namespace SysCredit.Api.Validations.Relationships;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Requests.Relationships;
using static SysCredit.Api.Properties.ErrorCodeMessages;
using static SysCredit.Api.Constants.ErrorCodes;
using static SysCredit.Api.Properties.SysCreditMessages;

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
        RuleFor(R => R.Name)
            .NotNull().WithErrorCode(SERVRS0102).WithMessage(GetMessageFromCode(SERVRS0102))
            .NotEmpty().WithErrorCode(SERVRS0101).WithMessage(GetMessageFromCode(SERVRS0101))
            .RelationshipUniqueNameAsync().WithErrorCode(SERVRS0103).WithMessage(GetMessageFromCode(SERVRS0103))
            .WithName(GetMessage("Name"));
    }
}
