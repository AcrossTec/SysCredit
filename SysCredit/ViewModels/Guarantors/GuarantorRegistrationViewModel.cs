namespace SysCredit.Mobile.ViewModels.Guarantors;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using DynamicData.Binding;
using DynamicData.Kernel;

using SysCredit.Enums;
using SysCredit.Mobile.Controls;
using SysCredit.Mobile.Controls.Dialogs;
using SysCredit.Mobile.Messages;
using SysCredit.Mobile.Models;
using SysCredit.Mobile.Models.Customers.Creates;
using SysCredit.Mobile.Services.Https;

using System.Threading.Tasks;

public partial class GuarantorRegistrationViewModel : ViewModelBase
{
    private readonly ISysCreditApiService SysCreditApi;

    public GuarantorRegistrationViewModel(ISysCreditApiService SysCreditApi)
    {
        this.SysCreditApi = SysCreditApi;
        this.LoadDataAsync();
    }

    public FormView? Form { get; set; }

    [ObservableProperty]
    private IObservableCollection<Relationship>? m_Relationships;

    [ObservableProperty]
    private CreateGuarantor m_Model = new();

    [RelayCommand]
    private void GenderSelectedValueChanged(PickerData? PickerItem)
    {
        Model.Gender = (Gender?)PickerItem?.Data;
    }

    [RelayCommand]
    private async Task RegisterGuarantor()
    {
        UserDialogs.ShowLoading("Registrando Fiador");
        var EntityId = await SysCreditApi.InsertGuarantorAsync(Model);
        UserDialogs.HideHud();

        if (EntityId is not null)
        {
            Messenger.Send(new InsertValueMessage<Guarantor>(new Guarantor
            {
                GuarantorId = EntityId.Id.ValueOr(0),
                Identification = Model.Identification,
                LastName = Model.LastName,
                Address = Model.Address,
                Gender = Model.Gender.GetValueOrDefault(),
                Email = Model.Email,
                Phone = Model.Phone,
                Name = Model.Name,
                Neighborhood = Model.Neighborhood,
                Relationship = Model.Relationship!,
                BussinessType = Model.BussinessType,
                BussinessAddress = Model.BussinessAddress,
            }));

            await Popups.ShowSysCreditPopup("Fiador registrado correctamente");
            Form?.Reset();
        }
        else
        {
            await Popups.ShowSysCreditPopup("Error al registrar el fiador");
        }
    }

    private async void LoadDataAsync()
    {
        UserDialogs.ShowLoading("Cargando");
        Relationships = await SysCreditApi.FetchRelationshipsAsync();
        UserDialogs.HideHud();
    }
}
