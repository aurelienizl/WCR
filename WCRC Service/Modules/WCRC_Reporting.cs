using System.Collections.Generic;
using System.Net;
using System.Threading;
using WCRC_Service.Modules;
using WCRC_Service.Reporting;

internal class WCRC_Reporting
{
    #region report

    private static List<Win32_Bios> Win32_Bios_ { get; set; }
    private static List<Win32_EncryptableVolume> Win32_EncryptableVolumes_ { get; set; }
    private static List<Win32_Tpm> Win32_Tpms_ { get; set; }
    private static List<Win32_Product> Win32_Products_ { get; set; }
    private static List<Win32_X509Cert> Win32_X509Cert_ { get; set; }
    private static List<Win32_QuickFixEngineering> Win32_QuickFixEngineerings_ { get; set; }
    private static List<Win32_Account> Win32_Accounts_ { get; set; }
    private static Win32_SystemInfo Win32_SystemInfo_ { get; set; }
    private static List<Win32_Startup> Win32_Startup_ { get; set; }
    private static List<Win32_Defender> Win32_Defender_ { get; set; }
    private static List<Win32_Software> Win32_Softwares_ { get; set; }


    public static void LaunchReport()
    {
        Thread.CurrentThread.IsBackground = true;

        var threads = new List<Thread>
        {
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_Bios_ = Win32_Bios.GetBios();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_EncryptableVolumes_ = Win32_EncryptableVolume.GetEncryptableVolume();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_Tpms_ = Win32_Tpm.GetTpm();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_Products_ = Win32_Product.GetProducts();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_X509Cert_ = Win32_X509Cert.GetX509Cert();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_QuickFixEngineerings_ = Win32_QuickFixEngineering.GetQuickFixEngineering();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_Accounts_ = Win32_Account.GetLocalUsers();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_SystemInfo_ = Win32_SystemInfo.GetSystemInfo();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_Startup_ = Win32_Startup.GetStartupApps();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_Defender_ = Win32_Defender.GetWin32_Defenders();
            }),

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Win32_Softwares_ = Win32_Software.GetInstalledApps();
            })
        };

        Logs.LogWrite("Starting WMI Queries...");

        foreach (var item in threads) item.Start();
        foreach (var item in threads) item.Join();

        Logs.LogWrite("WMI Queries finished");

        var report = new Win32_Report(
            Win32_Bios_,
            Win32_EncryptableVolumes_,
            Win32_Tpms_,
            Win32_Products_,
            Win32_X509Cert_,
            Win32_QuickFixEngineerings_,
            Win32_Accounts_,
            Win32_SystemInfo_,
            Win32_Startup_,
            Win32_Defender_,
            Win32_Softwares_
        );

        Logs.LogWrite("Serializing data...");
        Win32_Report.GenerateReport(report, @"C:\Windows\" + Dns.GetHostName() + ".json");
        Logs.LogWrite("Data serialized, report generated");
    }

    #endregion
}