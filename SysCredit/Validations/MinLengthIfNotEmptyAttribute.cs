namespace SysCredit.Mobile.Validations;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

public class MinLengthIfNotEmptyAttribute(int Length) : MinLengthAttribute(Length)
{
    public override bool IsValid(object? PropertyValue)
    {
        return PropertyValue switch
        {
            string Value when Value.Any() => base.IsValid(Value),
            IEnumerable<object> Value when Value.Any() => base.IsValid(Value),
            _ => true
        };
    }
}
