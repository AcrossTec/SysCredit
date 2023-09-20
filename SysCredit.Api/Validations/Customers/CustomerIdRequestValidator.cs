namespace SysCredit.Api.Validations.Customers;

using FluentValidation;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Customers;

using static Constants.ErrorCodeNumber;
using static Constants.ErrorCodePrefix;

/// <summary>
///     Validador del <see cref="CustomerIdRequest" />
/// </summary>
public class CustomerIdRequestValidator : AbstractValidator<CustomerIdRequest>
{

    /// <summary>
    ///     Se establecen las reglas para validad el <see cref="CustomerIdRequest" />
    /// </summary>
    public CustomerIdRequestValidator()
    {
        RuleFor(C => C.CustomerId)
            .NotEmpty()
            .NotNull()
            .VerifyIfCustomerExistsByIdAsync().WithErrorCode($"{CustomerServicePrefix}{_0003}")
            .WithName("Customer Id");
    }
}
