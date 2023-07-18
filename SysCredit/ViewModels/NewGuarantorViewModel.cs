namespace SysCredit.ViewModels;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Core.Views;
using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using SysCredit.Models;

public partial class NewGuarantorViewModel : BaseViewModel
{
    [RelayCommand(CanExecute = nameof(CanSaveGuarantor))]
    private async Task SaveGuarantor()
    {
        await Shell.Current.GoToAsync("..");
    }

    private bool CanSaveGuarantor()
    {
        return true;
    }
}
