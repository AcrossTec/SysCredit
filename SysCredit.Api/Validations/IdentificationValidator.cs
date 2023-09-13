namespace SysCredit.Api.Validations;

using FluentValidation;
using FluentValidation.Validators;
using SysCredit.Api.Validations.DNI.Nicaragua;

public class IdentificationValidator<T> : PropertyValidator<T, string?>
{
    public override bool IsValid(ValidationContext<T> Context, string? Value)
    {
        var DniValidator = new DniNicaraguaValidator();
        return DniValidator.IsValid(Value);
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' no tiene un formato válido.";
    }

    public override string Name => "IdentificationValidator";
}
