using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;

namespace Windows_Compliancy_Report_Client.Network.Networking_tcpclient;

public class FileSender : IStartable
{
    private readonly string _filepath;
    private readonly FileService _fileService;
    private readonly string _host;
    private readonly int _port;
    private FileStream? _stream;
    private TcpClient? _tcpClient;

    public FileSender(string host, int port, string filepath)
    {
        _host = host;
        _port = port;
        _filepath = filepath;
        _fileService = new FileService();
    }

    public void Start()
    {
        try
        {
            _stream = new FileStream(_filepath, FileMode.Open);
            var bufferSize = 1024;

            var bufferCount = Convert.ToInt32(Math.Ceiling(_stream.Length / (double)bufferSize));

            _tcpClient = new TcpClient(_host, _port);
            _tcpClient.SendTimeout = 5000;
            _tcpClient.ReceiveTimeout = 5000;

            var header = new byte[bufferSize];
            var fileHeaders = _fileService.GetFileInfo(_filepath);
            var headerStr = JsonConvert.SerializeObject(fileHeaders);

            Array.Copy(Encoding.ASCII.GetBytes(headerStr), header, Encoding.ASCII.GetBytes(headerStr).Length);

            _tcpClient.Client.Send(header);

            for (var i = 0; i < bufferCount; i++)
            {
                var buffer = new byte[bufferSize];
                var size = _stream.Read(buffer, 0, bufferSize);
                _tcpClient.Client.Send(buffer, size, SocketFlags.Partial);
            }

            if (_stream is not null) _stream.Close();
            if (_tcpClient is not null) _tcpClient.Client.Close();
        }
        catch (Exception)
        {
            if (_stream is not null) _stream.Close();
            if (_tcpClient is not null) _tcpClient.Client.Close();
        }
    }
}