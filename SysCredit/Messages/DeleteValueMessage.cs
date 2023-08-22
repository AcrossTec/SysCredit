namespace SysCredit.Mobile.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class DeleteValueMessage<T>(T Value) : ValueChangedMessage<T>(Value);
