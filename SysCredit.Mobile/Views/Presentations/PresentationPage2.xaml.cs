using SysCredit.Mobile.ViewModels.Presentations;

namespace SysCredit.Mobile.Views.Presentations;

public partial class PresentationPage2 : ContentPage
{
	public PresentationPage2(Presentation2ViewModel ViewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel;
	}
}