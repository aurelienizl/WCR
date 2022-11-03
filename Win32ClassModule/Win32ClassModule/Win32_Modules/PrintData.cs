using Win32ClassModule.System;
using Win32ClassModule.Win32_Class;

namespace Win32ClassModule.Win32_Modules;

internal class PrintData
{
    public static void PrintTpm(List<Win32_Tpm> win32_tpmList)
    {
        foreach (var win32_tpm in win32_tpmList)
        {
            Console.WriteLine("Tpm is activated : " + win32_tpm.GetIsActivated_InitialValue);
            Console.WriteLine("Tpm is enabled : " + win32_tpm.GetIsEnabled_InitialValue);
            Console.WriteLine("Tpm is owned : " + win32_tpm.GetIsOwned_InitialValue);
            Console.WriteLine("Tpm spec version : " + win32_tpm.GetSpecVersion);
            Console.WriteLine("Tpm Manufacturer version : " + win32_tpm.GetManufacturerVersion);
            Console.WriteLine("Tpm Manufacturer version info : " + win32_tpm.GetManufacturerVersionInfo);
            Console.WriteLine("Tpm Manufacturer id : " + win32_tpm.GetManufacturerId);
            Console.WriteLine("Tpm physical presence version : " + win32_tpm.GetPhysicalPresenceVersionInfo);
            Console.WriteLine("**********************************************************");
        }
    }

    public static void PrintProducts(List<Win32_Product> win32_Products)
    {
        foreach (var elt in win32_Products)
        {
            Console.WriteLine("Assignment type : " + elt.GetAssignmentType);
            Console.WriteLine("Caption : " + elt.GetCaption);
            Console.WriteLine("Description : " + elt.GetDescription);
            Console.WriteLine("Identifying Number : " + elt.GetIdentifyingNumber);
            Console.WriteLine("Install date : " + elt.GetInstallDate);
            Console.WriteLine("Install date v2 : " + elt.GetInstallDate2);
            Console.WriteLine("Installation location : " + elt.GetInstallLocation);
            Console.WriteLine("Installation state : " + elt.GetInstallState);
            Console.WriteLine("Help link : " + elt.GetHelpLink);
            Console.WriteLine("Help phone number : " + elt.GetHelpTelephone);
            Console.WriteLine("Installation source : " + elt.GetInstallSource);
            Console.WriteLine("Language : " + elt.GetLanguage);
            Console.WriteLine("Local package : " + elt.GetLocalPackage);
            Console.WriteLine("Name : " + elt.GetName);
            Console.WriteLine("Package cache : " + elt.GetPackageCache);
            Console.WriteLine("Package code : " + elt.GetPackageCode);
            Console.WriteLine("Package name : " + elt.GetPackageName);
            Console.WriteLine("Product id : " + elt.GetProductID);
            Console.WriteLine("Reg Owner : " + elt.GetRegOwner);
            Console.WriteLine("Reg compagny : " + elt.GetRegCompany);
            Console.WriteLine("SKU Number : " + elt.GetSKUNumber);
            Console.WriteLine("Transforms : " + elt.GetTransforms);
            Console.WriteLine("Url info abour : " + elt.GetURLInfoAbout);
            Console.WriteLine("Url update info : " + elt.GetURLUpdateInfo);
            Console.WriteLine("Vendor : " + elt.GetVendor);
            Console.WriteLine("Version : " + elt.GetVersion);
            Console.WriteLine("**************************************");
        }
    }

    public static void PrintBios(List<Win32_Bios> win32_Bios)
    {
        foreach (var bios in win32_Bios)
        {
            foreach (var elt in bios.GetBiosCharacteristics) Console.WriteLine("Bios characteristics " + elt);
            foreach (var elt in bios.GetBIOSVersion) Console.WriteLine("Bios versions : " + elt);
            Console.WriteLine("Build number : " + bios.GetBuildNumber);
            Console.WriteLine("Caption : " + bios.GetCaption);
            Console.WriteLine("Code set : " + bios.GetCodeSet);
            Console.WriteLine("Current language : " + bios.GetCurrentLanguage);
            Console.WriteLine("Descriptions : " + bios.GetDescription);
            Console.WriteLine("Embedded controller major version : " + bios.GetEmbeddedControllerMajorVersion);
            Console.WriteLine("Embedded controller minor version : " + bios.GetEmbeddedControllerMinorVersion);
            Console.WriteLine("Identification code : " + bios.GetIdentificationCode);
            Console.WriteLine("Installable language : " + bios.GetInstallableLanguages);
            Console.WriteLine("Installation date : " + bios.GetInstallDate);
            Console.WriteLine("Language edition : " + bios.GetLanguageEdition);
            foreach (var elt in bios.GetListOfLanguages) Console.WriteLine("Installed languages : " + elt);
            Console.WriteLine("Manifacturer : " + bios.GetManufacturer);
            Console.WriteLine("Name : " + bios.GetName);
            Console.WriteLine("Others target os : " + bios.GetOtherTargetOS);
            Console.WriteLine("Primary bios : " + bios.GetPrimaryBIOS);
            Console.WriteLine("Release date : " + bios.GetReleaseDate);
            Console.WriteLine("Serial number : " + bios.GetSerialNumber);
            Console.WriteLine("Bios version : " + bios.GetSMBIOSBIOSVersion);
            Console.WriteLine("Bios major version : " + bios.GetSMBIOSMajorVersion);
            Console.WriteLine("Bios minor version : " + bios.GetSMBIOSMinorVersion);
            Console.WriteLine("Bios is present : " + bios.GetSMBIOSPresent);
            Console.WriteLine("Software element ID " + bios.GetSoftwareElementID);
            Console.WriteLine("Software element state : " + bios.GetSoftwareElementState);
            Console.WriteLine("Bios status : " + bios.GetStatus);
            Console.WriteLine("Bios system major version : " + bios.GetSystemBiosMajorVersion);
            Console.WriteLine("Bios system minor version : " + bios.GetSystemBiosMinorVersion);
            Console.WriteLine("Target operating system : " + bios.GetTargetOperatingSystem);
            Console.WriteLine("Version : " + bios.GetVersion);
            Console.WriteLine("**********************************************************");
        }
    }

    public static void PrintEncryptableVolume(List<Win32_EncryptableVolume> win32_EncryptableVolumeList)
    {
        foreach (var win32_EncryptableVolume in win32_EncryptableVolumeList)
        {
            Console.WriteLine("Device : " + win32_EncryptableVolume.GetDeviceID);
            Console.WriteLine("Volume : " + win32_EncryptableVolume.GetPersistentVolumeID);
            Console.WriteLine("Letter : " + win32_EncryptableVolume.GetDriveLetter);
            Console.WriteLine("Status : " + win32_EncryptableVolume.GetProtectionStatus);
            Console.WriteLine("**********************************************************");
        }
    }

    public static void PrintCerts(List<X509Cert> certList)
    {
        if (certList is null) return;
        foreach (var cert in certList)
        {
            Console.WriteLine(cert.GetIssuer);
            Console.WriteLine(cert.GetSubject);
            Console.WriteLine(cert.GetExpirationDate);
            Console.WriteLine("**********************************************************");
        }
    }

    public static void printQFE(List<Win32_QuickFixEngineering> win32_QuickFixEngineerings)
    {
        foreach (var fix in win32_QuickFixEngineerings)
        {
            Console.WriteLine("Caption : " + fix.GetCaption);
            Console.WriteLine("Description : " + fix.GetDescription);
            Console.WriteLine("Install date : " + fix.GetInstallDate);
            Console.WriteLine("Name : " + fix.GetName);
            Console.WriteLine("Status : " + fix.GetStatus);
            Console.WriteLine("Cs name : " + fix.GetCSName);
            Console.WriteLine("Fix comments : " + fix.GetFixComments);
            Console.WriteLine("Hotfix id : " + fix.GetHotFixID);
            Console.WriteLine("Installed by : " + fix.GetInstalledBy);
            Console.WriteLine("Installed on : " + fix.GetInstalledOn);
            Console.WriteLine("Service pack in effect : " + fix.GetServicePackInEffect);
            Console.WriteLine("**********************************************************");
        }
    }

    public static void PrintAccounts(List<Account> accounts)
    {
        foreach (var account in accounts)
        {
            Console.WriteLine("Name : " + account.GetName);
            Console.WriteLine("Auth : " + account.GetAuthenticationType);
            Console.WriteLine("GUID : " + account.GetGuid);
            Console.WriteLine("**********************************************************");
        }
    }

    public static void PrintSystemInfo(SystemInfo systemInfo)
    {
        Console.WriteLine("System version : " + systemInfo.GetOsVersion);
        Console.WriteLine("Bios manufacturer : " + systemInfo.GetBiosManufacturer);
        Console.WriteLine("Mainboard name : " + systemInfo.GetMainboardName);
        Console.WriteLine("Cpu name : " + systemInfo.GetCpuName);
        Console.WriteLine("Gpu name : " + systemInfo.GetGpuName);
        Console.WriteLine("RAM : " + systemInfo.GetTotalPhysicalMemoryInMb);
        Console.WriteLine("Lan ip : " + systemInfo.GetLanIpAddress);
        Console.WriteLine("Mac address : " + systemInfo.GetMacAddress);
        Console.WriteLine("Reporting date : " + systemInfo.GetTimeDate);
        Console.WriteLine("Hardware id : " + systemInfo.GetHardwareID);
    }
}