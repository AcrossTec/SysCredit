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
        await Shell.Current.GoToAsync("ClientNew?BackButtonBehavior=True");
    }

    private async void OnListClientClicked(object Sender, EventArgs Event)
    {
        await Shell.Current.GoToAsync("ClientShow?BackButtonBehavior=True");
    }

    private async void OnLoanRequestClicked(object Sender, EventArgs Event)
    {
        await Shell.Current.GoToAsync("LoanNewRequest?BackButtonBehavior=True");
    }
}
