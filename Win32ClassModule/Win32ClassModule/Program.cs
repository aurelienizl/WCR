using System.Runtime.CompilerServices;
using Win32ClassModule.System;
using Win32ClassModule.Win32_Class;
using Win32ClassModule.Win32_Modules;

namespace Win32ClassModule;

public static class Program
{
    public static void Main()
    {
        InitReportingTool();
    }

    public static void InitReportingTool()
    {
        var bios = Win32_Bios.GetBios();
        if (bios != null) PrintData.PrintBios(bios);

        var win32_EncryptableVolumes =
            Win32_EncryptableVolume.GetEncryptableVolume();
        if (win32_EncryptableVolumes != null) PrintData.PrintEncryptableVolume(win32_EncryptableVolumes);

        var win32_Tpm = Win32_Tpm.GetTpm();
        if (win32_Tpm != null) PrintData.PrintTpm(win32_Tpm);

        var win32_Products = Win32_Product.GetProducts();
        if (win32_Products != null) PrintData.PrintProducts(win32_Products);

        var X509CertList = X509Cert.GetX509Cert();
        if (X509CertList != null) PrintData.PrintCerts(X509CertList);

        var win32_QFE = Win32_QuickFixEngineering.GetQuickFixEngineering();
        if (win32_QFE != null) PrintData.printQFE(win32_QFE);

        var accounts = Account.GetLocalUsers();
        if (accounts != null) PrintData.PrintAccounts(accounts);

        var sysinfo = SystemInfo.GetSystemInfo();
        if (sysinfo != null) PrintData.PrintSystemInfo(sysinfo);

        var report = new Report(
            bios,
            win32_EncryptableVolumes,
            win32_Tpm,
            win32_Products,
            X509CertList,
            win32_QFE,
            accounts,
            sysinfo
        );

        Report.GenerateReport(report);
    }
}