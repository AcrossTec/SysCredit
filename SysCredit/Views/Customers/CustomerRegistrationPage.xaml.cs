namespace SysCredit.Mobile.Views.Customers;

using SysCredit.Mobile.ViewModels.Customers;

using UraniumUI.Pages;

public partial class CustomerRegistrationPage : UraniumContentPage
{
    public CustomerRegistrationPage(CustomerRegistrationViewModel ViewModel)
    {
        InitializeComponent();

        ViewModel.Form = Form;
        BindingContext = ViewModel;
    }
}
