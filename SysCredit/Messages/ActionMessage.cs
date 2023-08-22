namespace SysCredit.Mobile.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class ActionMessage<T>(T Value) : ValueChangedMessage<T>(Value) where T : Delegate;
