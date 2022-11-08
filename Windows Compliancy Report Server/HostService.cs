using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace Windows_Compliancy_Report_Server
{
    public class HostService : IHostService
    {
        public string GetHostIP()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }

            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public bool IsConnected(string ip)
        {
            Ping ping = new Ping();
            IPAddress address = IPAddress.Parse(ip);
            PingReply pong = ping.Send(address);
            if (pong.Status == IPStatus.Success)
            {
                return true;
            }
            else return false;
        }
    }
}
