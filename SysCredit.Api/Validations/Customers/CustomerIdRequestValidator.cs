namespace SysCredit.Api.Validations.Customers;

using FluentValidation;

using SysCredit.Api.Attributes;
using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Customers;

using static Constants.ErrorCodePrefix;
using static Constants.ErrorCodes;

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
            .NotEmpty()
            .NotNull()
            .VerifyIfCustomerExistsByIdAsync().WithErrorCode(""/*SERVC0003*/) // TODO: Crear las entradas ERR<ErrorCodeNumber>
            .WithName("Customer Id"); // TODO: Configurar los nombres de propiedades
    }
}
