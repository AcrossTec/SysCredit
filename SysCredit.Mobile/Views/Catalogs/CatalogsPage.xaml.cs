namespace SysCredit.Mobile.Views.Catalogs;

using SysCredit.Mobile.ViewModels.Catalogs;

public partial class CatalogsPage : ContentPage
{
    public CatalogsPage(CatalogsPageViewModel ViewModel)
    {
        InitializeComponent();
        BindingContext = ViewModel;

        ItemsLayout.MaxColumns = 3;
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (width > height)
        {
            ItemsLayout.MaxColumns = 3;
        }
        else
        {
            ItemsLayout.MaxColumns = 5;
        }
    }
}