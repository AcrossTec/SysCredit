using SysCredit.Mobile.ViewModels.Users;

namespace SysCredit.Mobile.Views.Users;

public partial class RecoverAccountPage : ContentPage
{
    public RecoverAccountPage(RecoverAccountViewModel ViewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel;
    }
}