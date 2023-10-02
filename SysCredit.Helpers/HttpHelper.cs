namespace SysCredit.Helpers;

using System.Net;
using System.Net.Sockets;

/// <summary>
///     Métodos de ayuda para obtener información adicional asociada al servidor.
/// </summary>
public static class HttpHelper
{
    /// <summary>
    ///     Obtiene la dirección IP del servidor actual.
    /// </summary>
    /// <returns>
    ///     Regresa la dirección IP del servidor.
    /// </returns>
    public static async ValueTask<string> GetIPAddressAsync()
    {
        IPHostEntry IpHostInfo = await Dns.GetHostEntryAsync(Dns.GetHostName());
        IPAddress IpAddress = IpHostInfo.AddressList[0];
        return IpAddress.ToString();
    }

    /// <summary>
    ///     Obtiene la dirección IP del servidor actual.
    /// </summary>
    /// <returns>
    ///     Regresa la dirección IP el servidor.
    /// </returns>
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
