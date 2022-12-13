using Renci.SshNet;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace WCRC_Fingerprint
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type host ip :");
            string host = Console.ReadLine();
            Console.WriteLine("Type port number :");
            string port = Console.ReadLine();
            GetFingerprint(host, port);
            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        static int IsPortValid(string portAsStroing)
        {
            try
            {
                int port = Convert.ToInt32(portAsStroing);
                if (port < 65536 || port > 1)
                {
                    return port;
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Critical : Unable to parse port");
            }
            return -1;
        }
        static void GetFingerprint(string ip, string portAsString)
        {
            int port;
            port = IsPortValid(portAsString);
            if (port is -1) return;

            if (!Regex.IsMatch(ip, "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"))
            {
                Console.WriteLine("CRITICAL : Unable to parse ip");
                return;
            }

            using (var client = new SftpClient(ip, port, "guest", "pwd"))
            {
                client.HostKeyReceived += (sender, e) =>
                {
                    File.WriteAllBytes("fingerprint", e.FingerPrint);
                };
                try
                {
                    client.Connect();
                    client.Dispose();
                }
                catch (Exception)
                {
                    Console.WriteLine("Error, cannot connect");
                }
            }
            Console.WriteLine("Got fingerprint, exit");
        }
    }

}
