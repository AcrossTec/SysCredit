using SysCredit.Mobile.Properties;
using SysCredit.Mobile.ViewModels.Catalogs.LoanTypes;

namespace SysCredit.Mobile.Views.Catalogs.LoanTypes;

public partial class UpdateLoanTypePage : ContentPage
{
	public UpdateLoanTypePage(UpdateLoanTypePageViewModel ViewModel)
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var title = ((Button)sender).Text;
        var message = SysCreditResources.View_Enter_Name;
        await DisplayPromptAsync(title, message);
    }
}