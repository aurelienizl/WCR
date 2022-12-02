using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.IO;
using System;
using System.Collections.Generic;
using Renci.SshNet;

class WCRC
{
    public void Start()
    {
        LaunchReport();
        Upload();
    }

    #region report


    private static List<Win32_Bios> Bios { get; set; }
    private static List<Win32_EncryptableVolume> Win32_EncryptableVolumes { get; set; }
    private static List<Win32_Tpms> Win32_Tpm { get; set; }
    private static List<Win32_Product> Win32_Products { get; set; }
    private static List<Win32_X509Cert> X509CertList { get; set; }
    private static List<Win32_QuickFixEngineering> Win32_QFE { get; set; }
    private static List<Account> Accounts { get; set; }
    private static Win32_SystemInfo Sysinfo { get; set; }
    private static List<Win32_Startup> Startups { get; set; }

    public static void LaunchReport()
    {

        Thread.CurrentThread.IsBackground = true;
        var threads = new List<Thread>
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Bios = Win32_Bios.GetBios();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_EncryptableVolumes = Win32_EncryptableVolume.GetEncryptableVolume();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_Tpm = Win32_Tpms.GetTpm();

            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_Products = Win32_Product.GetProducts();

            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                X509CertList = Win32_X509Cert.GetX509Cert();


            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_QFE = Win32_QuickFixEngineering.GetQuickFixEngineering();


            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Accounts = Account.GetLocalUsers();


            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Sysinfo = Win32_SystemInfo.GetSystemInfo();

            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Startups = Win32_Startup.GetStartupApps();

            })
        };


        foreach (var item in threads) item.Start();
        foreach (var item in threads) item.Join();

        var report = new Win32_Report(
        Bios,
            Win32_EncryptableVolumes,
        Win32_Tpm,
            Win32_Products,
            X509CertList,
        Win32_QFE,
            Accounts,
            Sysinfo,
            Startups
        );
        Win32_Report.GenerateReport(report, Dns.GetHostName() + ".json");

    }

    #endregion

    #region network

    public const string serverIP = "127.0.0.1";
    public const int Port = 2222;
    private const string WorkingDirectory = @"/";
    private static readonly string Key = "password"; // key exemple 
    private static readonly string Username = "tester"; // username exemple

    public static void Upload(string host = serverIP, int port = Port)
    {
        string path = Dns.GetHostName() + ".json";

        using (var client = new SftpClient(host, port, Username, Key))
        {
            client.Connect();
            client.ChangeDirectory(WorkingDirectory);

            using (var fileStream = new FileStream(path, FileMode.Open))
            {
                client.BufferSize = 4 * 1024; // bypass Payload error on large files
                client.UploadFile(fileStream, Path.GetFileName(path));
            }
        }
    }

    public static bool IsServerAlive(string host)
    {
        var isServerAlive = false;
        try
        {
            var ping = new Ping();
            var pingReply = ping.Send(host, 5000);

            if (pingReply.Status == IPStatus.Success) isServerAlive = true;
        }
        catch (Exception)
        {
        }
        return isServerAlive;
    }

    public static void UploadReportUsingSftp(string host, int port, string path)
    {
    AutoReset:
        while (!IsServerAlive(host))
        {
            Thread.Sleep(30000);
        }
        try
        {
            Upload(host, port);
        }
        catch (Exception)
        {
            Thread.Sleep(30000);
            goto AutoReset;
        }
    }
    #endregion
}