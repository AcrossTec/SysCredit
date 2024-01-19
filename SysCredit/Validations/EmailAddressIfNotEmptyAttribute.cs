namespace SysCredit.Mobile.Validations;

using SysCredit.Mobile.Properties;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class EmailAddressIfNotEmptyAttribute : DataTypeAttribute
{
    public EmailAddressIfNotEmptyAttribute() : base(DataType.EmailAddress)
    {
        ErrorMessageResourceType = typeof(SysCreditResources);
        ErrorMessageResourceName = nameof(SysCreditResources.Validation_EmailFormatError);
    }

    public override bool IsValid(object? Value)
    {
        if (Value == null) return true;
        if (Value is not string ValueAsString) return false;
        if (ValueAsString.Length == 0) return true;

        // Only return true if there is only 1 '@' character and it is neither the first nor the last character
        int Index = ValueAsString.IndexOf('@');

        return (Index > 0) && (Index != ValueAsString.Length - 1) && (Index == ValueAsString.LastIndexOf('@'));
    }
}
