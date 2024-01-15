namespace SysCredit.Mobile.ViewModels.Presentations;

using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;

public partial class Presentation3ViewModel : ViewModelBase
{
    [RelayCommand]
    private async Task GoToNextPresentation()
    {
        await Shell.Current.GoToAsync("///Presentations/SysCredit/Presentation4");
    }

    [RelayCommand]
    private async Task GoToLogin()
    {
        await Shell.Current.GoToAsync("///Login");
    }
}
