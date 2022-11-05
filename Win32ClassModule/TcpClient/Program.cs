namespace TcpClientApp;

//Based on https://github.com/redmindsteam/share-bridge

class Program {

    public static string host = "127.0.0.1";
    public static int port = 8080;
    public static string path = @"C:\\Users\\aurel\\Desktop\\iedom\\Builds\\test.txt\";

    static void Main(string[] args)
    {
        FileSender fileSender = new FileSender(host, 8000, path);
        fileSender.Start();
    }

}

