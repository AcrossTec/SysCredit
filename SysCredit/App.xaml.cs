﻿namespace SysCredit.Mobile;

using CommunityToolkit.Mvvm.Messaging;

using SysCredit.Mobile.Messages;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }

    private static void OnRequestedThemeChanged(object? Sender, AppThemeChangedEventArgs Event)
    {
        WeakReferenceMessenger.Default.Send(new AppThemeChanged(Event.RequestedTheme));
    }
}
