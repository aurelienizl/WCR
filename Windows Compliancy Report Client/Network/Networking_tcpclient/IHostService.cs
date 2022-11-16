namespace Windows_Compliancy_Report_Client.Network.Networking_tcpclient;

public interface IHostService
{
    string GetHostIP();

    bool IsConnected(string ip);
}