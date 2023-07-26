namespace SysCredit.ViewModels.Customers;

using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.Input;

using SysCredit.Models;
using SysCredit.Models.Customers.Creates;
using SysCredit.Views.Customers;
using SysCredit.Views.Guarantors;

using The49.Maui.BottomSheet;

public partial class CustomerRegistrationViewModel : ViewModelBase
{
    public CustomerRegistrationViewModel()
    {
        Model = new();
        Model.PropertyChanged += OnModelPropertyChanged;
    }

    private void OnModelPropertyChanged(object? Sender, PropertyChangedEventArgs Event)
    {
        OnPropertyChanged(nameof(Model));
        RegisterCustomerCommand.NotifyCanExecuteChanged();
    }

    public CreateCustomer Model { get; }

    [RelayCommand(CanExecute = nameof(CanRegisterCustomer))]
    private async Task RegisterCustomer()
    {
        await Popups.ShowSysCreditPopup("Cliente registrado correctamente");
        await Shell.Current.GoToAsync("///Home");
    }

    private bool CanRegisterCustomer() => Model.IsValid;

    [RelayCommand]
    private async Task GoToGuarantorSearchPage()
    {
        await Shell.Current.GoToAsync(nameof(GuarantorSearchPage));
    }

    [RelayCommand]
    private void OpenSwipeView(SwipeView Swipe)
    {
        Swipe.Open(OpenSwipeItem.LeftItems);
    }

    [RelayCommand]
    private async Task OpenSearchReferenceBottomSheet()
    {
        var Sheet = new GuarantorSearchBottomSheet();
        await Sheet.ShowAsync(Shell.Current.Window);
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
