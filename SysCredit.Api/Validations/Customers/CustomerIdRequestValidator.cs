namespace SysCredit.Api.Validations.Customers;

using FluentValidation;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Customers;

using static Constants.ErrorCodePrefix;
using static Constants.ErrorCodes;

using static SysCredit.Api.Properties.ErrorCodeMessages;
using static SysCredit.Api.Properties.SysCreditMessages;


/// <summary>
///     Validador del <see cref="CustomerIdRequest" />
/// </summary>
[ErrorCodePrefix(AbstractValidatorPrefix)]
public class CustomerIdRequestValidator : AbstractValidator<CustomerIdRequest>
{
    /// <summary>
    ///     Se establecen las reglas para validad el <see cref="CustomerIdRequest" />
    /// </summary>
    public CustomerIdRequestValidator()
    {
        RuleFor(C => C.CustomerId)
            .NotEmpty().WithErrorCode(SERVC0125).WithMessage(GetMessageFromCode(SERVC0125)!)
            .NotNull().WithErrorCode(SERVC0126).WithMessage(GetMessageFromCode(SERVC0126)!)
            .VerifyIfCustomerExistsByIdAsync().WithErrorCode(SERVC0127).WithMessage(GetMessageFromCode(SERVC0127)!)
            .WithName(GetMessage("Customer Id")); 
    }
}
