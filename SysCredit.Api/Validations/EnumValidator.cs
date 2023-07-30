namespace SysCredit.Api.Validations;

using FluentValidation.Validators;
using FluentValidation;

public class EnumValidator<TObject, TEnum> : PropertyValidator<TObject, TEnum> where TEnum : Enum
{
    public override bool IsValid(ValidationContext<TObject> Context, TEnum Value)
    {
        return Enum.IsDefined(typeof(TEnum), Value);
    }

    protected override string GetDefaultMessageTemplate(string ErrorCode)
    {
        return "'{PropertyName}' debe tener un valor válido";
    }

    public override string Name => "EnumValidator";
}
