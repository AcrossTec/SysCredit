namespace SysCredit.ViewModels.Customers;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Microsoft.Maui;
using Microsoft.Maui.Controls;

using ReactiveUI;

using SysCredit.Controls.Parameters;
using SysCredit.Messages;
using SysCredit.Models.Customers.Creates;
using SysCredit.Views.Customers;
using SysCredit.Views.Guarantors;

using System.Threading.Tasks;

public partial class CustomerRegistrationViewModel : ViewModelBase, IRecipient<ValueMessage<CreateReference>>
{
    public CustomerRegistrationViewModel()
    {
        Messenger.Register(this);
        Model.ObservableForProperty(M => M.References).Subscribe(_ =>
        {
            OnPropertyChanged(nameof(Model));
        });
    }

    [ObservableProperty]
    private CreateCustomer m_Model = new();

    public void Receive(ValueMessage<CreateReference> Message)
    {
        Model.References.Add(Message.Value);
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
        // var Sheet = new GuarantorSearchBottomSheet();
        // await Sheet.ShowAsync(Shell.Current.Window);
    }

    [RelayCommand]
    private async Task OpenGuarantorListBottomSheet()
    {
        // var Sheet = new GuarantorListBottomSheet { BindingContext = Model.Guarantors };
        // await Sheet.ShowAsync(Shell.Current.Window);
    }

    [RelayCommand]
    private async Task OpenGuarantorRegistrationBottomSheet()
    {
        // await Shell.Current.GoToAsync(nameof(GuarantorRegistrationPage));
    }

    [RelayCommand]
    private async Task OpenReferenceRegistrationBottomSheet()
    {
        // var Sheet = new ReferenceRegistrationBottomSheet();
        // await Sheet.ShowAsync(Shell.Current.Window);
    }

    [RelayCommand]
    private async Task OpenReferenceListBottomSheet()
    {
        // var Sheet = new ReferenceListBottomSheet { BindingContext = Model.References };
        // await Sheet.ShowAsync(Shell.Current.Window);
    }
}
