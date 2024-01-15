namespace SysCredit.Mobile.Controls;

using CommunityToolkit.Mvvm.Input;

public class HyperlinkSpan : Span
{
    public static readonly BindableProperty UrlProperty = BindableProperty.Create(nameof(Url), typeof(string), typeof(HyperlinkSpan));

    public HyperlinkSpan()
    {
        TextDecorations = TextDecorations.Underline;
        TextColor = Colors.Blue;
        GestureRecognizers.Add(new TapGestureRecognizer
        {
            // Launcher.OpenAsync is provided by Essentials.
            Command = new AsyncRelayCommand(() => Launcher.OpenAsync(Url))
        });
    }

    public string Url
    {
        get => (string)GetValue(UrlProperty);
        set => SetValue(UrlProperty, value);
    }
}
