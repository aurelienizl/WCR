using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using WCRC_Core.Reporting;
using WCRC_Service;

class WCRC
{
    public static Logs log;
    Thread network;
    Thread report;

    public void Report()
    {
        log = new Logs("Initialised logs");

        log.LogWrite("Initialising reporting tool");
        report = new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;
            LaunchReport();
        });
        report.Start();
        report.Join();
    }
    public void Send()
    {
        log.LogWrite("Initialising networking tool");
        network = new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;
            StartUpload();
        });
        network.Start();
        network.Join();
    }

    #region report

    private static List<Win32_Bios> Bios_ { get; set; }
    private static List<Win32_EncryptableVolumes> Win32_EncryptableVolumes_ { get; set; }
    private static List<Win32_Tpms> Win32_Tpm_ { get; set; }
    private static List<Win32_Products> Win32_Products_ { get; set; }
    private static List<Win32_X509Cert> X509CertList_ { get; set; }
    private static List<Win32_QuickFixEngineerings> Win32_QFE_ { get; set; }
    private static List<Account> Accounts_ { get; set; }
    private static Win32_SystemInfo Sysinfo_ { get; set; }
    private static List<Win32_Startup> Startups_ { get; set; }
    public static Win32_Error Win32_Error_ { get; set; }

    public static void LaunchReport()
    {
        Thread.CurrentThread.IsBackground = true;
        Win32_Error_ = new Win32_Error();

        var threads = new List<Thread>
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Bios_ = Win32_Bios.GetBios();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_EncryptableVolumes_ = Win32_EncryptableVolumes.GetEncryptableVolume();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_Tpm_ = Win32_Tpms.GetTpm();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_Products_ = global::Win32_Products.GetProducts();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                X509CertList_ = Win32_X509Cert.GetX509Cert();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_QFE_ = Win32_QuickFixEngineerings.GetQuickFixEngineering();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Accounts_ = Account.GetLocalUsers();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Sysinfo_ = Win32_SystemInfo.GetSystemInfo();

            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Startups_ = Win32_Startup.GetStartupApps();
            })
        };

        log.LogWrite("Starting WMI Queries...");

        foreach (var item in threads) item.Start();
        foreach (var item in threads) item.Join();

        log.LogWrite("WMI Queries finished");

        var report = new Win32_Report(
        Bios_,
            Win32_EncryptableVolumes_,
        Win32_Tpm_,
            Win32_Products_,
            X509CertList_,
        Win32_QFE_,
            Accounts_,
            Sysinfo_,
            Startups_,
            Win32_Error_
        );

        log.LogWrite("Serializing data...");
        Win32_Report.GenerateReport(report, @"C:\Windows\" + Dns.GetHostName() + ".json");
        log.LogWrite("Data serialized, report generated");
    }

    #endregion

    #region network

    private const int ErrorDelay = 1000;
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
                                log.LogWrite("Unrecognized fingerprint from fingerprint 1");
                                fgp1 = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        log.LogWrite("Unrecognized fingerprint from fingerprint 1");
                        fgp1 = false;
                    }
                    if (expectedFingerPrint2.Length == e.FingerPrint.Length)
                    {
                        for (var i = 0; i < expectedFingerPrint2.Length; i++)
                        {
                            if (expectedFingerPrint2[i] != e.FingerPrint[i])
                            {
                                log.LogWrite("Unrecognized fingerprint from fingerprint 2");
                                fgp2 = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        log.LogWrite("Unrecognized fingerprint from fingerprint 2");
                        fgp2 = false;
                    }
                    e.CanTrust = fgp1 || fgp2;
                };

                client.Connect();
                log.LogWrite("Client connected...");
                client.ChangeDirectory(WorkingDirectory);

                using (var fileStream = new FileStream(path, FileMode.Open))
                {
                    log.LogWrite("Send data...");

                    client.BufferSize = 4 * 1024; // bypass Payload error on large files
                    client.UploadFile(fileStream, Path.GetFileName(path));
                }
            }
            log.LogWrite("File sent !");

            return true;
        }
        catch (Exception ex)
        {
            log.LogWrite("SFTP error :");
            log.LogWrite(ex.Message);

            return false;
        }

    }
    public static bool IsServerAlive(string host)
    {
        var isServerAlive = false;
        try
        {
            log.LogWrite("Send ping, wait for reply");

            var ping = new Ping();
            var pingReply = ping.Send(host, 5000);

            if (pingReply.Status == IPStatus.Success)
            {
                isServerAlive = true;
                log.LogWrite("Received pong");
            }
        }
        catch (Exception ex)
        {
            log.LogWrite("Server ping, error :");
            log.LogWrite(ex.Message);
        }
        return isServerAlive;
    }
    public static void StartUpload()
    {
        while(!SetIpAddr(ServerDNSBackup))
        {
            if(SetIpAddr(ServerDNS))
            {
                break;
            }
            Thread.Sleep(ErrorDelay);
        }

        while(!IsServerAlive(ip))
        {
            Thread.Sleep(ErrorDelay);
        }

        while(!UploadFile(ip, Port))
        {
            Thread.Sleep(ErrorDelay);
        }
    }

    public static bool SetIpAddr(string dns)
    {
        try
        {
            log.LogWrite("Getting ip address from hostname...");
            IPAddress[] addresslist = Dns.GetHostAddresses(dns);
            if (addresslist.Length == 0)
            {
                log.LogWrite("Unable to get ip address from hostname...");
                return false;
            }
            if (!IsIpValid(addresslist[0].ToString()))
            {
                log.LogWrite("Ip address is not valid !");
                return false;
            }
            ip = addresslist[0].ToString();
            log.LogWrite("Found ip : " + ip);
            return true;
        }
        catch (Exception)
        {
            log.LogWrite("Error getting ip from hostname " + dns);
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