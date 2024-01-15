namespace SysCredit.Mobile.Views.Guarantors;

using SysCredit.Mobile.ViewModels.Guarantors;

public partial class GuarantorListPage : ContentPage
{
    private readonly GuarantorListViewModel ViewModel;

    public GuarantorListPage(GuarantorListViewModel ViewModel)
    {
        InitializeComponent();
        BindingContext = this.ViewModel = ViewModel;
    }

    private void OnPageAppearing(object Sender, EventArgs EventInfo)
    {
        ViewModel.AppearingCommand.Execute(EventInfo);
    }
}