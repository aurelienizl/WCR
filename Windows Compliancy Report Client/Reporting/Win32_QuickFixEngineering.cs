using System.Management;

#pragma warning disable CA1416 // Valider la compatibilité de la plateforme


namespace Windows_Compliancy_Report_Client;

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

    public static List<Win32_QuickFixEngineering>? GetQuickFixEngineering()
    {
        try
        {
            var list = new List<Win32_QuickFixEngineering>();

            var searcher =
                new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_QuickFixEngineering");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                list.Add(
            new Win32_QuickFixEngineering(
                !string.IsNullOrEmpty((string)queryObj["Caption"])
                    ? (string)queryObj["Caption"]
                    : "N/A",
                !string.IsNullOrEmpty((string)queryObj["Description"])
                    ? (string)queryObj["Description"]
                    : "N/A",
                !string.IsNullOrEmpty((string)queryObj["InstallDate"])
                    ? (string)queryObj["InstallDate"]
                    : "N/A",
                !string.IsNullOrEmpty((string)queryObj["Name"])
                    ? (string)queryObj["Name"]
                    : "N/A",
                !string.IsNullOrEmpty((string)queryObj["Status"])
                    ? (string)queryObj["Status"]
                    : "N/A",
                !string.IsNullOrEmpty((string)queryObj["CSName"])
                    ? (string)queryObj["CSName"]
                    : "N/A",
                !string.IsNullOrEmpty((string)queryObj["FixComments"])
                    ? (string)queryObj["FixComments"]
                    : "N/A",
                !string.IsNullOrEmpty((string)queryObj["HotFixID"])
                    ? (string)queryObj["HotFixID"]
                    : "N/A",
                !string.IsNullOrEmpty((string)queryObj["InstalledBy"])
                    ? (string)queryObj["InstalledBy"]
                    : "N/A",
                !string.IsNullOrEmpty((string)queryObj["InstalledOn"])
                    ? (string)queryObj["InstalledOn"]
                    : "N/A",
                !string.IsNullOrEmpty((string)queryObj["ServicePackInEffect"])
                    ? (string)queryObj["ServicePackInEffect"]
                    : "N/A"
            )
        );
            }
        
            return list;
        }
        catch (Exception e)
        {
            Program.window?.Writeline("Updates exception : \n" + e.Message, false);
            return null;
        }
    }
}