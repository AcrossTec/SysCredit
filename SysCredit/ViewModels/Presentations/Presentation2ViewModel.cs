namespace SysCredit.Mobile.ViewModels.Presentations;

using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class Presentation2ViewModel : ViewModelBase
{
    [RelayCommand]
    private async Task GoToPresentationPage3()
    {
        await Shell.Current.GoToAsync("///Presentations/SysCredit/Presentation3");
    }

    [RelayCommand]
    private async Task GoToLogin()
    {
        await Shell.Current.GoToAsync("///Login");
    }
}
