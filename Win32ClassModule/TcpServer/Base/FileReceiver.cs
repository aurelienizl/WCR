using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace WindowsReportingClient
{
    public class FileReceiver : IStartable, IStopable
    {
        private readonly string _host;
        private readonly int _port;
        private readonly TcpListener _listener;
        private readonly IFileService _fileService;

        public FileReceiver(string host, int port)
        {
            _host = host;
            _port = port;
            _listener = new TcpListener(IPAddress.Any, _port);
            _fileService = new FileService();
        }

        public void Start()
        {
           
            Console.WriteLine("Starting listener...");
            _listener.Start();
            Console.WriteLine("Waiting clients...");
            

            AutoResetEvent:
            try
            {
                
                Socket socket = _listener.AcceptSocket();
                socket.ReceiveTimeout = 5000;
                socket.SendTimeout = 5000;
                
                Console.WriteLine("Client found !");

                int bufferSize = 1024;
                byte[] buffer, header;

                header = new byte[bufferSize];
                socket.Receive(header);

                string headerJson = Encoding.ASCII.GetString(header);
                var fileheaders = JsonConvert.DeserializeObject<FileHeaders>(headerJson);

                if (fileheaders is null) throw new Exception("File headers doesn't exist");

                string path = Path.Combine(_fileService.GetResoucePath(), fileheaders.FileName);

                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    while (fileheaders.Lenth > 0)
                    {
                        buffer = new byte[bufferSize];
                        int size = socket.Receive(buffer, SocketFlags.Partial);
                        fs.Write(buffer, 0, size);
                        fileheaders.Lenth -= size;
                    }
                    fs.Close();
                }
                Console.WriteLine("File downloaded !");
                Console.WriteLine("Closing socket...");
                socket.Close();
                goto AutoResetEvent;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);               
                goto AutoResetEvent;
            }         
            
        }



        public void Stop()
        {
            _listener.Stop();
        }
    }
}