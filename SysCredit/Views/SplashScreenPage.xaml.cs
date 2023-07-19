namespace SysCredit.Views;

using SkiaSharp.Extended.UI.Controls;

public partial class SplashScreenPage : ContentPage
{
    public SplashScreenPage()
    {
        InitializeComponent();
        BindingContext = this;
    }


    private async void Lottie_PropertyChanged(object Sender, System.ComponentModel.PropertyChangedEventArgs Event)
    {
        if (Event.PropertyName == nameof(SKLottieView.IsComplete))
        {
            if (Lottie.IsComplete)
            {
                await Shell.Current.GoToAsync("///Home");
            }
        }
    }
}