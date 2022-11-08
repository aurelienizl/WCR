using System.DirectoryServices.ActiveDirectory;
using System.Net;
using System.Net.NetworkInformation;

namespace Windows_Compliancy_Report_Client;

//Based on https://github.com/redmindsteam/share-bridge

public class Networking
{

    public static void UploadReport(string host, int port)
    {
        string path = Dns.GetHostName() + ".json";
        FileSender fileSender = new FileSender(host, port, path);

        AutoReset:
		try
		{
            fileSender.Start();
        }
		catch (Exception e)
		{
            Console.WriteLine(e.Message);
            Thread.Sleep(30000);
            goto AutoReset;			
		}
        
    }

}

