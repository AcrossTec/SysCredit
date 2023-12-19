using SysCredit.Mobile.ViewModels.Users;

namespace SysCredit.Mobile.Views.Users;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel ViewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel;
    }
}