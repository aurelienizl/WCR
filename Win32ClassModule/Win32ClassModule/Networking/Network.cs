using System.Net;
using System.Net.NetworkInformation;

namespace WindowsReportingClient;

//Based on https://github.com/redmindsteam/share-bridge

public class Networking {

    private static string host = "192.168.8.118";
    private static int port = 8000;

    public static void UploadReport()
    {
        Console.WriteLine("Exporting report, initializing network...");
        while (NetworkInterface.GetIsNetworkAvailable() == false)
        {
            Thread.Sleep(5000);
        }
        try
        {
            string path = Dns.GetHostName() + ".json";
            FileSender fileSender = new FileSender(host, port, path);
            fileSender.Start();
            Console.WriteLine("Successfully exported to server !");
        }
        catch (Exception e)
        {
            Thread.Sleep(5000);
            Console.WriteLine(e);
            UploadReport();
        }
        
    }

}

