using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace Windows_Compliancy_Report_Client
{
    public class FileSender : IStartable
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _filepath;
        private readonly FileService _fileService;

        public FileSender(string host, int port, string filepath)
        {
            _host = host;
            _port = port;
            _filepath = filepath;
            _fileService = new FileService();
        }

        TcpClient _tcpClient;   
        FileStream _stream;

        public void Start()
        {
            try
            {
                _stream = new FileStream(_filepath, FileMode.Open);
                int bufferSize = 1024;
                byte[] buffer, header;

                int bufferCount = Convert.ToInt32(Math.Ceiling(_stream.Length / (double)bufferSize));

                _tcpClient = new TcpClient(_host, _port);
                _tcpClient.SendTimeout = 5000;
                _tcpClient.ReceiveTimeout = 5000;

                header = new byte[bufferSize];
                var fileHeaders = _fileService.GetFileInfo(_filepath);
                string headerStr = JsonConvert.SerializeObject(fileHeaders);

                Array.Copy(Encoding.ASCII.GetBytes(headerStr), header, Encoding.ASCII.GetBytes(headerStr).Length);

                _tcpClient.Client.Send(header);

                for (int i = 0; i < bufferCount; i++)
                {
                    buffer = new byte[bufferSize];
                    int size = _stream.Read(buffer, 0, bufferSize);
                    _tcpClient.Client.Send(buffer, size, SocketFlags.Partial);
                }
                if (_stream is not null)
                {
                    _stream.Close();
                }
                if (_tcpClient is not null)
                {
                    _tcpClient.Client.Close();
                }
            }
            catch (Exception)
            {
                if(_stream is not null)
                {
                    _stream.Close();
                }
                if(_tcpClient is not null)
                {
                    _tcpClient.Client.Close();
                }
            }
        }
    }
}