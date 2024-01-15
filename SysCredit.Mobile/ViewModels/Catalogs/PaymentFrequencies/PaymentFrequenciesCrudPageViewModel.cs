namespace SysCredit.Mobile.ViewModels.Catalogs.PaymentFrequencies;

using CommunityToolkit.Mvvm.Input;
using SysCredit.Mobile.Views.Catalogs.PaymentFrequencies;

public partial class PaymentFrequenciesCrudPageViewModel : ViewModelBase
{
    public Func<Task> DisplayPrompt { get; set; } = null!;

    [RelayCommand]
    public async Task GoToFetch()
    {
        await Shell.Current.GoToAsync(nameof(ViewPaymentFrequencyCatalogPage));
    }

    [RelayCommand]
    public async Task DisplayInsert()
    {
        await DisplayPrompt.Invoke();
    }

    [RelayCommand]
    public async Task GoToDelete()
    {
        await Shell.Current.GoToAsync(nameof(DeletePaymentFrequencyPage));
    }

    [RelayCommand]
    public async Task GoToUpdate()
    {
        await Shell.Current.GoToAsync(nameof(UpdatePaymentFrequencyPage));
    }
}
