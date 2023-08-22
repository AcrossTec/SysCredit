namespace SysCredit.Mobile.Properties;

using System.Globalization;

[ContentProperty(nameof(Key))]
public class TranslateExtension : IMarkupExtension<string>
{
    public string? Key { get; set; }

    object IMarkupExtension.ProvideValue(IServiceProvider ServiceProvider)
    {
        return ProvideValue(ServiceProvider);
    }

    public string ProvideValue(IServiceProvider ServiceProvider)
    {
        if (Key == null)
        {
            return string.Empty;
        }

        var Translation = SysCreditResources.ResourceManager.GetString(Key, CultureInfo.CurrentUICulture);

        if (Translation == null)
        {
#if RELEASE
            Translation = Key; // HACK: returns the key, which GETS DISPLAYED TO THE USER
#else
            throw new InvalidOperationException(string.Format(CultureInfo.CurrentUICulture, SysCreditResources.TranslateKeyNotFound, Key, nameof(SysCreditResources), CultureInfo.CurrentUICulture.Name));
#endif
        }

        return Translation;
    }
}