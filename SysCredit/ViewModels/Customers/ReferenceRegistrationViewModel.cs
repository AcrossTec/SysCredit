namespace SysCredit.ViewModels.Customers;

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using Mopups.Services;

using SysCredit.Controls.Parameters;
using SysCredit.Extensions;
using SysCredit.Messages;
using SysCredit.Models.Customers.Creates;

using System.Threading.Tasks;

public partial class ReferenceRegistrationViewModel : ViewModelBase
{
    [ObservableProperty]
    private CreateReference m_Model = new();

    [RelayCommand]
    private async Task RegisterReference()
    {
        Messenger.Send(new ValueMessage<CreateReference>(Model));

        // TODO: Crear animación dentro del mismo BottomSheet

        if (await Popups.ShowSysCreditPopup("Referencia agregada correctamente.\n¿Agregar otra referencia?", "Si", "No"))
        {
            Model = new();
        }
        else
        {
            await MopupService.Instance.PopAsync();
        }
    }

    [RelayCommand]
    private async Task ResetReference(FormResetCommandParameter Parameter)
    {
        // TODO: Crear modal dentro del mismo BottomSheet

        await Application.Current!.GetCurrentPage().DisplaySnackbar(
             "¿Desea borrar el contenido de todos los campos?",
             Parameter.FormReset, "Si");
    }
}
