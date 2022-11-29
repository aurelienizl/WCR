using Renci.SshNet;
namespace Windows_Compliancy_Report_Client.Network.Networking_sftp;

internal class Sftp
{
    private const string workingdirectory = @"/";
    private static readonly string key = "password"; // key exemple 
    private static readonly string username = "tester"; // username exemple

    public static void Upload(string host, int port, string path)
    {
        using (var client = new SftpClient(host, port, username, key))
        {
            client.Connect();
            Program.window?.Writeline("[INFO] Connected to " + host, false);

            client.ChangeDirectory(workingdirectory);
            Program.window?.Writeline("[INFO] Changed directory to " + workingdirectory, false);

            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                Program.window?.Writeline("[INFO] Writing : " + fileStream.Length + " bytes", false);
                client.BufferSize = 4 * 1024; // bypass Payload error on large files
                client.UploadFile(fileStream, Path.GetFileName(path));
            }
        }
    }
}