namespace SysCredit.Mobile.ViewModels;

using CommunityToolkit.Mvvm.Input;

using SysCredit.Mobile.Views.Customers;
using SysCredit.Mobile.Views.Loans;
using SysCredit.Mobile.Views.Catalogs;

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
        await Shell.Current.GoToAsync("///Customer");
    }

    [RelayCommand]
    private async Task GoToCustomerListPage()
    {
        await Shell.Current.GoToAsync("///CustomerList");
    }

    [RelayCommand]
    private async Task GoToLoanRequestPage()
    {
        await Shell.Current.GoToAsync("///LoanRequest");
    }

    [RelayCommand]
    private async Task GoToReportPage()
    {
        await Shell.Current.GoToAsync("///Report");
    }

    [RelayCommand]
    private async Task GoToRoutePage()
    {
        await Shell.Current.GoToAsync("///Route");
    }

    [RelayCommand]
    private async Task GoToCatalogsPage()
    {
        await Shell.Current.GoToAsync(nameof(CatalogsPage));
    }
}
