namespace SysCredit.Mobile.Views;

using CommunityToolkit.Mvvm.Messaging;

using SkiaSharp.Extended.UI.Controls;

using SysCredit.Mobile.Messages;

using System;

public partial class SplashScreenPage : ContentPage
{
    public SplashScreenPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    private async void OnLottiePropertyChanged(object Sender, System.ComponentModel.PropertyChangedEventArgs Event)
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