namespace SysCredit.Mobile.Converters;

using CommunityToolkit.Maui.Converters;

using System.Globalization;

public class NotConverter : BaseConverterOneWay<bool, bool>
{
    public override bool ConvertFrom(bool Value, CultureInfo? Culture)
    {
        return !Value;
    }

    public override bool DefaultConvertReturnValue { get; set; }
}
