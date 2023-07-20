namespace SysCredit.ViewModels.Guarantors;

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

using SysCredit.Controls;
using SysCredit.Models;

public partial class GuarantorRegistrationViewModel : BaseViewModel
{
    [RelayCommand(CanExecute = nameof(CanRegisterGuarantor))]
    private async Task RegisterGuarantor()
    {
        if (!await Popups.ShowSysCreditPopup("¿Registrar otro fiador?", "Si", "No"))
        {
            await Shell.Current.GoToAsync("..");
        }
    }

    private bool CanRegisterGuarantor()
    {
        return true;
    }
}
