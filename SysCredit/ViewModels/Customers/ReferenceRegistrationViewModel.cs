namespace SysCredit.Mobile.ViewModels.Customers;

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Mopups.Services;

using SysCredit.Mobile.Controls.Parameters;
using SysCredit.Mobile.Extensions;
using SysCredit.Mobile.Messages;
using SysCredit.Mobile.Models.Customers.Creates;

using System.Threading.Tasks;

public partial class ReferenceRegistrationViewModel : ViewModelBase
{
    [ObservableProperty]
    private CreateReference m_Model = new();

    [RelayCommand]
    private async Task RegisterReference()
    {
        Messenger.Send(new ValueMessage<CreateReference>(Model));
        await Popups.ShowSysCreditPopup("Referencia agregada correctamente", "Aceptar");
        Model = new();
    }

    [RelayCommand]
    private async Task ResetReference(FormResetCommandParameter Parameter)
    {
        await Popups.ShowSysCreditPopup("¿Desea borrar el contenido de todos los campos?", "Aceptar", "Cancelar");
    }
}
