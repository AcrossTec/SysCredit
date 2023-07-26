namespace SysCredit.Views;

using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Messaging;

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
        WeakReferenceMessenger.Default.Register<AppThemeChanged>(this, OnAppThemeChanged);
        WeakReferenceMessenger.Default.Register<DisplayOrientationChanged>(this, OnDisplayOrientationChanged);
    }

    private void OnAppThemeChanged(object Recipient, AppThemeChanged Message)
    {
    }

    private void OnDisplayOrientationChanged(object Recipient, DisplayOrientationChanged Message)
    {
    }
}
