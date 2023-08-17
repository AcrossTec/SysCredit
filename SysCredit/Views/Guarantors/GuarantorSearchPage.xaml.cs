namespace SysCredit.Mobile.Views.Guarantors;

using SysCredit.Mobile.ViewModels.Guarantors;

public partial class GuarantorSearchPage : ContentPage
{
    public GuarantorSearchPage(GuarantorSearchViewModel ViewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel;
    }
}