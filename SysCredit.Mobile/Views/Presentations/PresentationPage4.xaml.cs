namespace SysCredit.Mobile.Views.Presentations;

using SysCredit.Mobile.ViewModels.Presentations;

public partial class PresentationPage4 : ContentPage
{
	public PresentationPage4(Presentation4ViewModel ViewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel;
	}
}