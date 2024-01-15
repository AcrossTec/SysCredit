namespace SysCredit.Mobile.Views.Customers;

using SysCredit.Mobile.ViewModels.Customers;

public partial class ReferenceRegistrationPage : ContentPage
{
    public ReferenceRegistrationPage(ReferenceRegistrationViewModel ViewModel)
    {
        InitializeComponent();

        ViewModel.Form = Form;
        Form.BindingContext = ViewModel;
    }
}