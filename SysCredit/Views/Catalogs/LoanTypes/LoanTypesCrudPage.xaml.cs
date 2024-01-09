namespace SysCredit.Mobile.Views.Catalogs.LoanTypes;

using SysCredit.Mobile.ViewModels.Catalogs.LoanTypes;
using SysCredit.Mobile.Properties;

public partial class LoanTypesCrudPage : ContentPage
{
	public LoanTypesCrudPage(LoanTypesCrudPageViewModel ViewModel)
	{
		InitializeComponent();
		ViewModel.DisplayPrompt = DisplayInsert;
		BindingContext = ViewModel;
	}

	public async Task DisplayInsert()
	{
		var title = SysCreditResources.View_LoanTypesCurd_Insert;
		var message = SysCreditResources.View_Insert_Message;
		await DisplayPromptAsync(title, message);
	}
}