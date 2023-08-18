namespace SysCredit.Helpers;

using System.Net;
using System.Net.Sockets;

public static class HttpHelper
{
    public static async ValueTask<string> GetIPAddressAsync()
    {
        IPHostEntry IpHostInfo = await Dns.GetHostEntryAsync(Dns.GetHostName());
        IPAddress IpAddress = IpHostInfo.AddressList[0];
        return IpAddress.ToString();
    }

    public static async ValueTask<string> GetServerIPAsync()
    {
        IPHostEntry IpHostInfo = await Dns.GetHostEntryAsync(Dns.GetHostName());

        foreach (IPAddress Address in IpHostInfo.AddressList)
        {
            if (Address.AddressFamily == AddressFamily.InterNetwork)
                return Address.ToString();
        }

        return string.Empty;
    }
}
