
namespace SysCredit.ViewModels.Customers;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using InputKit.Shared.Controls;

using SysCredit.Models.Customers.Creates;

public partial class GuarantorRegistrationBottomSheetViewModel : ViewModelBase
{
    public FormView? Form { get; set; }

    [ObservableProperty]
    private CreateGuarantor m_Model = new();

    [RelayCommand]
    private async Task RegisterGuarantor()
    {
        await Task.CompletedTask;
    }
}
