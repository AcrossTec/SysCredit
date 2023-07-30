namespace SysCredit.Api.Validations.Guarantor;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.ViewModels.Guarantors;

public class CreateGuarantorValidator : AbstractValidator<CreateGuarantorViewModel>
{
    public CreateGuarantorValidator()
    {
        RuleFor(G => G.Identification)
            .NotEmpty()
            .NotNull()
            .MaximumLength(16)
            .Identification()
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
            .WithName("Género");

        RuleFor(G => G.Email)
            .MaximumLength(64)
            .EmailAddress()
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
            .WithName("Teléfono");

        RuleFor(G => G.RelationshipId)
            .ExistsRelationshipAsync()
            .WithName("Parentesco");
    }
}
