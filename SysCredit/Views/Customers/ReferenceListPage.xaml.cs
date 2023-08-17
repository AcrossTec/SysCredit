namespace SysCredit.Mobile.Views.Customers;

using SysCredit.Mobile.ViewModels.Customers;

public partial class ReferenceListPage : ContentPage
{
    public ReferenceListPage(ReferenceListViewModel ViewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel;
    }
}
