namespace SysCredit.Mobile.Services.Settings;

using SysCredit.Mobile.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SettingsService : ISettingsService
{
    #region Setting Constants

    private const string AccessToken = "access_token";
    private const string TokenId = "token_id";
    private const string UseMocksId = "use_mocks";
    private const string IdentityBaseId = "url_base";
    private const string GatewayMarketingBaseId = "url_marketing";
    private const string GatewaySysCreditBaseId = "url_syscredit";
    private const string UseFakeLocationId = "use_fake_location";
    private const string LatitudeId = "latitude";
    private const string LongitudeId = "longitude";
    private const string AllowGpsLocationId = "allow_gps_location";

    private readonly string AccessTokenDefault = string.Empty;
    private readonly string TokenDefaultId = string.Empty;
    private readonly bool UseMocksDefault = true;
    private readonly bool UseFakeLocationDefault = false;
    private readonly bool AllowGpsLocationDefault = false;
    private readonly double FakeLatitudeDefault = 47.604610d;
    private readonly double FakeLongitudeDefault = -122.315752d;

    private readonly string UrlIdentityDefault = GlobalSetting.Instance.BaseIdentityEndpoint;
    private readonly string UrlGatewayMarketingDefault = GlobalSetting.Instance.BaseGatewayMarketingEndpoint;
    private readonly string UrlGatewaySysCreditDefault = GlobalSetting.Instance.BaseGatewaySysCreditEndpoint;

    #endregion

    #region Settings Properties

    public string AuthAccessToken
    {
        get => Preferences.Get(AccessToken, AccessTokenDefault);
        set => Preferences.Set(AccessToken, value);
    }

    public string AuthIdToken
    {
        get => Preferences.Get(TokenId, TokenDefaultId);
        set => Preferences.Set(TokenId, value);
    }

    public bool UseMocks
    {
        get => Preferences.Get(UseMocksId, UseMocksDefault);
        set => Preferences.Set(UseMocksId, value);
    }

    public string IdentityEndpointBase
    {
        get => Preferences.Get(IdentityBaseId, UrlIdentityDefault);
        set
        {
            Preferences.Set(IdentityBaseId, value);
            GlobalSetting.Instance.BaseIdentityEndpoint = value;
        }
    }

    public string GatewaySysCreditEndpointBase
    {
        get => Preferences.Get(GatewaySysCreditBaseId, UrlGatewaySysCreditDefault);
        set
        {
            Preferences.Set(GatewaySysCreditBaseId, value);
            GlobalSetting.Instance.BaseGatewaySysCreditEndpoint = value;
        }
    }

    public string GatewayMarketingEndpointBase
    {
        get => Preferences.Get(GatewayMarketingBaseId, UrlGatewayMarketingDefault);
        set
        {
            Preferences.Set(GatewayMarketingBaseId, value);
            GlobalSetting.Instance.BaseGatewayMarketingEndpoint = value;
        }
    }

    public bool UseFakeLocation
    {
        get => Preferences.Get(UseFakeLocationId, UseFakeLocationDefault);
        set => Preferences.Set(UseFakeLocationId, value);
    }

    public string Latitude
    {
        get => Preferences.Get(LatitudeId, FakeLatitudeDefault.ToString());
        set => Preferences.Set(LatitudeId, value);
    }

    public string Longitude
    {
        get => Preferences.Get(LongitudeId, FakeLongitudeDefault.ToString());
        set => Preferences.Set(LongitudeId, value);
    }

    public bool AllowGpsLocation
    {
        get => Preferences.Get(AllowGpsLocationId, AllowGpsLocationDefault);
        set => Preferences.Set(AllowGpsLocationId, value);
    }

    #endregion
}
