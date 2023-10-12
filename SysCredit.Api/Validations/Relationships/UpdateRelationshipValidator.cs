namespace SysCredit.Api.Validations.Relationships;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Requests.Relationships;

/// <summary>
///     
/// </summary>
public class UpdateRelationshipValidator : AbstractValidator<UpdateRelationshipRequest>
{
    /// <summary>
    /// 
    /// </summary>
    public UpdateRelationshipValidator()
    {
        // TODO: Establecer los código de errores y mensajes.

        RuleFor(R => R.Name)
            .NotNull()  // .WithErrorCode(string.Empty)                     // .WithMessage()
            .NotEmpty() // .WithErrorCode(string.Empty)                    // .WithMessage()
            .RelationshipUniqueNameAsync()// .WithErrorCode(string.Empty) // .WithMessage()
            .WithName(SysCreditMessages.GetMessage("Name"));
    }
}
