namespace SysCredit.Mobile.ViewModels.Catalogs.LoanTypes;

using CommunityToolkit.Mvvm.Input;
using SysCredit.Mobile.Views.Catalogs.LoanTypes;

public partial class LoanTypesCrudPageViewModel : ViewModelBase
{
    public Func<Task> DisplayPrompt { get; set; } = null!;

    [RelayCommand]
    public async Task GoToFetch()
    {
        await Shell.Current.GoToAsync(nameof(ViewLoanTypeCatalogPage));
    }

    [RelayCommand]
    public async Task DisplayInsert()
    {
        await DisplayPrompt.Invoke();
    }

    [RelayCommand]
    public async Task GoToDelete()
    {
        await Shell.Current.GoToAsync(nameof(DeleteLoanTypePage));
    }

    [RelayCommand]
    public async Task GoToUpdate()
    {
        await Shell.Current.GoToAsync(nameof(UpdateLoanTypePage));
    }
}
