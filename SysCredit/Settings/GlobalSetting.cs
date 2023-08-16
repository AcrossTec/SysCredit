namespace SysCredit.Mobile.Settings;

public class GlobalSetting
{
    public const string AzureTag = "Azure";
    public const string MockTag = "Mock";
    public const string DefaultEndpoint = "http://YOUR_IP_OR_DNS_NAME"; // i.e.: "http://YOUR_IP" or "http://YOUR_DNS_NAME"

    private string m_BaseIdentityEndpoint = string.Empty;
    private string m_BaseGatewaySysCreditEndpoint = string.Empty;
    private string m_BaseGatewayMarketingEndpoint = string.Empty;

    public GlobalSetting()
    {
        AuthToken = "INSERT AUTHENTICATION TOKEN";
        BaseIdentityEndpoint = DefaultEndpoint;
        BaseGatewaySysCreditEndpoint = DefaultEndpoint;
        BaseGatewayMarketingEndpoint = DefaultEndpoint;
    }

    public static GlobalSetting Instance { get; } = new GlobalSetting();

    public string BaseIdentityEndpoint
    {
        get => m_BaseIdentityEndpoint;
        set
        {
            m_BaseIdentityEndpoint = value;
            UpdateEndpoint(m_BaseIdentityEndpoint);
        }
    }

    public string BaseGatewaySysCreditEndpoint
    {
        get => m_BaseGatewaySysCreditEndpoint;
        set
        {
            m_BaseGatewaySysCreditEndpoint = value;
            UpdateGatewaySysCreditEndpoint(m_BaseGatewaySysCreditEndpoint);
        }
    }

    public string BaseGatewayMarketingEndpoint
    {
        get => m_BaseGatewayMarketingEndpoint;
        set
        {
            m_BaseGatewayMarketingEndpoint = value;
            UpdateGatewayMarketingEndpoint(m_BaseGatewayMarketingEndpoint);
        }
    }

    public string ClientId { get; } = "Xamarin";

    public string ClientSecret { get; } = "Secret";

    public string AuthToken { get; set; } = string.Empty;

    public string RegisterWebsite { get; set; } = string.Empty;

    public string AuthorizeEndpoint { get; set; } = string.Empty;

    public string UserInfoEndpoint { get; set; } = string.Empty;

    public string TokenEndpoint { get; set; } = string.Empty;

    public string LogoutEndpoint { get; set; } = string.Empty;

    public string Callback { get; set; } = string.Empty;

    public string LogoutCallback { get; set; } = string.Empty;

    public string GatewaySysCreditEndpoint { get; set; } = string.Empty;

    public string GatewayMarketingEndpoint { get; set; } = string.Empty;

    private void UpdateEndpoint(string Endpoint)
    {
        RegisterWebsite = $"{Endpoint}/Account/Register";
        LogoutCallback = $"{Endpoint}/Account/Redirecting";

        var ConnectBaseEndpoint = $"{Endpoint}/Connect";
        AuthorizeEndpoint = $"{ConnectBaseEndpoint}/Authorize";
        UserInfoEndpoint = $"{ConnectBaseEndpoint}/UserFnfo";
        TokenEndpoint = $"{ConnectBaseEndpoint}Token";
        LogoutEndpoint = $"{ConnectBaseEndpoint}/EndSession";

        var BaseUri = GlobalSetting.ExtractBaseUri(Endpoint);
        Callback = $"{BaseUri}/XamarinCallback";
    }

    private void UpdateGatewaySysCreditEndpoint(string Endpoint)
    {
        GatewaySysCreditEndpoint = Endpoint;
    }

    private void UpdateGatewayMarketingEndpoint(string Endpoint)
    {
        GatewayMarketingEndpoint = Endpoint;
    }

    private static string ExtractBaseUri(string Endpoint)
    {
        try
        {
            var Uri = new Uri(Endpoint);
            var BaseUri = Uri.GetLeftPart(UriPartial.Authority);

            return BaseUri;
        }
        catch (Exception)
        {
            return DefaultEndpoint;
        }
    }
}
