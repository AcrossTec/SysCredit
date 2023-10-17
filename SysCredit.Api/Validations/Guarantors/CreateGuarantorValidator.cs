namespace SysCredit.Api.Validations.Guarantors;

using FluentValidation;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Requests.Guarantors;

/// <summary>
///     Clase validadora de <see cref="CreateGuarantorRequest"/>
/// </summary>
public class CreateGuarantorValidator : AbstractValidator<CreateGuarantorRequest>
{
    public CreateGuarantorValidator()
    {
        RuleFor(G => G.Identification)
            .NotEmpty()
            .NotNull()
            .MaximumLength(16)
            .Identification()
            .GuarantorUniqueIdentificationAsync().WithErrorCode(ErrorCodes.SERVG0101)
            .WithName("Cédula");

        RuleFor(G => G.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(64)
            .WithName("Nombre");

        RuleFor(G => G.LastName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(64)
            .WithName("Apellido");

        RuleFor(G => G.Gender)
            .Enum()
            .WithMessage(ErrorCodeMessages.GetMessageFromCode(ErrorCodes.SERVG0102))
            .WithName("Género");

        RuleFor(G => G.Email)
            .MaximumLength(64)
            .EmailAddress()
            .GuarantorUniqueEmailAsync().WithErrorCode(ErrorCodes.SERVG0103)
            .WithName("Correo")
            .When(G => G.Email is not null);

        RuleFor(G => G.Address)
            .NotEmpty()
            .NotNull()
            .MaximumLength(256)
            .WithName("Dirección");

        RuleFor(G => G.Neighborhood)
            .NotEmpty()
            .NotNull()
            .MaximumLength(32)
            .WithName("Barrio");

        RuleFor(G => G.BussinessType)
            .NotEmpty()
            .NotNull()
            .MaximumLength(32)
            .WithName("Tipo Negocio");

        RuleFor(G => G.BussinessAddress)
            .NotEmpty()
            .NotNull()
            .MaximumLength(256)
            .WithName("Dirección Negocio");

        RuleFor(G => G.Phone)
            .NotEmpty()
            .NotNull()
            .MaximumLength(16)
            .Phone()
            .GuarantorUniquePhoneAsync().WithErrorCode(ErrorCodes.SERVG0104)
            .WithName("Teléfono");
    }
}
