namespace SysCredit.ViewModels;

using CommunityToolkit.Mvvm.Input;

using SysCredit.Helpers;
using SysCredit.Views.Customers;
using SysCredit.Views.Loans;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class MainViewModel : ViewModelBase
{
    [RelayCommand]
    private async Task GoToCustomerRegistrationPage()
    {
        await Shell.Current.GoToAsync(nameof(CustomerRegistrationPage), Parameters.Key<BackButtonBehavior>().Value(true));
    }

    [RelayCommand]
    private async Task GoToCustomerListPage()
    {
        await Shell.Current.GoToAsync(nameof(CustomerListPage), Parameters.Key<BackButtonBehavior>().Value(true));
    }

    [RelayCommand]
    private async Task GoToLoanRequestPage()
    {
        await Shell.Current.GoToAsync(nameof(LoanRequestPage), Parameters.Key<BackButtonBehavior>().Value(true));
    }
}
