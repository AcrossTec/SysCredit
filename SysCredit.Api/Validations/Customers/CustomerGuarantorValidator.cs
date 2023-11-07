namespace SysCredit.Api.Validations.Customers;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Customers;

using static SysCredit.Api.Properties.ErrorCodeMessages;
using static SysCredit.Api.Properties.SysCreditMessages;
using static SysCredit.Api.Constants.ErrorCodes;

/// <summary>
///     Validador del <see cref="CustomerGuarantorValidator"/>
/// </summary>
public class CustomerGuarantorValidator : AbstractValidator<CustomerGuarantorRequest>
{
    /// <summary>
    ///     Se establecen las reglas para validar el <see cref="CustomerGuarantorValidator"/>.
    /// </summary>
    public CustomerGuarantorValidator()
    {
        RuleFor(Cg => Cg.GuarantorId)
            .ExistsGuarantorAsync().WithErrorCode(SERVC0123).WithMessage((Request) => String.Format(GetMessageFromCode(SERVC0123)!, Request.GuarantorId)!)
            .WithName(GetMessage("Guarantor Id")!);

        RuleFor(Cg => Cg.RelationshipId)
            .ExistsRelationshipAsync().WithErrorCode(SERVC0124).WithMessage((Request) => String.Format(GetMessageFromCode(SERVC0124)!, Request.RelationshipId)!)
            .WithName(GetMessage("Relationship Id")!);
    }
}
