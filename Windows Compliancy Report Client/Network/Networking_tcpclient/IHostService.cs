namespace Windows_Compliancy_Report_Client;

public interface IHostService
{
    string GetHostIP();

    bool IsConnected(string ip);
}