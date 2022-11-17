using System.Management;

#pragma warning disable CA1416 // Valider la compatibilité de la plateforme

namespace Windows_Compliancy_Report_Client.Reporting;

internal class Win32_Bios
{
    public Win32_Bios(ushort[] biosCharacteristics, string[] bIOSVersion, string buildNumber, string caption,
        string codeSet,
        string currentLanguage, string description, byte embeddedControllerMajorVersion,
        byte embeddedControllerMinorVersion,
        string identificationCode, ushort installableLanguages, string installDate, string languageEdition,
        string[] listOfLanguages,
        string manufacturer, string name, string otherTargetOS, bool primaryBIOS, string releaseDate,
        string serialNumber,
        string sMBIOSBIOSVersion, ushort sMBIOSMajorVersion, ushort sMBIOSMinorVersion, bool sMBIOSPresent,
        string softwareElementID,
        ushort softwareElementState, string status, byte systemBiosMajorVersion, byte systemBiosMinorVersion,
        ushort targetOperatingSystem, string version)
    {
        GetBiosCharacteristics = biosCharacteristics;
        GetBIOSVersion = bIOSVersion;
        GetBuildNumber = buildNumber;
        GetCaption = caption;
        GetCodeSet = codeSet;
        GetCurrentLanguage = currentLanguage;
        GetDescription = description;
        GetEmbeddedControllerMajorVersion = embeddedControllerMajorVersion;
        GetEmbeddedControllerMinorVersion = embeddedControllerMinorVersion;
        GetIdentificationCode = identificationCode;
        GetInstallableLanguages = installableLanguages;
        GetInstallDate = installDate;
        GetLanguageEdition = languageEdition;
        GetListOfLanguages = listOfLanguages;
        GetManufacturer = manufacturer;
        GetName = name;
        GetOtherTargetOS = otherTargetOS;
        GetPrimaryBIOS = primaryBIOS;
        GetReleaseDate = releaseDate;
        GetSerialNumber = serialNumber;
        GetSMBIOSBIOSVersion = sMBIOSBIOSVersion;
        GetSMBIOSMajorVersion = sMBIOSMajorVersion;
        GetSMBIOSMinorVersion = sMBIOSMinorVersion;
        GetSMBIOSPresent = sMBIOSPresent;
        GetSoftwareElementID = softwareElementID;
        GetSoftwareElementState = softwareElementState;
        GetStatus = status;
        GetSystemBiosMajorVersion = systemBiosMajorVersion;
        GetSystemBiosMinorVersion = systemBiosMinorVersion;
        GetTargetOperatingSystem = targetOperatingSystem;
        GetVersion = version;
    }

    public ushort[]? GetBiosCharacteristics { get; }

    public string[]? GetBIOSVersion { get; }

    public string? GetBuildNumber { get; }

    public string? GetCaption { get; }

    public string GetCodeSet { get; }

    public string? GetCurrentLanguage { get; }

    public string? GetDescription { get; }

    public byte? GetEmbeddedControllerMajorVersion { get; }

    public byte? GetEmbeddedControllerMinorVersion { get; }

    public string? GetIdentificationCode { get; }

    public ushort? GetInstallableLanguages { get; }

    public string? GetInstallDate { get; }

    public string? GetLanguageEdition { get; }

    public string[]? GetListOfLanguages { get; }

    public string? GetManufacturer { get; }

    public string? GetName { get; }

    public string? GetOtherTargetOS { get; }

    public bool? GetPrimaryBIOS { get; }

    public string? GetReleaseDate { get; }

    public string? GetSerialNumber { get; }

    public string? GetSMBIOSBIOSVersion { get; }

    public ushort? GetSMBIOSMajorVersion { get; }

    public ushort? GetSMBIOSMinorVersion { get; }

    public bool? GetSMBIOSPresent { get; }

    public string? GetSoftwareElementID { get; }

    public ushort? GetSoftwareElementState { get; }

    public string? GetStatus { get; }

    public byte? GetSystemBiosMajorVersion { get; }

    public byte? GetSystemBiosMinorVersion { get; }

    public ushort? GetTargetOperatingSystem { get; }

    public string? GetVersion { get; }

    public static List<Win32_Bios>? GetBios()
    {
        try
        {
            var query = new ObjectQuery("SELECT * FROM Win32_BIOS");
            var search = new ManagementObjectSearcher(query);

            var moc = search.Get();

            var list = new List<Win32_Bios>();

            foreach (var mo in moc.Cast<ManagementObject>())
            {
                var bios = new Win32_Bios(
                    (ushort[])mo["BiosCharacteristics"],
                    (string[])mo["BIOSVersion"],
                    !string.IsNullOrEmpty((string)mo["BuildNumber"]) ? (string)mo["BuildNumber"] : "N/A",
                    !string.IsNullOrEmpty((string)mo["Caption"]) ? (string)mo["Caption"] : "N/A",
                    !string.IsNullOrEmpty((string)mo["CodeSet"]) ? (string)mo["CodeSet"] : "N/A",
                    !string.IsNullOrEmpty((string)mo["CurrentLanguage"]) ? (string)mo["CurrentLanguage"] : "N/A",
                    !string.IsNullOrEmpty((string)mo["Description"]) ? (string)mo["Description"] : "N/A",
                    (byte)mo["EmbeddedControllerMajorVersion"],
                    (byte)mo["EmbeddedControllerMinorVersion"],
                    !string.IsNullOrEmpty((string)mo["IdentificationCode"]) ? (string)mo["IdentificationCode"] : "N/A",
                    (ushort)mo["InstallableLanguages"],
                    !string.IsNullOrEmpty((string)mo["InstallDate"]) ? (string)mo["InstallDate"] : "N/A",
                    !string.IsNullOrEmpty((string)mo["LanguageEdition"]) ? (string)mo["LanguageEdition"] : "N/A",
                    (string[])mo["ListOfLanguages"],
                    !string.IsNullOrEmpty((string)mo["Manufacturer"]) ? (string)mo["Manufacturer"] : "N/A",
                    !string.IsNullOrEmpty((string)mo["Name"]) ? (string)mo["Name"] : "N/A",
                    !string.IsNullOrEmpty((string)mo["OtherTargetOS"]) ? (string)mo["OtherTargetOS"] : "N/A",
                    (bool)mo["PrimaryBIOS"],
                    !string.IsNullOrEmpty((string)mo["ReleaseDate"]) ? (string)mo["ReleaseDate"] : "N/A",
                    !string.IsNullOrEmpty((string)mo["SerialNumber"]) ? (string)mo["SerialNumber"] : "N/A",
                    !string.IsNullOrEmpty((string)mo["SMBIOSBIOSVersion"]) ? (string)mo["SMBIOSBIOSVersion"] : "N/A",
                    (ushort)mo["SMBIOSMajorVersion"],
                    (ushort)mo["SMBIOSMinorVersion"],
                    (bool)mo["SMBIOSPresent"],
                    !string.IsNullOrEmpty((string)mo["SoftwareElementID"]) ? (string)mo["SoftwareElementID"] : "N/A",
                    (ushort)mo["SoftwareElementState"],
                    !string.IsNullOrEmpty((string)mo["Status"]) ? (string)mo["Status"] : "N/A",
                    (byte)mo["SystemBiosMajorVersion"],
                    (byte)mo["SystemBiosMinorVersion"],
                    (ushort)mo["TargetOperatingSystem"],
                    !string.IsNullOrEmpty((string)mo["Version"]) ? (string)mo["Version"] : "N/A"
                );

                list.Add(bios);
            }

            return list;
        }
        catch (Exception e)
        {
            Program.window?.Writeline("Bios exception : \n" + e.Message, false);
            return null;
        }
    }
}