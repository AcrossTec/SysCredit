using SysCredit.Mobile.ViewModels.Users;

namespace SysCredit.Mobile.Views.Users;

public partial class UserRegistrationPage : ContentPage
{
    public UserRegistrationPage(UserRegistrationViewModel ViewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel;
    }
}