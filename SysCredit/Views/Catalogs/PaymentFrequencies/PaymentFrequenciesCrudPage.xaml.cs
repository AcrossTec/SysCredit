namespace SysCredit.Mobile.Views.Catalogs.PaymentFrequencies;

using SysCredit.Mobile.Properties;
using SysCredit.Mobile.ViewModels.Catalogs.PaymentFrequencies;

public partial class PaymentFrequenciesCrudPage : ContentPage
{
	public PaymentFrequenciesCrudPage(PaymentFrequenciesCrudPageViewModel ViewModel)
	{
		InitializeComponent();
        ViewModel.DisplayPrompt = DisplayInsert;
		BindingContext = ViewModel;
	}

    public async Task DisplayInsert()
    {
        var title = SysCreditResources.View_PaymentFrequenciesCrud_Insert;
        var message = SysCreditResources.View_Insert_Message;
        await DisplayPromptAsync(title, message);
    }
}