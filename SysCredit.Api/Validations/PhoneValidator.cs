namespace SysCredit.Api.Validations;

using FluentValidation;
using FluentValidation.Validators;

using System.Text.RegularExpressions;

public class PhoneValidator<T> : PropertyValidator<T, string?>
{
    public override bool IsValid(ValidationContext<T> Context, string? Value)
    {
        return Regex.IsMatch(Value ?? string.Empty, @"[578]{1}\d{7}");
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' no tiene un formato válido.";
    }

    public override string Name => "PhoneValidator";
}