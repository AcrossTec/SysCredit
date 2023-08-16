namespace SysCredit.Mobile.ViewModels.Customers;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using InputKit.Shared.Controls;

using SysCredit.Mobile.Models.Customers.Creates;

public partial class GuarantorRegistrationViewModel : ViewModelBase
{
    [ObservableProperty]
    private CreateGuarantor m_Model = new();

    [RelayCommand]
    private async Task RegisterGuarantor()
    {
        await Task.CompletedTask;
    }
}
