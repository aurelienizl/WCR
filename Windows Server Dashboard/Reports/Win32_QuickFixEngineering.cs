

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

        public string? GetCaption { get; }

        public string? GetDescription { get; }

        public string? GetInstallDate { get; }

        public string? GetName { get; }

        public string? GetStatus { get; }

        public string? GetCSName { get; }

        public string? GetFixComments { get; }

        public string? GetHotFixID { get; }

        public string? GetInstalledBy { get; }

        public string? GetInstalledOn { get; }

        public string? GetServicePackInEffect { get; }

       
        
    }
}