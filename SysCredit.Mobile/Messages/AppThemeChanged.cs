namespace SysCredit.Mobile.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class AppThemeChanged(AppTheme Value) : ValueChangedMessage<AppTheme>(Value);
