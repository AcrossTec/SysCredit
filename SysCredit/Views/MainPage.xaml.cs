namespace SysCredit.Views;

using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Messaging;

using Maui.FreakyControls;

using Microsoft.Maui;
using Microsoft.Maui.Controls;

using SysCredit.Messages;
using SysCredit.Views.Customers;
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

        WeakReferenceMessenger.Default.Register<AppThemeChanged>(this, OnAppThemeChanged);
        WeakReferenceMessenger.Default.Register<DisplayOrientationChanged>(this, OnDisplayOrientationChanged);
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

    private void OnAppThemeChanged(object Recipient, AppThemeChanged Message)
    {
    }

    private void OnDisplayOrientationChanged(object Recipient, DisplayOrientationChanged Message)
    {
    }
}
