using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Management;

#pragma warning disable CA1416 // Valider la compatibilité de la plateforme


internal class Win32_Product
{
    public Win32_Product(ushort assignmentType, string caption, string description,
        string identifyingNumber, string installDate, string installDate2,
        string installLocation, short installState, string helpLink,
        string helpTelephone, string installSource, string language,
        string localPackage, string name, string packageCache,
        string packageCode, string packageName, string productID,
        string regOwner, string regCompany, string sKUNumber,
        string transforms, string uRLInfoAbout, string uRLUpdateInfo,
        string vendor, string version)
    {
        GetAssignmentType = assignmentType;
        GetCaption = caption;
        GetDescription = description;
        GetIdentifyingNumber = identifyingNumber;
        GetInstallDate = installDate;
        GetInstallDate2 = installDate2;
        GetInstallLocation = installLocation;
        GetInstallState = installState;
        GetHelpLink = helpLink;
        GetHelpTelephone = helpTelephone;
        GetInstallSource = installSource;
        GetLanguage = language;
        GetLocalPackage = localPackage;
        GetName = name;
        GetPackageCache = packageCache;
        GetPackageCode = packageCode;
        GetPackageName = packageName;
        GetProductID = productID;
        GetRegOwner = regOwner;
        GetRegCompany = regCompany;
        GetSKUNumber = sKUNumber;
        GetTransforms = transforms;
        GetURLInfoAbout = uRLInfoAbout;
        GetURLUpdateInfo = uRLUpdateInfo;
        GetVendor = vendor;

        GetVersion = version;
    }

    public ushort GetAssignmentType { get; }

    public string GetCaption { get; }

    public string GetDescription { get; }

    public string GetIdentifyingNumber { get; }

    public string GetInstallDate { get; }

    public string GetInstallDate2 { get; }

    public string GetInstallLocation { get; }

    public short GetInstallState { get; }

    public string GetHelpLink { get; }
        
    public string GetHelpTelephone { get; }

    public string GetInstallSource { get; }

    public string GetLanguage { get; }

    public string GetLocalPackage { get; }

    public string GetName { get; }

    public string GetPackageCache { get; }

    public string GetPackageCode { get; }

    public string GetPackageName { get; }

    public string GetProductID { get; }

    public string GetRegOwner { get; }

    public string GetRegCompany { get; }

    public string GetSKUNumber { get; }

    public string GetTransforms { get; }

    public string GetURLInfoAbout { get; }

    public string GetURLUpdateInfo { get; }

    public string GetVendor { get; }

    public string GetVersion { get; }

    public static List<Win32_Product> GetProducts()
    {
        try
        {
            var products = new List<Win32_Product>();

            var searcher =
                new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT * FROM Win32_Product");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                try
                {
                    var win32_Products =
                   new Win32_Product(
                       (ushort)queryObj["AssignmentType"],
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
                       (short)queryObj["InstallState"],
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
                       !string.IsNullOrEmpty((string)queryObj["Version"])
                           ? (string)queryObj["Version"]
                           : "N/A"
                   );

                    products.Add(win32_Products);
                }
               catch(Exception ex)
                {
                    WCRC.log.LogWrite("Internal error on products...");
                    WCRC.log.LogWrite(ex.Message);

                }
            }

            return products;
        }
        catch (Exception ex)
        {
            WCRC.log.LogWrite("Critical error on products...");
            WCRC.log.LogWrite(ex.Message);

            return null;
        }
    }
}