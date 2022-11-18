

namespace Windows_Server_Dashboard.Reports
{
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

        public string? GetCaption { get; set; }

        public string? GetDescription { get; set; }

        public string? GetInstallDate { get; set; }

        public string? GetName { get; set; }

        public string? GetStatus { get; set; }

        public string? GetCSName { get; set; }

        public string? GetFixComments { get; set; }

        public string? GetHotFixID { get; set; }

        public string? GetInstalledBy { get; set; }

        public string? GetInstalledOn { get; set; }

        public string? GetServicePackInEffect { get; set; }



    }
}