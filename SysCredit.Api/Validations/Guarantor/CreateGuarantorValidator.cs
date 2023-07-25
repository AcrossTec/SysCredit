using FluentValidation;
using SysCredit.Api.ViewModels.Guarantor;

namespace SysCredit.Api.Validations.Guarantor;

public class CreateGuarantorValidator : AbstractValidator<CreateGuarantor>
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
            .NotEmpty()
            .NotNull()
            .WithName("Código de relación");
    }
}
