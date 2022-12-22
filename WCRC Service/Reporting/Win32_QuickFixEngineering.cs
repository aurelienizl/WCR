using System;
using System.Collections.Generic;
using System.Management;
using WCRC_Service.Modules;

internal class Win32_QuickFixEngineering
{
    public Win32_QuickFixEngineering(string caption, string description, string installDate,
        string name, string status, string cSName, string fixComments, string hotFixID,
        string installedBy, string installedOn, string servicePackInEffect)
    {
        GetCaption = caption;
        GetDescription = description;
        GetInstallDate = installDate;
        GetName = name;
        GetStatus = status;
        GetCSName = cSName;
        GetFixComments = fixComments;
        GetHotFixID = hotFixID;
        GetInstalledBy = installedBy;
        GetInstalledOn = installedOn;
        GetServicePackInEffect = servicePackInEffect;
    }

    public string GetCaption { get; }

    public string GetDescription { get; }

    public string GetInstallDate { get; }

    public string GetName { get; }

    public string GetStatus { get; }

    public string GetCSName { get; }

    public string GetFixComments { get; }

    public string GetHotFixID { get; }

    public string GetInstalledBy { get; }

    public string GetInstalledOn { get; }

    public string GetServicePackInEffect { get; }

    public static string QuerySafeGetter(ManagementObject obj, string query)
    {
        try
        {
            if (!String.IsNullOrEmpty(obj[query].ToString()))
            {
                return obj[query].ToString();
            }

            string res = (string)obj[query];
            if (!String.IsNullOrEmpty(res))
            {
                return res;
            }
            return "N/A";
        }
        catch (Exception)
        {
            return "N/A";
        }
    }

    public static List<Win32_QuickFixEngineering> GetQuickFixEngineering()
    {
        var list = new List<Win32_QuickFixEngineering>();

        try
        {

            var searcher =
                new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_QuickFixEngineering");

            foreach (var o in searcher.Get())
            {
                var queryObj = (ManagementObject)o;
                try
                {
                    list.Add(
                        new Win32_QuickFixEngineering(
                            QuerySafeGetter(queryObj, "Caption"),
                            QuerySafeGetter(queryObj, "Description"),
                            QuerySafeGetter(queryObj, "InstallDate"),
                            QuerySafeGetter(queryObj, "Name"),
                            QuerySafeGetter(queryObj, "Status"),
                            QuerySafeGetter(queryObj, "CSName"),
                            QuerySafeGetter(queryObj, "FixComments"),
                            QuerySafeGetter(queryObj, "HotFixID"),
                            QuerySafeGetter(queryObj, "InstalledBy"),
                            QuerySafeGetter(queryObj, "InstalledOn"),
                            QuerySafeGetter(queryObj, "ServicePackInEffect")                        
                        )
                    );
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            Logs.LogWrite("Got qfe successfully");

            return list;
        }
        catch (Exception)
        {
            Logs.LogWrite("Error : qfe");

            return list;
        }
    }
}
