namespace SysCredit.Mobile.Views.Catalogs.PaymentFrequencies;

using SysCredit.Mobile.Properties;

public partial class DeletePaymentFrequencyPage : ContentPage
{
	public DeletePaymentFrequencyPage()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var title = SysCreditResources.View_Delete_Alert;
        var message = ((Button)sender).Text;
        var confirm = SysCreditResources.View_Confirm_Message;
        var cancel = SysCreditResources.View_Cancel_Message;
        await DisplayAlert(title, message, confirm, cancel);
    }
}