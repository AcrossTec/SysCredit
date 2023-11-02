namespace SysCredit.Api.Validations.Customers;

using FluentValidation;
using log4net.Core;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Customers;

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
            .ExistsGuarantorAsync().WithErrorCode(ErrorCodes.SERVC0107)
            .WithName("Fiador");

        RuleFor(Cg => Cg.RelationshipId)
            .ExistsRelationshipAsync().WithErrorCode(ErrorCodes.SERVC0108)
            .WithName("Parentesco");
    }
}
