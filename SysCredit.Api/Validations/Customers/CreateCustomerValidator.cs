namespace SysCredit.Api.Validations.Customers;

using FluentValidation;

using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Requests.Customers;

/// <summary>
///     Validador del <see cref="CreateCustomerValidator"/>.
/// </summary>
public class CreateCustomerValidator : AbstractValidator<CreateCustomerRequest>
{
    /// <summary>
    ///     Se establecen las Reglas para Validar <see cref="CreateCustomerValidator"/>.
    /// </summary>
    public CreateCustomerValidator()
    {
        RuleFor(C => C.Identification)
            .NotEmpty()
            .NotNull()
            .MaximumLength(16)
            .Identification()
            .CustomerUniqueIdentificationAsync().WithErrorCode(ErrorCodes.SERVC0101)
            .WithName("Cédula");

        RuleFor(C => C.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(64)
            .WithName("Nombre");

        RuleFor(C => C.LastName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(64)
            .WithName("Apellido");

        RuleFor(C => C.Gender)
            .Enum()
            .WithMessage(ErrorCodeMessages.GetMessageFromCode(ErrorCodes.SERVC0102))
            .WithName("Género");

        RuleFor(C => C.Email)
            .MaximumLength(64)
            .EmailAddress()
            .CustomerUniqueEmailAsync().WithErrorCode(ErrorCodes.SERVC0103)
            .WithName("Correo")
            .When(G => G.Email is not null);

        RuleFor(C => C.Address)
            .NotEmpty()
            .NotNull()
            .MaximumLength(256)
            .WithName("Dirección");

        RuleFor(C => C.Neighborhood)
            .NotEmpty()
            .NotNull()
            .MaximumLength(32)
            .WithName("Barrio");

        RuleFor(C => C.BussinessType)
            .NotEmpty()
            .NotNull()
            .MaximumLength(32)
            .WithName("Tipo Negocio");

        RuleFor(C => C.BussinessAddress)
            .NotEmpty()
            .NotNull()
            .MaximumLength(256)
            .WithName("Dirección Negocio");

        RuleFor(C => C.Phone)
            .NotEmpty()
            .NotNull()
            .MaximumLength(16)
            .Phone()
            .CustomerUniquePhoneAsync().WithErrorCode(ErrorCodes.SERVC0104)
            .WithName("Teléfono");

        RuleFor(C => C.Guarantors)
            .CustomerGuarantorsUniqueInRequest().WithErrorCode(ErrorCodes.SERVC0105)
            .WithName("Fiadores");

        RuleForEach(C => C.Guarantors)
            .ExistsGuarantorAndRelationship();

        RuleForEach(C => C.References)
            .CreateReferenceIsValid();
    }
}
