namespace SysCredit.Mobile.ViewModels.Presentations;

using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public partial class Presentation1ViewModel : ViewModelBase
{
    [RelayCommand]
    private async Task GoToPresentationPage2()
    {
        await Shell.Current.GoToAsync("///Presentations/SysCredit/Presentation2");
    }

    [RelayCommand]
    private async Task GoToLogin()
    {
        await Shell.Current.GoToAsync("///Login");
    }
}
