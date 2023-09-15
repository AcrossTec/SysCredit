namespace SysCredit.Api.Validations.References;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.References;

public class CreateReferenceValidator : AbstractValidator<CreateReferenceRequest>
{
    public CreateReferenceValidator()
    {
        RuleFor(R => R.Identification)
            .NotEmpty()
            .MaximumLength(16)
            .Identification()
            .WithName("Cédula")
            .When(R => R.Identification is not null);

        RuleFor(R => R.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(64)
            .WithName("Nombre");

        RuleFor(R => R.LastName)
            .NotEmpty()
            .NotNull()
            .MaximumLength(64)
            .WithName("Apellido");

        RuleFor(R => R.Gender)
            .Enum()
            .WithMessage("'{PropertyName}' debe tener un género válido: Hombre o Mujer")
            .WithName("Género");

        RuleFor(R => R.Email)
            .MaximumLength(64)
            .EmailAddress()
            .WithName("Correo")
            .When(G => G.Email is not null);

        RuleFor(R => R.Address)
            .NotEmpty()
            .NotNull()
            .MaximumLength(256)
            .WithName("Dirección")
            .When(R => R.Address is not null);

        RuleFor(R => R.Phone)
            .NotEmpty()
            .NotNull()
            .MaximumLength(16)
            .Phone()
            .WithName("Teléfono");
    }
}
