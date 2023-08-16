namespace SysCredit.Mobile.Extensions;

public static class PageExtensions
{
    public static Page GetCurrentPage(this Application App)
    {
        return App.MainPage switch
        {
            Shell Shell => Shell.CurrentPage,
            NavigationPage Nav => Nav.CurrentPage,
            TabbedPage Tabbed => Tabbed.CurrentPage,
            _ => Application.Current!.MainPage!
        };
    }
}
