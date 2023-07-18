namespace SysCredit.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using SysCredit.Views.Clients;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class SearchGuarantorViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<string> _GuarantorsFound = new ObservableCollection<string>();

    [RelayCommand]
    private async Task GoToNewGuarantorView()
    {
        await Shell.Current.GoToAsync("GuarantorNew");
    }
}
