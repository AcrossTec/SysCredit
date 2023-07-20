namespace SysCredit;

using CommunityToolkit.Maui.Views;

using SysCredit.Controls;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

public static class Popups
{
    public static async Task<bool> ShowSysCreditPopup(string Text, string? OkText = null, string? CancelText = null, ICommand? OkCommand = null, object? OkCommandParameter = null, ICommand? CancelCommand = null, object? CancelCommandParameter = null)
    {
        var Popup = new SysCreditPopup
        {
            Text = Text,
            OkCommand = OkCommand,
            OkCommandParameter = OkCommandParameter,
            CancelCommand = CancelCommand,
            CancelCommandParameter = CancelCommandParameter
        };

        if (OkText is not null)
        {
            Popup.OkText = OkText;
        }

        if (CancelText is not null)
        {
            Popup.IsCancelEnabled = true;
            Popup.CancelText = CancelText;
        }

        var Result = await Shell.Current.ShowPopupAsync(Popup);
        return (bool)Result!;
    }
}
