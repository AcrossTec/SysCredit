namespace SysCredit.Mobile.Views.Guarantors;

using SysCredit.Mobile.ViewModels.Guarantors;

public partial class GuarantorListPage : ContentPage
{
    public GuarantorListPage(GuarantorListViewModel ViewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel;
    }
}