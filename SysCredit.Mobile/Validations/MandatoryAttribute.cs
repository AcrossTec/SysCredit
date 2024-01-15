namespace SysCredit.Mobile.Validations;

using SysCredit.Mobile.Properties;

using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class MandatoryAttribute : ValidationAttribute
{
    public override bool IsValid(object? PropertyValue)
    {
        return PropertyValue switch
        {
            IEnumerable Value => Value.Cast<object?>().Any(),
            object Value => Value is not null,
            _ => false
        };
    }

    public override string FormatErrorMessage(string Name)
    {
        return string.Format(CultureInfo.CurrentUICulture, SysCreditResources.RequiredValidationError, Name);
    }
}
