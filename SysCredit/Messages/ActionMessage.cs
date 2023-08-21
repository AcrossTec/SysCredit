namespace SysCredit.Mobile.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class ActionMessage<T> : ValueChangedMessage<T>
{
    public ActionMessage(T Value) : base(Value)
    {
    }
}
