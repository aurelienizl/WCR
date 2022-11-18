using System.Net;
using Newtonsoft.Json;

namespace Windows_Server_Dashboard.Reports;

internal class Win32_Report
{
    public Win32_Report(List<Win32_Bios>? win32_Bios, List<Win32_EncryptableVolume>? win32_EncryptableVolumes,
        List<Win32_Tpm>? win32_Tpms,
        List<Win32_Product>? win32_Products, List<Win32_X509Cert>? x509Certs,
        List<Win32_QuickFixEngineering>? win32_QuickFixEngineerings,
        List<Win32_Accounts>? accounts, Win32_SystemInfo? systemInfo, List<Win32_Startup>? startup)
    {
        Win32_Bios = win32_Bios;
        Win32_EncryptableVolumes = win32_EncryptableVolumes;
        Win32_Tpms = win32_Tpms;
        Win32_Products = win32_Products;
        Win32_X509Certs = x509Certs;
        Win32_QuickFixEngineerings = win32_QuickFixEngineerings;
        Win32_Accounts = accounts;
        Win32_SystemInfo = systemInfo;
        Win32_Startup = startup;
    }

    public List<Win32_Bios>? Win32_Bios { get; set; }
    public List<Win32_EncryptableVolume>? Win32_EncryptableVolumes { get; set; }
    public List<Win32_Tpm>? Win32_Tpms { get; set; }
    public List<Win32_Product>? Win32_Products { get; set; }
    public List<Win32_X509Cert>? Win32_X509Certs { get; set; }
    public List<Win32_QuickFixEngineering>? Win32_QuickFixEngineerings { get; set; }
    public List<Win32_Accounts>? Win32_Accounts { get; set; }
    public Win32_SystemInfo? Win32_SystemInfo { get; set; }
    public List<Win32_Startup>? Win32_Startup { get; set; }


}