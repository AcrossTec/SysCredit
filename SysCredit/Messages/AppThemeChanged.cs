namespace SysCredit.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class AppThemeChanged : ValueChangedMessage<AppTheme>
{
    public AppThemeChanged(AppTheme Value) : base(Value)
    {
    }
}
