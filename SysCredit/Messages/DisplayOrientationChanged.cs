namespace SysCredit.Mobile.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class DisplayOrientationChanged(DisplayOrientation Value) : ValueChangedMessage<DisplayOrientation>(Value);
