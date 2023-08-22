namespace SysCredit.Mobile.Messages;

using CommunityToolkit.Mvvm.Messaging.Messages;

public class EditValueMessage<T>(T Value) : ValueChangedMessage<T>(Value);
