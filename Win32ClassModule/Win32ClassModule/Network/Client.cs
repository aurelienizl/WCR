using System.Net;
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Text;
using Win32ClassModule.Win32_Modules;


namespace Win32ClassModule.Network
{
    internal class Client
    {

        private const int BufferSize = 1024;

        public static void StartClient(string fileName = "report.json", string ip = "127.0.0.1", Int32 port = 8088)
        {
            bool reported = false;

            byte[] SendingBuffer = null;
            TcpClient client = null;
            NetworkStream netstream = null;
            try
            {
                client = new TcpClient(ip, port);
                Console.WriteLine("Connected to the Server...");
                netstream = client.GetStream();
                FileStream Fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                int NoOfPackets = Convert.ToInt32
               (Math.Ceiling(Convert.ToDouble(Fs.Length) / Convert.ToDouble(BufferSize)));
                int TotalLength = (int)Fs.Length, CurrentPacketLength;
                Console.WriteLine("Starting file uploading...");
                for (int i = 0; i < NoOfPackets; i++)
                {
                    if (TotalLength > BufferSize)
                    {
                        CurrentPacketLength = BufferSize;
                        TotalLength = TotalLength - CurrentPacketLength;
                    }
                    else
                    {
                        CurrentPacketLength = TotalLength;
                    }
                    SendingBuffer = new byte[CurrentPacketLength];
                    Fs.Read(SendingBuffer, 0, CurrentPacketLength);
                    netstream.Write(SendingBuffer, 0, (int)SendingBuffer.Length);
                }
                Console.WriteLine("Sent " + Fs.Length.ToString() + " bytes to the server");
                Fs.Close();
                reported = true;

                netstream.Close();
                client.Close();
            }
            catch (Exception ex)
            {               
                Console.WriteLine(ex.Message);
                Thread.Sleep(30000); 
                StartClient();
            }

        }
    }
}
