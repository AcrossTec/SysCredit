namespace SysCredit.Mobile.ViewModels.Customers;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using SysCredit.Enums;
using SysCredit.Mobile.Controls;
using SysCredit.Mobile.Controls.Parameters;
using SysCredit.Mobile.Messages;
using SysCredit.Mobile.Models;
using SysCredit.Mobile.Models.Customers.Creates;

using System.Threading.Tasks;

public partial class ReferenceRegistrationViewModel : ViewModelBase
{
    public FormView? Form { get; set; }

    [ObservableProperty]
    private CreateReference m_Model = new();

    private void ModelReset()
    {
        Model = new();
        Form?.BaseReset();
    }

    [RelayCommand]
    private void OnGenderSelectedValueChanged(PickerData? PickerItem)
    {
        Model.Gender = (Gender?)PickerItem?.Data;
    }

    [RelayCommand]
    private async Task OnRegisterReference()
    {
        Messenger.Send(new InsertValueMessage<CreateReference>(Model));
        await Popups.ShowSysCreditPopup("Referencia Agregada Correctamente");
        ModelReset();
    }

    [RelayCommand]
    private async Task OnResetReference(FormResetCommandParameter Parameter)
    {
        if (await Popups.ShowSysCreditPopup("¿Limpiar Formulario?", "Sí", "No"))
        {
            ModelReset();
        }
    }
}
