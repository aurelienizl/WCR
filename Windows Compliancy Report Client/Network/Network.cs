using System.DirectoryServices.ActiveDirectory;
using System.Net;
using System.Net.NetworkInformation;

namespace Windows_Compliancy_Report_Client;

//Based on https://github.com/redmindsteam/share-bridge

public static class Networking
{
    #region networksMethods
    public static void UploadReportUsingSftp(string host, int port, string path)
    {
    AutoReset:
        while (!IsServerAlive(host))
        {
            Thread.Sleep(4000);
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

        while (!IsServerAlive(host))
        {
            Thread.Sleep(4000);
        }

        FileSender fileSender = new FileSender(host, port, path);

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

    public static bool IsServerAlive(string host)
    {
        bool isServerAlive = false;
        try
        {
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(host, 5000);

            if (pingReply.Status == IPStatus.Success)
            {
                isServerAlive = true;
            }
        }
        catch (Exception e)
        {
            Program.window?.Writeline("[WARNING] Server unreachable", false);
            Program.window?.Writeline("[EXCEPTION] " + e, false);
        }
        return isServerAlive;
    }

}

