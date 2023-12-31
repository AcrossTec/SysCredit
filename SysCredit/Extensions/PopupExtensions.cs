﻿namespace SysCredit.Mobile;

using Microsoft.Extensions.Options;
using Microsoft.Maui.Controls;

using Mopups.Services;

using SysCredit.Mobile.Controls;
using SysCredit.Mobile.Extensions;

using System.Threading.Tasks;
using System.Windows.Input;

using UraniumUI;
using UraniumUI.Dialogs;

public static class Popups
{
    public static async Task<bool> ShowSysCreditPopup(string Text, string? OkText = null, string? CancelText = null, ICommand? OkCommand = null, object? OkCommandParameter = null, ICommand? CancelCommand = null, object? CancelCommandParameter = null)
    {
        var TaskSource = new TaskCompletionSource<bool>();

        var Popup = new SysCreditPopup
        {
            Text = Text,
            WidthRequest = Application.Current!.GetCurrentPage().Width,
            OkCommand = new Command(Parameter =>
            {
                OkCommand?.Execute(Parameter);
                TaskSource.TrySetResult(true);
                MopupService.Instance.PopAsync();
            }),
            OkCommandParameter = OkCommandParameter,
            CancelCommand = new Command(Parameter =>
            {
                CancelCommand?.Execute(Parameter);
                TaskSource.TrySetResult(false);
                MopupService.Instance.PopAsync();
            }),
            CancelCommandParameter = CancelCommandParameter,
            Parent = Application.Current!.GetCurrentPage(),
        };

        Popup.BackgroundClickedCommand = new Command(Parameter =>
        {
            if (Popup.IsCancelEnabled)
                CancelCommand?.Execute(CancelCommandParameter);
            else
                OkCommand?.Execute(OkCommandParameter);

            TaskSource.TrySetResult(true);
            MopupService.Instance.PopAsync();
        });

        if (OkText is not null)
        {
            Popup.OkText = OkText;
        }

        if (CancelText is not null)
        {
            Popup.IsCancelEnabled = true;
            Popup.CancelText = CancelText;
        }

        var Options = UraniumServiceProvider.Current.GetRequiredService<IOptions<DialogOptions>>()?.Value;

        foreach (var EffectFactory in Options?.Effects ?? Enumerable.Empty<Func<RoutingEffect>>())
        {
            Popup.ContentFrame.Effects.Add(EffectFactory());
        }

        await MopupService.Instance.PushAsync(Popup);
        return await TaskSource.Task;
    }
}
