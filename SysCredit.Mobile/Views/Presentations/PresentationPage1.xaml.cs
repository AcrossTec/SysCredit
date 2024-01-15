using SysCredit.Mobile.ViewModels.Presentations;

namespace SysCredit.Mobile.Views.Presentations;

public partial class PresentationPage1 : ContentPage
{
	public PresentationPage1(Presentation1ViewModel ViewModel)
	{
		InitializeComponent();
		BindingContext = ViewModel;
	}
}