using System.Management;
using System.Runtime.CompilerServices;


#pragma warning disable CA1416 // Valider la compatibilité de la plateforme


namespace Win32ClassModule.Win32_Class
{
    internal class Win32_QuickFixEngineering
    {
        private string Caption;
        private string Description;
        private string InstallDate;
        private string Name;
        private string Status;
        private string CSName;
        private string FixComments;
        private string HotFixID;
        private string InstalledBy;
        private string InstalledOn;
        private string ServicePackInEffect;

        public string GetCaption => Caption;
        public string GetDescription => Description;
        public string GetInstallDate => InstallDate;
        public string GetName => Name;
        public string GetStatus => Status;
        public string GetCSName => CSName;
        public string GetFixComments => FixComments;
        public string GetHotFixID => HotFixID;
        public string GetInstalledBy => InstalledBy;
        public string GetInstalledOn => InstalledOn;
        public string GetServicePackInEffect => ServicePackInEffect;

        public Win32_QuickFixEngineering(string caption, string description, string installDate,
            string name, string status, string cSName, string fixComments, string hotFixID,
            string installedBy, string installedOn, string servicePackInEffect)
        {
            Caption = caption;
            Description = description;
            InstallDate = installDate;
            Name = name;
            Status = status;
            CSName = cSName;
            FixComments = fixComments;
            HotFixID = hotFixID;
            InstalledBy = installedBy;
            InstalledOn = installedOn;
            ServicePackInEffect = servicePackInEffect;
        }

        public static List<Win32_QuickFixEngineering>? GetQuickFixEngineering()
        {
            try
            {
                List<Win32_QuickFixEngineering> list = new List<Win32_QuickFixEngineering>();

                ManagementObjectSearcher searcher =
                        new ManagementObjectSearcher("root\\CIMV2",
                        "SELECT * FROM Win32_QuickFixEngineering");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    list.Add(
                        new Win32_QuickFixEngineering(
                            !String.IsNullOrEmpty((string)queryObj["Caption"])
                            ? (string)queryObj["Caption"]
                            : "N/A",
                            !String.IsNullOrEmpty((string)queryObj["Description"])
                            ? (string)queryObj["Description"]
                            : "N/A",
                            !String.IsNullOrEmpty((string)queryObj["InstallDate"])
                            ? (string)queryObj["InstallDate"]
                            : "N/A",
                            !String.IsNullOrEmpty((string)queryObj["Name"])
                            ? (string)queryObj["Name"]
                            : "N/A",
                            !String.IsNullOrEmpty((string)queryObj["Status"])
                            ? (string)queryObj["Status"]
                            : "N/A",
                            !String.IsNullOrEmpty((string)queryObj["CSName"])
                            ? (string)queryObj["CSName"]
                            : "N/A",
                            !String.IsNullOrEmpty((string)queryObj["FixComments"])
                            ? (string)queryObj["FixComments"]
                            : "N/A",
                            !String.IsNullOrEmpty((string)queryObj["HotFixID"])
                            ? (string)queryObj["HotFixID"]
                            : "N/A",
                            !String.IsNullOrEmpty((string)queryObj["InstalledBy"])
                            ? (string)queryObj["InstalledBy"]
                            : "N/A",
                            !String.IsNullOrEmpty((string)queryObj["InstalledOn"])
                            ? (string)queryObj["InstalledOn"]
                            : "N/A",
                            !String.IsNullOrEmpty((string)queryObj["ServicePackInEffect"])
                            ? (string)queryObj["ServicePackInEffect"]
                            : "N/A"
                            )
                        );
                }
                return list;
            }
            catch (Exception e)
            {

                return null;
            }

            
        }
    }
}
