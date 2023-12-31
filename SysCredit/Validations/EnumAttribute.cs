﻿namespace SysCredit.Mobile.Validations;

using SysCredit.Mobile.Models;
using SysCredit.Mobile.Properties;

using System.ComponentModel.DataAnnotations;
using System.Globalization;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class EnumAttribute<TEnum> : ValidationAttribute where TEnum : Enum
{
    public override bool IsValid(object? PropertyValue)
    {
        return PropertyValue switch
        {
            TEnum EnumValue => Enum.IsDefined(typeof(TEnum), EnumValue),
            PickerData Picker when Picker.Data?.GetType().IsEnum is true => Enum.IsDefined(typeof(TEnum), Picker.Data),
            _ => false
        };
    }

    public override string FormatErrorMessage(string Name)
    {
        return string.Format(CultureInfo.CurrentUICulture, SysCreditResources.GenericValidationError, Name);
    }
}
