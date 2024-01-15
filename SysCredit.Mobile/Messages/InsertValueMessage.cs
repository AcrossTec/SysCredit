namespace SysCredit.Mobile.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class InsertValueMessage<T>(T Value) : ValueChangedMessage<T>(Value);
