namespace SysCredit.Mobile.Views;

public partial class SplashScreenPage : ContentPage
{
    public SplashScreenPage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(1000);

        await Shell.Current.GoToAsync("///Presentations/SysCredit/Presentation1");
    }
}