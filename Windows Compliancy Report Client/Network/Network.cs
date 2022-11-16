using System.Net.NetworkInformation;
using Windows_Compliancy_Report_Client.Network.Networking_sftp;
using Windows_Compliancy_Report_Client.Network.Networking_tcpclient;

namespace Windows_Compliancy_Report_Client.Network;

//Based on https://github.com/redmindsteam/share-bridge

public static class Networking
{
    public static bool IsServerAlive(string host)
    {
        var isServerAlive = false;
        try
        {
            var ping = new Ping();
            var pingReply = ping.Send(host, 5000);

            if (pingReply.Status == IPStatus.Success) isServerAlive = true;
        }
        catch (Exception e)
        {
            Program.window?.Writeline("[WARNING] Server unreachable", false);
            Program.window?.Writeline("[EXCEPTION] " + e, false);
        }

        return isServerAlive;
    }

    #region networksMethods

    public static void UploadReportUsingSftp(string host, int port, string path)
    {
        AutoReset:
        while (!IsServerAlive(host))
        {
            Program.window?.Writeline("[INFO] Server unreachable... restarting", false);
            Thread.Sleep(30000);
        }

        try
        {
            Sftp.Upload(host, port, path);
        }
        catch (Exception e)
        {
            Program.window?.Writeline("[EXCEPTION] " + e.Message, false);
            Thread.Sleep(30000);
            goto AutoReset;
        }
    }

    //Not secure, no encryption. 

    [Obsolete]
    public static void UploadReportUsingTcpCient(string host, int port, string path)
    {
        while (!IsServerAlive(host)) Thread.Sleep(4000);

        var fileSender = new FileSender(host, port, path);

        AutoReset:
        try
        {
            fileSender.Start();
        }
        catch (Exception)
        {
            Thread.Sleep(30000);
            goto AutoReset;
        }
    }

    #endregion
}