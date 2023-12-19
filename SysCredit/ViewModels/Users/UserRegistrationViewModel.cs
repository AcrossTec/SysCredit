namespace SysCredit.Mobile.ViewModels.Users;

using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class UserRegistrationViewModel : ViewModelBase
{
    [RelayCommand]
    private async Task GoToLogin()
    {
        await Shell.Current.GoToAsync("///Login");
    }

    [RelayCommand]
    private async Task GoToHome()
    {
        await Shell.Current.GoToAsync("///Home");
    }
}
