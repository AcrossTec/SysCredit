namespace SysCredit.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class ValueMessage<T> : ValueChangedMessage<T>
{
    public ValueMessage(T Value) : base(Value)
    {
    }
}
