namespace WindowsReportingClient;

//Based on https://github.com/redmindsteam/share-bridge

class Program
{
    static void Main(string[] args)
    {
        FileReceiver fileReceiver = new FileReceiver("0.0.0.0", 8000);
        fileReceiver.Start();
    }
}