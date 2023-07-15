namespace SysCredit.Views;

using System;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        InitializeEvents();
    }

    private void InitializeEvents()
    {
        ClickGestureRecognizer AddClientGesture = new();
        AddClientGesture.Clicked += AddClientGesture_Clicked;
        AddClientGesture.NumberOfClicksRequired = 1;
        NewClient.GestureRecognizers.Add(AddClientGesture);
    }

    private async void AddClientGesture_Clicked(object? Sender, EventArgs Event)
    {
        await Shell.Current.GoToAsync("Client/Add");
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Client/Add");
    }
}
