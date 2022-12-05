using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO;
using System;
using System.Collections.Generic;
using Renci.SshNet;
using WCRC_Service;
using WCRC_Core.Reporting;

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

    private const int Port = 2222;
    private const string ServerIP = "127.0.0.1";
    private const string WorkingDirectory = @"/";
    private const string Key = "password"; // key exemple 
    private const string Username = "tester"; // username exemple

    public static bool UploadFile(string host = ServerIP, int port = Port)
    {
        try
        {
            string path = @"C:\Windows\" + Dns.GetHostName() + ".json";

            using (var client = new SftpClient(host, port, Username, Key))
            {
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
    public static void StartUpload(string host = ServerIP, int port = Port)
    {
        while (!IsServerAlive(host))
        {
            log.LogWrite("Server unrechable... sleeping");
            Thread.Sleep(30000);
        }
        log.LogWrite("Server is alive !");
        try
        {
            log.LogWrite("Starting authentification...");

            if (UploadFile(host, port))
            {
                log.LogWrite("File uploaded, exiting...");
                return;
            }
            else
            {
                log.LogWrite("File not uploaded, restarting...");
                Thread.Sleep(30000);
                StartUpload();
            }
        }
        catch (Exception ex)
        {
            log.LogWrite("File upload, error :");
            log.LogWrite(ex.Message);
            Thread.Sleep(30000);
            StartUpload();
        }
    }

    #endregion
}