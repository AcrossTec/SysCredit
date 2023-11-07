namespace SysCredit.Api.Validations.Customers;

using FluentValidation;

using SysCredit.Api.Extensions;
using SysCredit.Api.Requests.Customers;

using static SysCredit.Api.Properties.ErrorCodeMessages;
using static SysCredit.Api.Properties.SysCreditMessages;
using static SysCredit.Api.Constants.ErrorCodes;

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
            .NotEmpty().WithErrorCode(SERVC0101).WithMessage(GetMessageFromCode(SERVC0101)!)
            .NotNull().WithErrorCode(SERVC0102).WithMessage(GetMessageFromCode(SERVC0102)!)
            .MaximumLength(16)
            .Identification().WithErrorCode(SERVC0103).WithMessage((Request) => String.Format(GetMessageFromCode(SERVC0103)!, Request.Identification)!)
            .CustomerUniqueIdentificationAsync().WithErrorCode(SERVC0104).WithMessage(GetMessageFromCode(SERVC0104)!)
            .WithName(GetMessage("Identification")!);

        RuleFor(C => C.Name)
            .NotEmpty().WithErrorCode(SERVC0105).WithMessage(GetMessageFromCode(SERVC0105)!)
            .NotNull().WithErrorCode(SERVC0106).WithMessage(GetMessageFromCode(SERVC0106)!)
            .MaximumLength(64)
            .WithName(GetMessage("Name")!);

        RuleFor(C => C.LastName)
            .NotEmpty().WithErrorCode(SERVC0107).WithMessage(GetMessageFromCode(SERVC0107)!)
            .NotNull().WithErrorCode(SERVC0108).WithMessage(GetMessageFromCode(SERVC0108)!)
            .MaximumLength(64)
            .WithName(GetMessage("LastName")!);

        RuleFor(C => C.Gender)
            .Enum()
             .WithErrorCode(SERVC0109).WithMessage(GetMessageFromCode(SERVC0109)!)
            .WithName(GetMessage("Gender")!);

        RuleFor(C => C.Email)
            .MaximumLength(64)
            .EmailAddress().WithErrorCode(SERVC0110).WithMessage((Request) => String.Format(GetMessageFromCode(SERVC0110)!, Request.Email)!)
            .CustomerUniqueEmailAsync().WithErrorCode(SERVC0111).WithMessage(GetMessageFromCode(SERVC0111)!)
            .WithName(GetMessage("Email")!)
            .When(G => G.Email is not null);

        RuleFor(C => C.Address)
            .NotEmpty().WithErrorCode(SERVC0112).WithMessage(GetMessageFromCode(SERVC0112)!)
            .NotNull().WithErrorCode(SERVC0113).WithMessage(GetMessageFromCode(SERVC0113)!)
            .MaximumLength(256)
            .WithName(GetMessage("Address")!);

        RuleFor(C => C.Neighborhood)
            .NotEmpty().WithErrorCode(SERVC0114).WithMessage(GetMessageFromCode(SERVC0114)!)
            .NotNull().WithErrorCode(SERVC0115).WithMessage(GetMessageFromCode(SERVC0115)!)
            .MaximumLength(32)
            .WithName(GetMessage("Neighborhood")!);

        RuleFor(C => C.BussinessType)
            .NotEmpty().WithErrorCode(SERVC0116).WithMessage(GetMessageFromCode(SERVC0116)!)
            .NotNull().WithErrorCode(SERVC0117).WithMessage(GetMessageFromCode(SERVC0117)!)
            .MaximumLength(32)
            .WithName(GetMessage("BussinessType")!);

        RuleFor(C => C.BussinessAddress)
            .NotEmpty().WithErrorCode(SERVC0118).WithMessage(GetMessageFromCode(SERVC0118)!)
            .NotNull().WithErrorCode(SERVC0119).WithMessage(GetMessageFromCode(SERVC0119)!)
            .MaximumLength(256)
            .WithName(GetMessage("BussinessAddress")!);

        RuleFor(C => C.Phone)
            .NotEmpty().WithErrorCode(SERVC0120).WithMessage(GetMessageFromCode(SERVC0120)!)
            .NotNull().WithErrorCode(SERVC0121).WithMessage(GetMessageFromCode(SERVC0121)!)
            .MaximumLength(16)
            .Phone().WithErrorCode(SERVG0122).WithMessage((Request) => String.Format(GetMessageFromCode(SERVG0122)!, Request.Phone)!)
            .CustomerUniquePhoneAsync()
            .WithName(GetMessage("BussinessAddress")!);

        RuleFor(C => C.Guarantors)
            .CustomerGuarantorsUniqueInRequest().WithErrorCode(SERVC0128).WithMessage(GetMessageFromCode(SERVC0128)!)
            .WithName(GetMessage("Guarantors")!);

        RuleForEach(C => C.Guarantors)
            .ExistsGuarantorAndRelationship();

        RuleForEach(C => C.References)
            .CreateReferenceIsValid();
    }
}
