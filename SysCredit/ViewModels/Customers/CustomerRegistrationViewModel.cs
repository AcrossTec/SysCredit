namespace SysCredit.Mobile.ViewModels.Customers;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using DynamicData.Binding;

using Microsoft.Maui;
using Microsoft.Maui.Controls;

using SysCredit.Enums;
using SysCredit.Helpers.Delegates;
using SysCredit.Mobile.Controls.Parameters;
using SysCredit.Mobile.Messages;
using SysCredit.Mobile.Models;
using SysCredit.Mobile.Models.Customers.Creates;
using SysCredit.Mobile.Views.Guarantors;

using System.Threading.Tasks;

public partial class CustomerRegistrationViewModel
    : ViewModelBase
    , IRecipient<InsertValueMessage<CreateReference>>
    , IRecipient<InsertValueMessage<Guarantor>>
    , IRecipient<DeleteValueMessage<CreateReference>>
    , IRecipient<DeleteValueMessage<Guarantor>>
    , IRecipient<ActionMessage<Fetch<IObservableCollection<CreateReference>>>>
    , IRecipient<ActionMessage<Fetch<IObservableCollection<Guarantor>>>>
{
    public CustomerRegistrationViewModel()
    {
        Initialize();
        Messenger.Register<InsertValueMessage<Guarantor>>(this);
        Messenger.Register<InsertValueMessage<CreateReference>>(this);
        Messenger.Register<DeleteValueMessage<CreateReference>>(this);
        Messenger.Register<ActionMessage<Fetch<IObservableCollection<CreateReference>>>>(this);
        Messenger.Register<ActionMessage<Fetch<IObservableCollection<Guarantor>>>>(this);
    }

    protected virtual void Initialize()
    {
    }

    [ObservableProperty]
    private CreateCustomer m_Model = new();

    public void Receive(InsertValueMessage<CreateReference> Message)
    {
        Model.References.Add(Message.Value);
    }

    public void Receive(DeleteValueMessage<CreateReference> Message)
    {
        Model.References.Remove(Message.Value);
    }

    public void Receive(InsertValueMessage<Guarantor> Message)
    {
        Model.Guarantors.Add(Message.Value);
    }

    public void Receive(DeleteValueMessage<Guarantor> Message)
    {
        Model.Guarantors.Remove(Message.Value);
    }

    public void Receive(ActionMessage<Fetch<IObservableCollection<CreateReference>>> Message)
    {
        Message.Value.Invoke(Model.References);
    }

    public void Receive(ActionMessage<Fetch<IObservableCollection<Guarantor>>> Message)
    {
        Message.Value.Invoke(Model.Guarantors);
    }

    [RelayCommand]
    private void GenderSelectedValueChanged(PickerData? PickerItem)
    {
        Model.Gender = (Gender?)PickerItem?.Data;
    }

    [RelayCommand]
    private async Task OnSubmitCustomer()
    {
        if (Model.IsValid)
        {
            if (!await Popups.ShowSysCreditPopup("Cliente registrado correctamente.\n¿Registrar otro cliente?", "Si", "No"))
            {
                await Shell.Current.GoToAsync("///Home");
            }
        }
    }

    [RelayCommand]
    private async Task OnResetCustomer(FormResetCommandParameter Parameter)
    {
        if (await Popups.ShowSysCreditPopup("¿Desea borrar el contenido de todos los campos?", "Sí", "No"))
        {
            Parameter.FormReset();
            Model.References.Clear();
            Model.Guarantors.Clear();
        }
    }

    [RelayCommand]
    private void OpenSwipeView(SwipeView Swipe)
    {
        Swipe.Open(OpenSwipeItem.LeftItems);
    }

    [RelayCommand]
    private async Task GoToGuarantorSearchPage()
    {
        await Shell.Current.GoToAsync(nameof(GuarantorSearchPage));
    }

    [RelayCommand]
    private async Task OpenGuarantorSearchPage()
    {
        await Shell.Current.GoToAsync("///Customer/Guarantor/Search");
    }

    [RelayCommand]
    private async Task OpenGuarantorListPage()
    {
        await Shell.Current.GoToAsync("///Customer/Guarantor/List");
    }

    [RelayCommand]
    private async Task OpenGuarantorRegistrationPage()
    {
        await Shell.Current.GoToAsync("///Customer/Guarantor/Registration");
    }

    [RelayCommand]
    private async Task OpenReferenceRegistrationPage()
    {
        await Shell.Current.GoToAsync("///Customer/Reference/Registration");
    }

    [RelayCommand]
    private async Task OpenReferenceListPage()
    {
        await Shell.Current.GoToAsync("///Customer/Reference/List");
    }
}
