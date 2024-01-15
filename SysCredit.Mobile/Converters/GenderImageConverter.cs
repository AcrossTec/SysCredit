namespace SysCredit.Mobile.Converters;

using CommunityToolkit.Maui.Converters;

using System.Globalization;

public class GenderImageConverter : BaseConverterOneWay<SysCredit.Models.Gender, ImageSource>
{
    public GenderImageConverter()
    {
        DefaultConvertReturnValue = ImageSource.FromFile("default_gravatar.svg");
    }

    public override ImageSource ConvertFrom(SysCredit.Models.Gender Value, CultureInfo? Culture)
    {
        return Value switch
        {
            SysCredit.Models.Gender.Male => ImageSource.FromFile("male_avatar_boy_face.svg"),
            SysCredit.Models.Gender.Female => ImageSource.FromFile("female_avatar_girl_face.svg"),
            _ => DefaultConvertReturnValue
        };
    }

    public override ImageSource DefaultConvertReturnValue { get; set; }
}
