namespace SysCredit.Mobile.Validations;

using SysCredit.Mobile.Properties;
using System.ComponentModel.DataAnnotations;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class FieldMustContainsOnlyLettersAttribute : DataTypeAttribute
{
    public FieldMustContainsOnlyLettersAttribute() : base(DataType.Text)
    {
        ErrorMessageResourceType = typeof(SysCreditResources);
        ErrorMessageResourceName = nameof(SysCreditResources.NameIsOnlyLettersValidationError);
    }

    public override bool IsValid(object? Value)
    {
        if (Value == null) return true;
        if (Value is not string ValueAsString) return false;
        return System.Text.RegularExpressions.Regex.IsMatch(ValueAsString, @"^[a-zA-Z ]+$");
    }
}