namespace SysCredit.Mobile.ViewModels.Customers;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using DynamicData.Binding;

using SysCredit.Mobile.Messages;
using SysCredit.Mobile.Models.Customers.Creates;

public partial class ReferenceListViewModel : ViewModelBase, IRecipient<ValueMessage<CreateReference>>
{
    public ReferenceListViewModel()
    {
        Messenger.Register<ValueMessage<CreateReference>>(this);
    }

    public void Receive(ValueMessage<CreateReference> Message)
    {
        if (References.FirstOrDefault(Reference => ReferenceEquals(Reference, Message.Value)) is null)
        {
            References.Add(Message.Value);
        }
    }

    [ObservableProperty]
    private IObservableCollection<CreateReference> m_References = new ObservableCollectionExtended<CreateReference>();

    protected override void ApplyQueryAttributes()
    {
        References = LookupParam<IObservableCollection<CreateReference>>(nameof(References), new ObservableCollectionExtended<CreateReference>())!;
    }
}
