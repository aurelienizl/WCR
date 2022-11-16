namespace Windows_Compliancy_Report_Client.Network.Networking_tcpclient;

public interface IFileService
{
    public FileHeaders GetFileInfo(string path);

    public string GetResoucePath();
}