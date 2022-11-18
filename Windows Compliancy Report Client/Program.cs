using System.Net;
using System.Security.Principal;
using Windows_Compliancy_Report_Client.Forms;
using Windows_Compliancy_Report_Client.Network;
using Windows_Compliancy_Report_Client.Reporting;

namespace Windows_Compliancy_Report_Client;

internal static class Program
{
    //<params> Hardcoded parameters

    private const string Host = "10.209.242.60";
    private const int Port = 443;
    private static string? FileName;

    //</params>

    public static Window? window;

    [STAThread]
    private static int Main()
    {
        
        FileName = Dns.GetHostName() + ".json";
        if (!IsAdministrator())
        {
            MessageBox.Show(@"Run as admin or hostname not found");
            return 0;
        }

        ApplicationConfiguration.Initialize();
        window = new Window();
        Application.Run(window);
        return 0;
    }


    //Check if program is properly launched
    private static bool IsAdministrator()
    {
        var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    //Called when user press button exit
    public static void ExitApp()
    {
        if (NetworkThread is not null)
            if (NetworkThread.IsAlive)
            {
                window?.Writeline("[INFO] Please wait networking thread to finished !", false);
                NetworkThread.Join();
                window?.Writeline("[INFO] Networking thread finished !", false);
            }

        if (ReportingThread is not null)
            if (ReportingThread.IsAlive)
            {
                window?.Writeline("[INFO] Please wait reporting thread to finished !", false);
                ReportingThread.Join();
                window?.Writeline("[INFO] Reporting thread finished !", false);
            }

        window?.Writeline("[INFO] Closing app... Have a nice day !", false);
    }

    //Automatic startup sequence
    public static void Automatic_Launch()
    {
        new Thread(() =>
        {
            InitReportingTool();
            if (ReportingThread is not null) ReportingThread.Join();
            InitNetworking();
            if (NetworkThread is not null) NetworkThread.Join();
        }).Start();
    }

    #region Networking

    private static Thread? NetworkThread;

    public static void InitNetworking()
    {
        //Check if hostname is found or exit
        if (FileName is null)

        {
            window?.Writeline("[CRITICAL] Unable to get Hostname", false);
            return;
        }

        //If report is not initialized exit
        if (ReportingThread is null)
        {
            window?.Writeline("[INFO] Generate report first !", false);
            return;
        }

        //Wait reporter to finished & exit
        if (ReportingThread.IsAlive)
        {
            window?.Writeline("[INFO] Please wait reporter thread to finished !", false);
            return;
        }

        // Start network thread 
        if (NetworkThread is null || !NetworkThread.IsAlive)
        {
            NetworkThread = new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Networking.UploadReportUsingSftp(Host, Port, FileName);
                window?.Writeline("[INFO] Network upload job finished !", false);
            });
            NetworkThread.Start();
            window?.Writeline("[INFO] Starting network thread...", false);
            return;
        }

        window?.Writeline("[INFO] Network thread already running !", false);
    }

    #endregion

    #region Report

    private static List<Win32_Bios>? Bios { get; set; }
    private static List<Win32_EncryptableVolume>? win32_EncryptableVolumes { get; set; }
    private static List<Win32_Tpm>? Win32_Tpm { get; set; }
    private static List<Win32_Product>? Win32_Products { get; set; }
    private static List<X509Cert>? X509CertList { get; set; }
    private static List<Win32_QuickFixEngineering>? Win32_QFE { get; set; }
    private static List<Account>? Accounts { get; set; }
    private static SystemInfo? Sysinfo { get; set; }
    private static List<Startup>? Startups { get; set; }
    private static Thread? ReportingThread;

    public static void InitReportingTool()
    {
        //Check 1 : If networking thread is running or reporting thread is running exit,

        if (FileName is null)

        {
            window?.Writeline("[CRITICAL] Unable to get Hostname", false);
            return;
        }

        //Wait reporter to finished
        if (NetworkThread is not null)
            if (NetworkThread.IsAlive)
            {
                window?.Writeline("[INFO] Please wait networking thread to finished !", false);
                return;
            }

        if (ReportingThread is null || !ReportingThread.IsAlive)
        {
            LaunchReport();
            window?.Writeline("[INFO] Starting reporting thread...", false);
            return;
        }

        window?.Writeline("[INFO] Reporting thread already running !", false);
    }

    public static void LaunchReport()
    {
        ReportingThread = new Thread(() =>
        {
            Thread.CurrentThread.IsBackground = true;
            var threads = new List<Thread>();

            threads.Add(new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Bios = Win32_Bios.GetBios();
                if (Bios != null)
                    window?.Writeline("[INFO] Bios check : OK !", false);
                else
                    window?.Writeline("[ERROR] Bios check : WARNING !", false);
            }));

            threads.Add(new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                win32_EncryptableVolumes = Win32_EncryptableVolume.GetEncryptableVolume();

                if (win32_EncryptableVolumes != null)
                    window?.Writeline("[INFO] Bitlocker check : OK !", false);
                else
                    window?.Writeline("[ERROR] Bitlocker check : WARNING !", false);
            }));

            threads.Add(new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_Tpm = Reporting.Win32_Tpm.GetTpm();
                if (Win32_Tpm != null)
                    window?.Writeline("[INFO] Tpm check : OK !", false);
                else
                    window?.Writeline("[ERROR] Tpm check : WARNING !", false);
            }));

            threads.Add(new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_Products = Win32_Product.GetProducts();
                if (Win32_Products != null)
                    window?.Writeline("[INFO] Softwares check : OK !", false);
                else
                    window?.Writeline("[ERROR] Softwares check : WARNING !", false);
            }));

            threads.Add(new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                X509CertList = X509Cert.GetX509Cert();

                if (X509CertList != null)
                    window?.Writeline("[INFO] Certificates check : OK !", false);
                else
                    window?.Writeline("[ERROR] Certificates check : WARNING !", false);
            }));

            threads.Add(new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_QFE = Win32_QuickFixEngineering.GetQuickFixEngineering();

                if (Win32_QFE != null)
                    window?.Writeline("[INFO] Updates check : OK !", false);
                else
                    window?.Writeline("[ERROR] Updates check : WARNING !", false);
            }));

            threads.Add(new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Accounts = Account.GetLocalUsers();

                if (Accounts != null)
                    window?.Writeline("[INFO] Admins check : OK !", false);
                else
                    window?.Writeline("[ERROR] Admins check : WARNING !", false);
            }));

            threads.Add(new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Sysinfo = SystemInfo.GetSystemInfo();
                if (Sysinfo != null)
                    window?.Writeline("[INFO] System info check : OK !", false);
                else
                    window?.Writeline("[ERROR] System info check : WARNING !", false);
            }));

            threads.Add(new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Startups = Startup.GetStartupApps();
                if (Startups != null)
                    window?.Writeline("[INFO] Startup info check : OK !", false);
                else
                    window?.Writeline("[ERROR] Startup info check : WARNING !", false);
            }));


            foreach (var item in threads) item.Start();
            foreach (var item in threads) item.Join();

            var report = new Report(
                Bios,
                win32_EncryptableVolumes,
                Win32_Tpm,
                Win32_Products,
                X509CertList,
                Win32_QFE,
                Accounts,
                Sysinfo,
                Startups
            );

#pragma warning disable CS8604 
            Report.GenerateReport(report, FileName);
#pragma warning restore CS8604

            window?.Writeline("[INFO] Reporting job finished !", false);
        });
        ReportingThread.Start();
    }

    #endregion
}