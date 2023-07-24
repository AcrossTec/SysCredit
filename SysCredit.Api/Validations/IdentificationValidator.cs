namespace SysCredit.Api.Validations;

using FluentValidation.Validators;
using FluentValidation;
using System.Text.RegularExpressions;

public class IdentificationValidator<T> : PropertyValidator<T, string?>
{
    public override bool IsValid(ValidationContext<T> Context, string? Value)
    {
        return Regex.IsMatch(Value ?? string.Empty, @"\d{3}-\d{6}-\d{4}[A-Za-z]{1}");
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' no tiene un formato válido.";
    }

    public override string Name => "IdentificationValidator";
}
