namespace SysCredit.Views;

using CommunityToolkit.Maui.Markup;

using Maui.FreakyControls;

using Microsoft.Maui;
using Microsoft.Maui.Controls;

using SysCredit.Views.Clients;
using SysCredit.Views.Loans;

using System;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        InitializeControls();
    }

    private void InitializeControls()
    {
        foreach (FreakyButton Child in MainLayout)
        {
            ButtonSetting(Child);
        }
    }

    private void ButtonSetting(FreakyButton Button)
    {
        Grid MainGrid = Button.FindByName<Grid>("mainGrid");

        MainGrid.Margin = new Thickness(0, 15, 0, 10);
        MainGrid.ColumnDefinitions.Clear();
        MainGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));

        foreach (View Child in MainGrid.Children)
        {
            Grid.SetColumn(Child, 0);
        }

        MainGrid.FindByName<ContentView>("leadingContentView").Top();
        MainGrid.FindByName<Label>("txtLabel").Bottom();
    }

    private async void OnAddClientClicked(object Sender, EventArgs Event)
    {
        await Shell.Current.GoToAsync($"{nameof(NewClientPage)}?BackButtonBehavior=True");
    }

    private async void OnListClientClicked(object Sender, EventArgs Event)
    {
        await Shell.Current.GoToAsync($"{nameof(ListClientPage)}?BackButtonBehavior=True");
    }

    private async void OnLoanRequestClicked(object Sender, EventArgs Event)
    {
        await Shell.Current.GoToAsync($"{nameof(LoanRequestPage)}?BackButtonBehavior=True");
    }
}
