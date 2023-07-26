namespace SysCredit.Services;

public interface INavigationService
{
    Task InitializeAsync();

    Task NavigateToAsync(string Route, IDictionary<string, object>? RouteParameters = null);

    Task PopAsync();
}
