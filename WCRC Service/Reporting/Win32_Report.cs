using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using WCRC_Service.Reporting;

internal class Win32_Report
{
    public Win32_Report(List<Win32_Bios> win32_Bios, List<Win32_EncryptableVolume> win32_EncryptableVolumes,
        List<Win32_Tpm> win32_Tpms,
        List<Win32_Product> win32_Products, List<Win32_X509Cert> win32_x509Certs,
        List<Win32_QuickFixEngineering> win32_QuickFixEngineerings,
        List<Win32_Account> win32_Accounts, Win32_SystemInfo win32_SystemInfo, List<Win32_Startup> win32_Startup, List<Win32_Defender> win32_Defenders,
        List<Win32_Software> win32_Softwares)
    {
        Win32_Bios = win32_Bios;
        Win32_EncryptableVolumes = win32_EncryptableVolumes;
        Win32_Tpms = win32_Tpms;
        Win32_Products = win32_Products;
        Win32_X509Certs = win32_x509Certs;
        Win32_QuickFixEngineerings = win32_QuickFixEngineerings;
        Win32_Accounts = win32_Accounts;
        Win32_SystemInfo = win32_SystemInfo;
        Win32_Startup = win32_Startup;
        Win32_Defender = win32_Defenders;
        Win32_Software = win32_Softwares;
    }

    public List<Win32_Bios> Win32_Bios { get; }
    public List<Win32_EncryptableVolume> Win32_EncryptableVolumes { get; }
    public List<Win32_Tpm> Win32_Tpms { get; }
    public List<Win32_Product> Win32_Products { get; }
    public List<Win32_X509Cert> Win32_X509Certs { get; }
    public List<Win32_QuickFixEngineering> Win32_QuickFixEngineerings { get; }
    public List<Win32_Account> Win32_Accounts { get; }
    public Win32_SystemInfo Win32_SystemInfo { get; }
    public List<Win32_Startup> Win32_Startup { get; }
    public List<Win32_Software> Win32_Software { get; }
    public List<Win32_Defender> Win32_Defender { get; }

    public static void GenerateReport(Win32_Report report, string FileName)
    {
        if (Dns.GetHostName() != null) WriteToJsonFile(FileName, report);
    }

    public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false)
    {
        TextWriter writer = null;
        try
        {
            var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
            writer = new StreamWriter(filePath, append);
            writer.Write(contentsToWriteToFile);
            writer.Flush();
            writer.Close();
        }
        catch (Exception)
        {
            if (writer != null) writer.Close();
        }
    }
}