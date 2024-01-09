using SysCredit.Mobile.Properties;
using SysCredit.Mobile.ViewModels.Catalogs.Relationships;

namespace SysCredit.Mobile.Views.Catalogs.Relationships;

public partial class RelationshipsCrudPage : ContentPage
{
	public RelationshipsCrudPage(RelationshipsCrudPageViewModel ViewModel)
	{
		InitializeComponent();
        ViewModel.DisplayPrompt = DisplayInsert;
		BindingContext = ViewModel;
	}

    public async Task DisplayInsert()
    {
        var title = SysCreditResources.View_RelationshipsCrud_Insert;
        var message = SysCreditResources.View_Insert_Message;
        await DisplayPromptAsync(title, message);
    }
}