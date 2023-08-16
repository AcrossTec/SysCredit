namespace SysCredit.Mobile.Services;

using SysCredit.Mobile.Services.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MauiNavigationService : INavigationService
{
    private readonly ISettingsService SettingsService;

    public MauiNavigationService(ISettingsService SettingsService)
    {
        this.SettingsService = SettingsService;
    }

    public Task InitializeAsync() =>
        NavigateToAsync(
            string.IsNullOrEmpty(SettingsService.AuthAccessToken)
                ? "///Login"
                : "///Home");

    public Task NavigateToAsync(string Route, IDictionary<string, object>? RouteParameters = null)
    {
        return RouteParameters is null
            ? Shell.Current.GoToAsync(Route)
            : Shell.Current.GoToAsync(Route, RouteParameters);
    }

    public Task PopAsync() =>
        Shell.Current.GoToAsync("..");
}
