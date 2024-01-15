namespace SysCredit.Mobile.ViewModels.Catalogs;

using CommunityToolkit.Mvvm.Input;
using SysCredit.Mobile.Views.Catalogs.LoanTypes;
using SysCredit.Mobile.Views.Catalogs.PaymentFrequencies;
using SysCredit.Mobile.Views.Catalogs.Relationships;

public partial class CatalogsPageViewModel : ViewModelBase
{
    [RelayCommand]
    private async Task GoToPaymentFrequency()
    {
        await Shell.Current.GoToAsync(nameof(PaymentFrequenciesCrudPage));
    }

    [RelayCommand]
    private async Task GoToLoanType()
    {
        await Shell.Current.GoToAsync(nameof(LoanTypesCrudPage));
    }

    [RelayCommand]
    private async Task GoToRelationship()
    {
        await Shell.Current.GoToAsync(nameof(RelationshipsCrudPage));
    }
}
