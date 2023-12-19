using SysCredit.Mobile.ViewModels.Presentations;

namespace SysCredit.Mobile.Views.Presentations;

public partial class PresentationPage3 : ContentPage
{
	public PresentationPage3(Presentation3ViewModel ViewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel;
	}
}