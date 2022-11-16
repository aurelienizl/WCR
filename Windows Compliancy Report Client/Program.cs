using System.Net;
using System.Security.Principal;

namespace Windows_Compliancy_Report_Client;

internal static class Program
{
    /// <summary>
    ///     The main entry point for the application.
    /// </summary>
    public static string Host = "10.209.242.60";

    public static int Port = 443;
    public static string? FileName;

    public static Window? window;

    [STAThread]
    private static int Main()
    {
        // To customize application configuration such as set high DPI settings or default font,
        // see https://aka.ms/applicationconfiguration.
        FileName = Dns.GetHostName() + ".json";
        if (!IsAdministrator() || FileName is null)
        {
            MessageBox.Show("Program needs admin rights or Hostname not found");
            return 0;
        }

        ApplicationConfiguration.Initialize();
        window = new Window();
        Application.Run(window);
        return 0;
    }

    public static bool IsAdministrator()
    {
        var identity = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(identity);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

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

    private static List<Win32_Bios>? bios { get; set; }
    private static List<Win32_EncryptableVolume>? win32_EncryptableVolumes { get; set; }
    private static List<Win32_Tpm>? win32_Tpm { get; set; }
    private static List<Win32_Product>? win32_Products { get; set; }
    private static List<X509Cert>? X509CertList { get; set; }
    private static List<Win32_QuickFixEngineering>? win32_QFE { get; set; }
    private static List<Account>? accounts { get; set; }
    private static SystemInfo? sysinfo { get; set; }
    private static List<Startup>? startups { get; set; }
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
                bios = Win32_Bios.GetBios();
                if (bios != null)
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
                win32_Tpm = Win32_Tpm.GetTpm();
                if (win32_Tpm != null)
                    window?.Writeline("[INFO] Tpm check : OK !", false);
                else
                    window?.Writeline("[ERROR] Tpm check : WARNING !", false);
            }));

            threads.Add(new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                win32_Products = Win32_Product.GetProducts();
                if (win32_Products != null)
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
                win32_QFE = Win32_QuickFixEngineering.GetQuickFixEngineering();

                if (win32_QFE != null)
                    window?.Writeline("[INFO] Updates check : OK !", false);
                else
                    window?.Writeline("[ERROR] Updates check : WARNING !", false);
            }));

            threads.Add(new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                accounts = Account.GetLocalUsers();

                if (accounts != null)
                    window?.Writeline("[INFO] Admins check : OK !", false);
                else
                    window?.Writeline("[ERROR] Admins check : WARNING !", false);
            }));

            threads.Add(new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                sysinfo = SystemInfo.GetSystemInfo();
                if (sysinfo != null)
                    window?.Writeline("[INFO] System info check : OK !", false);
                else
                    window?.Writeline("[ERROR] System info check : WARNING !", false);
            }));

            threads.Add(new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                startups = Startup.GetStartupApps();
                if (startups != null)
                    window?.Writeline("[INFO] Startup info check : OK !", false);
                else
                    window?.Writeline("[ERROR] Startup info check : WARNING !", false);
            }));


            foreach (var item in threads) item.Start();
            foreach (var item in threads) item.Join();

            var report = new Report(
                bios,
                win32_EncryptableVolumes,
                win32_Tpm,
                win32_Products,
                X509CertList,
                win32_QFE,
                accounts,
                sysinfo,
                startups
            );

#pragma warning disable CS8604 // Existence possible d'un argument de r�f�rence null.
            Report.GenerateReport(report, FileName);
#pragma warning restore CS8604 // Existence possible d'un argument de r�f�rence null.

            window?.Writeline("[INFO] Reporting job finished !", false);
        });
        ReportingThread.Start();
    }

    #endregion
}