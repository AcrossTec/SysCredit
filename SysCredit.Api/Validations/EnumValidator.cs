namespace SysCredit.Api.Validations;

using FluentValidation;
using FluentValidation.Validators;

/// <summary>
/// 
/// </summary>
/// <typeparam name="TObject"></typeparam>
/// <typeparam name="TEnum"></typeparam>
public class EnumValidator<TObject, TEnum> : PropertyValidator<TObject, TEnum> where TEnum : Enum
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Context"></param>
    /// <param name="Value"></param>
    /// <returns></returns>
    public override bool IsValid(ValidationContext<TObject> Context, TEnum Value)
    {
        return Enum.IsDefined(typeof(TEnum), Value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="ErrorCode"></param>
    /// <returns></returns>
    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' debe tener un valor válido";
    }

    /// <summary>
    /// 
    /// </summary>
    public override string Name => "EnumValidator";
}
