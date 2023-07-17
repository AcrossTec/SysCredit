namespace SysCredit.Views;

using SysCredit.Views.Clients;

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
    }

    private async void OnAddClientClicked(object Sender, EventArgs Event)
    {
        await Shell.Current.GoToAsync("Client/Add?BackButtonBehavior=True");
    }

    private async void OnListClientClicked(object Sender, EventArgs Event)
    {
        await Shell.Current.GoToAsync("Client/List?BackButtonBehavior=True");
    }

    private async void OnLoanRequestClicked(object Sender, EventArgs Event)
    {
        await Shell.Current.GoToAsync("Loan/Request?BackButtonBehavior=True");
    }
}
