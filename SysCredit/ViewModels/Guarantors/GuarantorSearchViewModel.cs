namespace SysCredit.ViewModels.Guarantors;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using SysCredit.Views.Guarantors;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class GuarantorSearchViewModel : ViewModelBase
{
    [RelayCommand]
    private void PerformSearch(string Query)
    {
    }

    [RelayCommand]
    private async Task GoToGuarantorRegistrationPage()
    {
        await Shell.Current.GoToAsync(nameof(GuarantorRegistrationPage));
    }
}
