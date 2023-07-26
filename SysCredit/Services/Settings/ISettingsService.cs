namespace SysCredit.Services.Settings;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISettingsService
{
    string AuthAccessToken { get; set; }

    string AuthIdToken { get; set; }

    bool UseMocks { get; set; }

    string IdentityEndpointBase { get; set; }

    string GatewaySysCreditEndpointBase { get; set; }

    string GatewayMarketingEndpointBase { get; set; }

    bool UseFakeLocation { get; set; }

    string Latitude { get; set; }

    string Longitude { get; set; }

    bool AllowGpsLocation { get; set; }
}
