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

    [RelayCommand]
    private void GenderSelectedValueChanged(PickerData? PickerItem)
    {
        Model.Gender = (Gender?)PickerItem?.Data;
    }

    [RelayCommand]
    private async Task RegisterReference()
    {
        Messenger.Send(new ValueMessage<CreateReference>(Model));
        await Popups.ShowSysCreditPopup("Referencia agregada correctamente", "Aceptar");
        Form?.Reset();
    }

    [RelayCommand]
    private async Task ResetReference(FormResetCommandParameter Parameter)
    {
        if (await Popups.ShowSysCreditPopup("¿Desea borrar el contenido de todos los campos?", "Sí", "No"))
        {
            Parameter.FormReset();
        }
    }
}
