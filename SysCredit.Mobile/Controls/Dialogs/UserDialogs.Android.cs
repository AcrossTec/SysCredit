namespace SysCredit.Mobile.Controls.Dialogs;

using static global::Controls.UserDialogs.Maui.UserDialogs;

public static partial class UserDialogs
{
    public static partial void ShowLoading(string Message)
    {
        Instance.ShowLoading(Message);
    }

    public static partial void HideHud()
    {
        Instance.HideHud();
    }
}
