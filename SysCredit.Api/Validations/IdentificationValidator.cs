namespace SysCredit.Api.Validations;

using FluentValidation;
using FluentValidation.Validators;

using System.Text.RegularExpressions;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class IdentificationValidator<T> : PropertyValidator<T, string?>
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Context"></param>
    /// <param name="Value"></param>
    /// <returns></returns>
    public override bool IsValid(ValidationContext<T> Context, string? Value)
    {
        return Regex.IsMatch(Value ?? string.Empty, @"\d{3}-\d{6}-\d{4}[A-Za-z]{1}");
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
    public override string Name => "IdentificationValidator";
}
