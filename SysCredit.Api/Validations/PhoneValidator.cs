namespace SysCredit.Api.Validations;

using FluentValidation;
using FluentValidation.Validators;

using System.Text.RegularExpressions;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class PhoneValidator<T> : PropertyValidator<T, string?>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Context"></param>
    /// <param name="Value"></param>
    /// <returns></returns>
    public override bool IsValid(ValidationContext<T> Context, string? Value)
    {
        return Regex.IsMatch(Value ?? string.Empty, @"[578]{1}\d{7}");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ErrorCode"></param>
    /// <returns></returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' no tiene un formato válido.";
    }

    /// <summary>
    /// 
    /// </summary>
    public override string Name => "PhoneValidator";
}