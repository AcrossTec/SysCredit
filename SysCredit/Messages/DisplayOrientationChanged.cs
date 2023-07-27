namespace SysCredit.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class DisplayOrientationChanged : ValueChangedMessage<DisplayOrientation>
{
    public DisplayOrientationChanged(DisplayOrientation Value) : base(Value)
    {
    }
}
