namespace SysCredit.Mobile.ViewModels.Users;

using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class LoginViewModel : ViewModelBase
{
    [RelayCommand]
    private async Task GoToHome()
    {
        await Shell.Current.GoToAsync("///Home");
    }

    [RelayCommand]
    private async Task GoToUserRegister()
    {
        await Shell.Current.GoToAsync("///UserRegistration");
    }

    [RelayCommand]
    private async Task GoToRecoverAccount()
    {
        await Shell.Current.GoToAsync("///RecoverAccount");
    }
}
