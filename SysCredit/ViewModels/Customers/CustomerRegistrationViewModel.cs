namespace SysCredit.ViewModels.Customers;

using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

using Microsoft.Maui.Controls;

using SysCredit.Extensions;
using SysCredit.Messages;
using SysCredit.Models;
using SysCredit.Models.Customers.Creates;
using SysCredit.Views.Customers;
using SysCredit.Views.Guarantors;

using The49.Maui.BottomSheet;

public partial class CustomerRegistrationViewModel : ViewModelBase
{
    public CustomerRegistrationViewModel()
    {
        WeakReferenceMessenger.Default.Register<ValueMessage<CreateReference>>(this, OnPropertyChangedMessage);
    }

    public CreateCustomer Model { get; } = new();

    private void OnPropertyChangedMessage(object Recipient, ValueMessage<CreateReference> Message)
    {
        Model.References.Add(Message.Value);
    }

    [RelayCommand]
    private async Task RegisterCustomer()
    {
        await Popups.ShowSysCreditPopup("Cliente registrado correctamente");
        await Shell.Current.GoToAsync("///Home");
    }

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
    private async Task OpenGuarantorSearchBottomSheet()
    {
        var Sheet = new GuarantorSearchBottomSheet();
        await Sheet.ShowAsync(Shell.Current.Window);
    }

    [RelayCommand]
    private async Task OpenGuarantorListBottomSheet()
    {
        var Sheet = new GuarantorListBottomSheet { BindingContext = Model.Guarantors };
        await Sheet.ShowAsync(Shell.Current.Window);
    }

    [RelayCommand]
    private async Task OpenReferenceRegistrationBottomSheet()
    {
        var Sheet = new ReferenceRegistrationBottomSheet();
        Sheet.BindingContext.Property("Form", Sheet.FindByName("Form"));
        await Sheet.ShowAsync(Shell.Current.Window);
    }

    [RelayCommand]
    private async Task OpenReferenceListBottomSheet()
    {
        var Sheet = new ReferenceListBottomSheet { BindingContext = Model.References };
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
