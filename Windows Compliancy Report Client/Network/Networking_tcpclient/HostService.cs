using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Windows_Compliancy_Report_Client;

public class HostService : IHostService
{
    public string GetHostIP()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
            if (ip.AddressFamily == AddressFamily.InterNetwork)
                return ip.ToString();
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }

    public bool IsConnected(string ip)
    {
        var ping = new Ping();
        var address = IPAddress.Parse(ip);
        var pong = ping.Send(address);
        if (pong.Status == IPStatus.Success)
            return true;
        return false;
    }
}