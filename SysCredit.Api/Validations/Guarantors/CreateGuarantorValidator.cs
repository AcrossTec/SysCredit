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
    public CreateGuarantorValidator()
    {
        RuleFor(G => G.Identification)
            .NotEmpty().WithErrorCode(SERVG0106).WithMessage(GetMessageFromCode(SERVG0106)!)
            .NotNull().WithErrorCode(SERVG0107).WithMessage(GetMessageFromCode(SERVG0107)!)
            .MaximumLength(16)
            .Identification().WithErrorCode(SERVG0108).WithMessage((Request)=> String.Format(GetMessageFromCode(SERVG0108)!, Request.Identification)!)
            .GuarantorUniqueIdentificationAsync().WithErrorCode(SERVG0101).WithMessage(GetMessageFromCode(SERVG0101)!)
            .WithName(GetMessage("Identification"));

        RuleFor(G => G.Name)
            .NotEmpty().WithErrorCode(SERVG0109).WithMessage(GetMessageFromCode(SERVG0109)!)
            .NotNull().WithErrorCode(SERVG0110).WithMessage(GetMessageFromCode(SERVG0110)!)
            .MaximumLength(64)
            .WithName(GetMessage("Name")!);

        RuleFor(G => G.LastName)
            .NotEmpty().WithErrorCode(SERVG0111).WithMessage(GetMessageFromCode(SERVG0111)!)
            .NotNull().WithErrorCode(SERVG0112).WithMessage(GetMessageFromCode(SERVG0112)!)
            .MaximumLength(64)
            .WithName(GetMessage("LastName")!);

        RuleFor(G => G.Gender)
            .Enum()
            .WithErrorCode(SERVG0102).WithMessage(GetMessageFromCode(SERVG0102)!)
            .WithName(GetMessage("Gender")!);

        RuleFor(G => G.Email)
            .MaximumLength(64)
            .EmailAddress().WithErrorCode(SERVG0114).WithMessage((Request)=> String.Format(GetMessageFromCode(SERVG0114)!, Request.Email)!)
            .GuarantorUniqueEmailAsync().WithErrorCode(SERVG0103).WithMessage(GetMessageFromCode(SERVG0103)!)
            .WithName(GetMessage("Email")!)
            .When(G => G.Email is not null);

        RuleFor(G => G.Address)
            .NotEmpty().WithErrorCode(SERVG0115).WithMessage(GetMessageFromCode(SERVG0115)!)
            .NotNull().WithErrorCode(SERVG0116).WithMessage(GetMessageFromCode(SERVG0116)!)
            .MaximumLength(256)
            .WithName(GetMessage("Address")!);

        RuleFor(G => G.Neighborhood)
            .NotEmpty().WithErrorCode(SERVG0117).WithMessage(GetMessageFromCode(SERVG0117)!)
            .NotNull().WithErrorCode(SERVG0118).WithMessage(GetMessageFromCode(SERVG0118)!)
            .MaximumLength(32)
            .WithName(GetMessage("Neighborhood")!);

        RuleFor(G => G.BussinessType)
            .NotEmpty().WithErrorCode(SERVG0119).WithMessage(GetMessageFromCode(SERVG0119)!)
            .NotNull().WithErrorCode(SERVG0120).WithMessage(GetMessageFromCode(SERVG0120)!)
            .MaximumLength(32)
            .WithName(GetMessage("BussinessType")!);

        RuleFor(G => G.BussinessAddress)
            .NotEmpty().WithErrorCode(SERVG0121).WithMessage(GetMessageFromCode(SERVG0121)!)
            .NotNull().WithErrorCode(SERVC0122).WithMessage(GetMessageFromCode(SERVC0122)!)
            .MaximumLength(256)
            .WithName(GetMessage("BussinessAddress")!);

        RuleFor(G => G.Phone)
            .NotEmpty().WithErrorCode(SERVG0123).WithMessage(GetMessageFromCode(SERVG0123)!)
            .NotNull().WithErrorCode(SERVG0124).WithMessage(GetMessageFromCode(SERVG0124)!)
            .MaximumLength(16)
            .Phone().WithErrorCode(SERVG0125).WithMessage((Request) => String.Format(GetMessageFromCode(SERVG0125)!, Request.Phone)!)
            .GuarantorUniquePhoneAsync().WithErrorCode(ErrorCodes.SERVG0104)
            .WithName(GetMessage("Phone")!);
    }
}
