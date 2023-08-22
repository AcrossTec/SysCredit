namespace SysCredit.Mobile.Views.Customers;

using SysCredit.Mobile.ViewModels.Customers;

public partial class ReferenceListPage : ContentPage
{
    private readonly ReferenceListViewModel ViewModel;

    public ReferenceListPage(ReferenceListViewModel ViewModel)
    {
        InitializeComponent();
        BindingContext = this.ViewModel = ViewModel;
    }

    private void OnPageAppearing(object Sender, EventArgs EventInfo)
    {
        ViewModel.AppearingCommand.Execute(EventInfo);
    }
}
