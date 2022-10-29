using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable CA1416 // Valider la compatibilité de la plateforme


namespace Win32ClassModule
{
    internal class Win32_Product
    {
        private UInt16 AssignmentType;
        private string Caption;
        private string Description;
        private string IdentifyingNumber;
        private string InstallDate;
        private string InstallDate2;
        private string InstallLocation;
        private Int16 InstallState;
        private string HelpLink;
        private string HelpTelephone;
        private string InstallSource;
        private string Language;
        private string LocalPackage;
        private string Name;
        private string PackageCache;
        private string PackageCode;
        private string PackageName;
        private string ProductID;
        private string RegOwner;
        private string RegCompany;
        private string SKUNumber;
        private string Transforms;
        private string URLInfoAbout;
        private string URLUpdateInfo;
        private string Vendor;
        private UInt32 WordCount;
        private string Version;

        public UInt16 GetAssignmentType => AssignmentType;
        public string GetCaption => Caption;
        public string GetDescription => Description;
        public string GetIdentifyingNumber => IdentifyingNumber;
        public string GetInstallDate => InstallDate;
        public string GetInstallDate2 => InstallDate2;
        public string GetInstallLocation => InstallLocation;
        public Int16 GetInstallState => InstallState;
        public string GetHelpLink => HelpLink;
        public string GetHelpTelephone => HelpTelephone;
        public string GetInstallSource => InstallSource;
        public string GetLanguage => Language;
        public string GetLocalPackage => LocalPackage;
        public string GetName => Name;
        public string GetPackageCache => PackageCache;
        public string GetPackageCode => PackageCode;
        public string GetPackageName => PackageName;
        public string GetProductID => ProductID;
        public string GetRegOwner => RegOwner;
        public string GetRegCompany => RegCompany;
        public string GetSKUNumber => SKUNumber;
        public string GetTransforms => Transforms;
        public string GetURLInfoAbout => URLInfoAbout;
        public string GetURLUpdateInfo => URLUpdateInfo;
        public string GetVendor => Vendor;
        public UInt32 GetWordCount => WordCount;
        public string GetVersion => Version;

        public Win32_Product(ushort assignmentType, string caption, string description, 
            string identifyingNumber, string installDate, string installDate2, 
            string installLocation, Int16 installState, string helpLink, 
            string helpTelephone, string installSource, string language, 
            string localPackage, string name, string packageCache, 
            string packageCode, string packageName, string productID, 
            string regOwner, string regCompany, string sKUNumber, 
            string transforms, string uRLInfoAbout, string uRLUpdateInfo, 
            string vendor, uint wordCount, string version)
        {
            AssignmentType = assignmentType;
            Caption = caption;
            Description = description;
            IdentifyingNumber = identifyingNumber;
            InstallDate = installDate;
            InstallDate2 = installDate2;
            InstallLocation = installLocation;
            InstallState = installState;
            HelpLink = helpLink;
            HelpTelephone = helpTelephone;
            InstallSource = installSource;
            Language = language;
            LocalPackage = localPackage;
            Name = name;
            PackageCache = packageCache;
            PackageCode = packageCode;
            PackageName = packageName;
            ProductID = productID;
            RegOwner = regOwner;
            RegCompany = regCompany;
            SKUNumber = sKUNumber;
            Transforms = transforms;
            URLInfoAbout = uRLInfoAbout;
            URLUpdateInfo = uRLUpdateInfo;
            Vendor = vendor;
            WordCount = wordCount;
            Version = version;
        }

        public static List<Win32_Product> GetProducts()
        {
            List<Win32_Product> products = new List<Win32_Product>();

            ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Product");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                Win32_Product win32_Products =
                    new Win32_Product(

                (UInt16)queryObj["AssignmentType"],

                !string.IsNullOrEmpty((string)queryObj["Caption"])
                ? (string)queryObj["Caption"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["Description"])
                ? (string)queryObj["Description"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["IdentifyingNumber"])
                ? (string)queryObj["IdentifyingNumber"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["InstallDate"])
                ? (string)queryObj["InstallDate"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["InstallDate2"])
                ? (string)queryObj["InstallDate2"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["InstallLocation"])
                ? (string)queryObj["InstallLocation"]
                : "N/A",
                (Int16)queryObj["InstallState"],
                !string.IsNullOrEmpty((string)queryObj["HelpLink"])
                ? (string)queryObj["HelpLink"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["HelpTelephone"])
                ? (string)queryObj["HelpTelephone"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["InstallSource"])
                ? (string)queryObj["InstallSource"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["Language"])
                ? (string)queryObj["Language"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["LocalPackage"])
                ? (string)queryObj["LocalPackage"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["Name"])
                ? (string)queryObj["Name"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["PackageCache"])
                ? (string)queryObj["PackageCache"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["PackageCode"])
                ? (string)queryObj["PackageCode"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["PackageName"])
                ? (string)queryObj["PackageName"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["ProductID"])
                ? (string)queryObj["ProductID"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["RegOwner"])
                ? (string)queryObj["RegOwner"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["RegCompany"])
                ? (string)queryObj["RegCompany"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["SKUNumber"])
                ? (string)queryObj["SKUNumber"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["Transforms"])
                ? (string)queryObj["Transforms"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["URLInfoAbout"])
                ? (string)queryObj["URLInfoAbout"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["URLUpdateInfo"])
                ? (string)queryObj["URLUpdateInfo"]
                : "N/A",
                !string.IsNullOrEmpty((string)queryObj["Vendor"])
                ? (string)queryObj["Vendor"]
                : "N/A",
                (UInt32)queryObj["WordCount"],

                !string.IsNullOrEmpty((string)queryObj["Version"])
                ? (string)queryObj["Version"]
                : "N/A"
                );

                products.Add(win32_Products);

            }
            return products;
        
        }
    }
}
