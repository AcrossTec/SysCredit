namespace SysCredit.ViewModels.Customers;

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
using SysCredit.Views.Guarantors;

public partial class CustomerRegistrationViewModel : BaseViewModel
{
    [RelayCommand(CanExecute = nameof(CanRegisterCustomer))]
    private async Task RegisterCustomer()
    {
        await Shell.Current.CurrentPage.DisplayAlert(CompanyName, "Cliente registrado correctamente", "Cerrar");
    }

    private bool CanRegisterCustomer()
    {
        return true;
    }

    [RelayCommand]
    private async Task GoToGuarantorSearchPage()
    {
        await Shell.Current.GoToAsync(nameof(GuarantorSearchPage));
    }
}

public partial class CustomerRegistrationViewModel
{
    public override void ApplyQueryAttributes(IDictionary<string, object> Query)
    {
        base.ApplyQueryAttributes(Query);
        SetBackButtonBehavior(LookupParam<bool>(nameof(BackButtonBehavior)));
    }

    private void SetBackButtonBehavior(bool IsEnabled)
    {
        if (IsEnabled is false)
        {
            Shell.SetBackButtonBehavior(Shell.GetBackButtonBehavior(Shell.Current.CurrentPage), default);
        }
    }
}
