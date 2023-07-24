namespace SysCredit.Api.Validations;

using FluentValidation;

using SysCredit.Api.ViewModels;

public class CreateCustomerValidator : AbstractValidator<CreateCustomer>
{
    public CreateCustomerValidator()
    {
        RuleFor(C => C.Identification)
            .NotEmpty()
            .NotNull()
            .MaximumLength(16)
            .Identification()
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
            .WithName("Teléfono");
    }
}
