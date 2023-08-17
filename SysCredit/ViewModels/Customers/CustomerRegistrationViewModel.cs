namespace SysCredit.Mobile.ViewModels.Customers;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Maui;
using Microsoft.Maui.Controls;

using SysCredit.Enums;
using SysCredit.Mobile.Controls.Parameters;
using SysCredit.Mobile.Messages;
using SysCredit.Mobile.Models;
using SysCredit.Mobile.Models.Customers.Creates;
using SysCredit.Mobile.Views.Guarantors;

using System.Threading.Tasks;

using static Helpers.Parameters;

public partial class CustomerRegistrationViewModel : ViewModelBase, IRecipient<ValueMessage<CreateReference>>, IRecipient<ValueMessage<Guarantor>>
{
    public CustomerRegistrationViewModel()
    {
        Messenger.Register<ValueMessage<CreateReference>>(this);
        Messenger.Register<ValueMessage<Guarantor>>(this);
    }

    [ObservableProperty]
    private CreateCustomer m_Model = new();

    public void Receive(ValueMessage<CreateReference> Message)
    {
        Model.References.Add(Message.Value);
    }

    public void Receive(ValueMessage<Guarantor> Message)
    {
        Model.Guarantors.Add(Message.Value);
    }

    [RelayCommand]
    private void GenderSelectedValueChanged(PickerData? PickerItem)
    {
        Model.Gender = (Gender?)PickerItem?.Data;
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
        await Shell.Current.GoToAsync("///Customer/Reference/List", Key(nameof(Model.References)).Value(Model.References));
    }
}
