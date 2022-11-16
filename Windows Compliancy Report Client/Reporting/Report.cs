using System.Net;
using Newtonsoft.Json;

namespace Windows_Compliancy_Report_Client;

internal class Report
{
    public Report(List<Win32_Bios>? win32_Bios, List<Win32_EncryptableVolume>? win32_EncryptableVolumes,
        List<Win32_Tpm>? win32_Tpms,
        List<Win32_Product>? win32_Products, List<X509Cert>? x509Certs,
        List<Win32_QuickFixEngineering>? win32_QuickFixEngineerings,
        List<Account>? accounts, SystemInfo? systemInfo, List<Startup>? startup)
    {
        Win32_Bios = win32_Bios;
        Win32_EncryptableVolumes = win32_EncryptableVolumes;
        Win32_Tpms = win32_Tpms;
        Win32_Products = win32_Products;
        X509Certs = x509Certs;
        Win32_QuickFixEngineerings = win32_QuickFixEngineerings;
        Accounts = accounts;
        SystemInfo = systemInfo;
        Startup = startup;
    }

    public List<Win32_Bios>? Win32_Bios { get; }
    public List<Win32_EncryptableVolume>? Win32_EncryptableVolumes { get; }
    public List<Win32_Tpm>? Win32_Tpms { get; }
    public List<Win32_Product>? Win32_Products { get; }
    public List<X509Cert>? X509Certs { get; }
    public List<Win32_QuickFixEngineering>? Win32_QuickFixEngineerings { get; }
    public List<Account>? Accounts { get; }
    public SystemInfo? SystemInfo { get; }
    public List<Startup>? Startup { get; }


    public static void GenerateReport(Report report, string FileName)
    {
        if (Dns.GetHostName() != null) WriteToJsonFile(FileName, report);
    }

    public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false)
    {
        TextWriter? writer = null;
        try
        {
            var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
            writer = new StreamWriter(filePath, append);
            writer.Write(contentsToWriteToFile);
            writer.Flush();
            writer.Close();
        }
        catch (Exception e)
        {
            Program.window?.Writeline("Report exception : \n" + e.Message, false);
            if (writer != null) writer.Close();
        }
    }
}