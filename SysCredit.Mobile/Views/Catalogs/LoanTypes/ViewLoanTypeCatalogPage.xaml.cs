namespace SysCredit.Mobile.Views.Catalogs.LoanTypes;

using SysCredit.Mobile.ViewModels.Catalogs.LoanTypes;

public partial class ViewLoanTypeCatalogPage : ContentPage
{	
    public ViewLoanTypeCatalogPage(ViewLoanTypeCatalogPageViewModel ViewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel;
	}
}