namespace SysCredit.Api.Validations.Guarantors;

using FluentValidation;
using SysCredit.Api.Constants;
using SysCredit.Api.Extensions;
using SysCredit.Api.Properties;
using SysCredit.Api.Requests.Guarantors;

using static SysCredit.Api.Properties.ErrorCodeMessages;
using static SysCredit.Api.Properties.SysCreditMessages;
using static SysCredit.Api.Constants.ErrorCodes;


/// <summary>
///     Clase validadora de <see cref="CreateGuarantorRequest"/>
/// </summary>
public class CreateGuarantorValidator : AbstractValidator<CreateGuarantorRequest>
{
    /// <summary>
    ///     Constructor de la clase <see cref="CreateGuarantorValidator"/>
    /// </summary>
    public CreateGuarantorValidator()
    {
        // Reglas para validar la identificación del fiador.
        
        RuleFor(G => G.Identification)
            .NotEmpty().WithErrorCode(SERVG0101).WithMessage(GetMessageFromCode(SERVG0101)!)
            .NotNull().WithErrorCode(SERVG0102).WithMessage(GetMessageFromCode(SERVG0102)!)
            .MaximumLength(16)
            .Identification().WithErrorCode(SERVG0103).WithMessage((Request) => String.Format(GetMessageFromCode(SERVG0103)!, Request.Identification)!)
            .GuarantorUniqueIdentificationAsync().WithErrorCode(SERVG0104).WithMessage(GetMessageFromCode(SERVG0104)!)
            .WithName(GetMessage("Identification")!);
        
        // Reglas para validar el nombre del fiador.
        RuleFor(G => G.Name)
            .NotEmpty().WithErrorCode(SERVG0105).WithMessage(GetMessageFromCode(SERVG0105)!)
            .NotNull().WithErrorCode(SERVG0106).WithMessage(GetMessageFromCode(SERVG0106)!)
            .MaximumLength(64)
            .WithName(GetMessage("Name"));

        // Reglas para validar el apellido del fiador.
        RuleFor(G => G.LastName)
            .NotEmpty().WithErrorCode(SERVG0107).WithMessage(GetMessageFromCode(SERVG0107)!)
            .NotNull().WithErrorCode(SERVG0108).WithMessage(GetMessageFromCode(SERVG0108)!)
            .MaximumLength(64)
            .WithName(GetMessage("LastName")!);

        // Reglas para validar el gemero del fiador.
        RuleFor(G => G.Gender)
            .Enum()
            .WithErrorCode(SERVG0110).WithMessage(GetMessageFromCode(SERVG0110)!)
            .WithName(GetMessage("Gender")!);

        // Reglas para validar el correo electronico del fiador.
        RuleFor(G => G.Email)
            .MaximumLength(64)
            .EmailAddress().WithErrorCode(SERVG0111).WithMessage((Request) => String.Format(GetMessageFromCode(SERVG0111)!, Request.Email)!)
            .GuarantorUniqueEmailAsync().WithErrorCode(SERVG0112).WithMessage(GetMessageFromCode(SERVG0112)!)
            .WithName(GetMessage("Email")!)
            .When(G => G.Email is not null);

        // Reglas para validar dirección del fiador 
        RuleFor(G => G.Address)
            .NotEmpty().WithErrorCode(SERVG0113).WithMessage(GetMessageFromCode(SERVG0113)!)
            .NotNull().WithErrorCode(SERVG0114).WithMessage(GetMessageFromCode(SERVG0114)!)
            .MaximumLength(256)
            .WithName(GetMessage("Address")!);

        // Reglas para validar barrio del fiador.
        RuleFor(G => G.Neighborhood)
            .NotEmpty().WithErrorCode(SERVG0115).WithMessage(GetMessageFromCode(SERVG0115)!)
            .NotNull().WithErrorCode(SERVG0116).WithMessage(GetMessageFromCode(SERVG0116)!)
            .MaximumLength(32)
            .WithName(GetMessage("Neighborhood")!);

        // Reglas para validar el tipo de negocio.
        RuleFor(G => G.BussinessType)
            .NotEmpty().WithErrorCode(SERVG0117).WithMessage(GetMessageFromCode(SERVG0117)!)
            .NotNull().WithErrorCode(SERVG0118).WithMessage(GetMessageFromCode(SERVG0118)!)
            .MaximumLength(32)
            .WithName(GetMessage("BussinessType")!);

        // Reglas para validar la dirección del negocio.
        RuleFor(G => G.BussinessAddress)
            .NotEmpty().WithErrorCode(SERVG0119).WithMessage(GetMessageFromCode(SERVG0119)!)
            .NotNull().WithErrorCode(SERVG0120).WithMessage(GetMessageFromCode(SERVG0120)!)
            .MaximumLength(256)
            .WithName(GetMessage("BussinessAddress")!);

        // Reglas para validar el numero de teléfono o celular del fiador.
        RuleFor(G => G.Phone)
            .NotEmpty().WithErrorCode(SERVG0121).WithMessage(GetMessageFromCode(SERVG0121)!)
            .NotNull().WithErrorCode(SERVG0122).WithMessage(GetMessageFromCode(SERVG0122)!)
            .MaximumLength(16)
            .Phone().WithErrorCode(SERVG0123).WithMessage((Request) => String.Format(GetMessageFromCode(SERVG0123)!, Request.Phone)!)
            .GuarantorUniquePhoneAsync().WithErrorCode(SERVG0124).WithMessage(GetMessageFromCode(SERVG0124)!)
            .WithName(GetMessage("Phone")!);
    }
}
