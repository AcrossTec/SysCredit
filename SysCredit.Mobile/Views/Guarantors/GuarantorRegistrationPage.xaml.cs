namespace SysCredit.Mobile.Views.Guarantors;

using SysCredit.Mobile.ViewModels.Guarantors;

public partial class GuarantorRegistrationPage : ContentPage
{
    public GuarantorRegistrationPage(GuarantorRegistrationViewModel ViewModel)
    {
        InitializeComponent();

        ViewModel.Form = Form;
        Form.BindingContext = ViewModel;
    }
}
