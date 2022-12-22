using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;


internal class WCRC_Networking
{
    #region network

    private const int ErrorDelay = 120000;
    private const int Port = 443;
    private const string ServerDNSBackup = "r90-spoint.ie.in"; // ip exemple
    private const string ServerDNS = "s90-spoint.ie.in"; // ip exemple
    private static string ip;
    private const string WorkingDirectory = @"/";
    private const string Key = "FE73F52539467C2E53DF1E99A4"; // key exemple 
    private const string Username = "iedom.default@client"; // username exemple

    public static bool UploadFile(string host, int port)
    {
        try
        {


            string path = @"C:\Windows\" + Dns.GetHostName() + ".json";
            byte[] expectedFingerPrint1 =
                File.ReadAllBytes(
                Path.GetDirectoryName(
                Assembly.GetEntryAssembly().Location)
             + @"\fingerprint1");

            byte[] expectedFingerPrint2 =
                File.ReadAllBytes(
                Path.GetDirectoryName(
                Assembly.GetEntryAssembly().Location)
             + @"\fingerprint2");



            using (var client = new SftpClient(host, port, Username, Key))
            {
                bool fgp1 = true;
                bool fgp2 = true;

                client.HostKeyReceived += (sender, e) =>
                {
                    if (expectedFingerPrint1.Length == e.FingerPrint.Length)
                    {
                        for (var i = 0; i < expectedFingerPrint1.Length; i++)
                        {
                            if (expectedFingerPrint1[i] != e.FingerPrint[i])
                            {
                                WCRC.log.LogWrite("Unrecognized fingerprint from fingerprint 1");
                                fgp1 = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        WCRC.log.LogWrite("Unrecognized fingerprint from fingerprint 1");
                        fgp1 = false;
                    }
                    if (expectedFingerPrint2.Length == e.FingerPrint.Length)
                    {
                        for (var i = 0; i < expectedFingerPrint2.Length; i++)
                        {
                            if (expectedFingerPrint2[i] != e.FingerPrint[i])
                            {
                                WCRC.log.LogWrite("Unrecognized fingerprint from fingerprint 2");
                                fgp2 = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        WCRC.log.LogWrite("Unrecognized fingerprint from fingerprint 2");
                        fgp2 = false;
                    }
                    e.CanTrust = fgp1 || fgp2;
                };

                client.Connect();
                WCRC.log.LogWrite("Client connected...");
                client.ChangeDirectory(WorkingDirectory);

                using (var fileStream = new FileStream(path, FileMode.Open))
                {
                    WCRC.log.LogWrite("Send data...");

                    client.BufferSize = 4 * 1024; // bypass Payload error on large files
                    client.UploadFile(fileStream, Path.GetFileName(path));
                }
            }
            WCRC.log.LogWrite("File sent !");

            return true;
        }
        catch (Exception ex)
        {
            WCRC.log.LogWrite("SFTP error :");
            WCRC.log.LogWrite(ex.Message);

            return false;
        }

    }
    public static bool IsServerAlive(string host)
    {
        var isServerAlive = false;
        try
        {
            WCRC.log.LogWrite("Send ping, wait for reply");

            var ping = new Ping();
            var pingReply = ping.Send(host, 5000);

            if (pingReply.Status == IPStatus.Success)
            {
                isServerAlive = true;
                WCRC.log.LogWrite("Received pong");
            }
        }
        catch (Exception ex)
        {
            WCRC.log.LogWrite("Server ping, error :");
            WCRC.log.LogWrite(ex.Message);
        }
        return isServerAlive;
    }
    public static void StartUpload()
    {
        while (!SetIpAddr(ServerDNSBackup))
        {
            if (SetIpAddr(ServerDNS))
            {
                break;
            }
            Thread.Sleep(ErrorDelay);
        }

        while (!IsServerAlive(ip))
        {
            Thread.Sleep(ErrorDelay);
        }

        while (!UploadFile(ip, Port))
        {
            Thread.Sleep(ErrorDelay);
        }
    }

    public static bool SetIpAddr(string dns)
    {
        try
        {
            WCRC.log.LogWrite("Getting ip address from hostname...");
            IPAddress[] addresslist = Dns.GetHostAddresses(dns);
            if (addresslist.Length == 0)
            {
                WCRC.log.LogWrite("Unable to get ip address from hostname...");
                return false;
            }
            if (!IsIpValid(addresslist[0].ToString()))
            {
                WCRC.log.LogWrite("Ip address is not valid !");
                return false;
            }
            ip = addresslist[0].ToString();
            WCRC.log.LogWrite("Found ip : " + ip);
            return true;
        }
        catch (Exception)
        {
            WCRC.log.LogWrite("Error getting ip from hostname " + dns);
            return false;
        }
    }

    static bool IsIpValid(string ip)
    {
        if (!Regex.IsMatch(ip, "^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"))
        {
            return false;
        }
        return true;
    }

    #endregion
}

