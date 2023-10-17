namespace SysCredit.Api.Validations.Customers;

using FluentValidation;

using SysCredit.Api.Extensions;
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
            .CustomerUniqueIdentificationAsync()
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
            .WithMessage("'{PropertyName}' debe tener un género válido: Hombre o Mujer")
            .WithName("Género");

        RuleFor(C => C.Email)
            .MaximumLength(64)
            .EmailAddress()
            .CustomerUniqueEmailAsync()
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
            .CustomerUniquePhoneAsync()
            .WithName("Teléfono");

        RuleFor(C => C.Guarantors)
            .CustomerGuarantorsUniqueInRequest()
            .WithName("Fiadores");

        RuleForEach(C => C.Guarantors)
            .ExistsGuarantorAndRelationship();

        RuleForEach(C => C.References)
            .CreateReferenceIsValid();
    }
}
