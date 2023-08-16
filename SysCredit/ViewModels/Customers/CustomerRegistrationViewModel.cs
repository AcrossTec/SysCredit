namespace SysCredit.Mobile.ViewModels.Customers;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Maui;
using Microsoft.Maui.Controls;

using ReactiveUI;

using SysCredit.Mobile.Controls.Parameters;
using SysCredit.Mobile.Messages;
using SysCredit.Mobile.Models.Customers.Creates;
using SysCredit.Mobile.Views.Customers;
using SysCredit.Mobile.Views.Guarantors;

using System.Threading.Tasks;

public partial class CustomerRegistrationViewModel : ViewModelBase, IRecipient<ValueMessage<CreateReference>>, IRecipient<ValueMessage<CreateGuarantor>>
{
    public CustomerRegistrationViewModel()
    {
        Messenger.Register<ValueMessage<CreateReference>>(this);
        Messenger.Register<ValueMessage<CreateGuarantor>>(this);

        Model.References.CollectionChanged += delegate
        {
            OnPropertyChanged(nameof(Model));
        };

        Model.Guarantors.CollectionChanged += delegate
        {
            OnPropertyChanged(nameof(Model));
        };
    }

    [ObservableProperty]
    private CreateCustomer m_Model = new();

    public void Receive(ValueMessage<CreateReference> Message)
    {
        Model.References.Add(Message.Value);
    }

    public void Receive(ValueMessage<CreateGuarantor> Message)
    {
        Model.Guarantors.Add(Message.Value);
    }

    [RelayCommand]
    private async Task SubmitCustomer()
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
    private async Task ResetCustomer(FormResetCommandParameter Parameter)
    {
        if (await Popups.ShowSysCreditPopup("¿Desea borrar el contenido de todos los campos?", "Sí", "No"))
        {
            Parameter.FormReset();
            Model.References.Clear();
            Model.Guarantors.Clear();
        }
    }

    [RelayCommand]
    private async Task GoToGuarantorSearchPage()
    {
        await Shell.Current.GoToAsync(nameof(GuarantorSearchPage));
    }

    [RelayCommand]
    private void OpenSwipeView(SwipeView Swipe)
    {
        Swipe.Open(OpenSwipeItem.LeftItems);
    }

    [RelayCommand]
    private async Task OpenGuarantorSearchBottomSheet()
    {
        await Shell.Current.GoToAsync("///Customer/Guarantor/Search");
    }

    [RelayCommand]
    private async Task OpenGuarantorListBottomSheet()
    {
        await Shell.Current.GoToAsync("///Customer/Guarantor/List");
    }

    [RelayCommand]
    private async Task OpenGuarantorRegistrationBottomSheet()
    {
        await Shell.Current.GoToAsync("///Customer/Guarantor/Registration");
    }

    [RelayCommand]
    private async Task OpenReferenceRegistrationBottomSheet()
    {
        await Shell.Current.GoToAsync("///Customer/Reference/Registration");
    }

    [RelayCommand]
    private async Task OpenReferenceListBottomSheet()
    {
        await Shell.Current.GoToAsync("///Customer/Reference/List");
    }
}
