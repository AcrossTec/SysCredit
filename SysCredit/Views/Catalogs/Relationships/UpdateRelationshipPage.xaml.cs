namespace SysCredit.Mobile.Views.Catalogs.Relationships;

using SysCredit.Mobile.Properties;

public partial class UpdateRelationshipPage : ContentPage
{
	public UpdateRelationshipPage()
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