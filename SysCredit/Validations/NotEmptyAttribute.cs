namespace SysCredit.Mobile.Validations;

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
public class NotEmptyAttribute : ValidationAttribute
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
        return $"El campo '{Name}' es requerido.";
    }
}
