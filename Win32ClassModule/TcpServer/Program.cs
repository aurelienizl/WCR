namespace TcpClientApp;

//Based on https://github.com/redmindsteam/share-bridge

class Program
{
    static void Main(string[] args)
    {
        FileReceiver fileReceiver = new FileReceiver("127.0.0.1", 8000);
        fileReceiver.Start();
    }
}