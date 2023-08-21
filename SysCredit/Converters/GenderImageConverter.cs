namespace SysCredit.Mobile.Converters;

using CommunityToolkit.Maui.Converters;

using SysCredit.Enums;

using System.Globalization;

public class GenderImageConverter : BaseConverterOneWay<Gender, ImageSource>
{
    public GenderImageConverter()
    {
        DefaultConvertReturnValue = ImageSource.FromFile("default_gravatar.svg");
    }

    public override ImageSource ConvertFrom(Gender Value, CultureInfo? Culture)
    {
        return Value switch
        {
            Gender.Male => ImageSource.FromFile("male_avatar_boy_face.svg"),
            Gender.Female => ImageSource.FromFile("female_avatar_girl_face.svg"),
            _ => DefaultConvertReturnValue
        };

    }

    public override ImageSource DefaultConvertReturnValue { get; set; }
}
