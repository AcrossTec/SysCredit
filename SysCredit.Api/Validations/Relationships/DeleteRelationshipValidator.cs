namespace SysCredit.Api.Validations.Relationships;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Relationships;
using static SysCredit.Api.Properties.ErrorCodeMessages;
using static SysCredit.Api.Constants.ErrorCodes;
using static SysCredit.Api.Properties.SysCreditMessages;

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
            .NotNull().WithErrorCode(SERVRS0104).WithMessage(GetMessageFromCode(SERVRS0104))
            .WithName(GetMessage("RelationshipId"));
    }
}
