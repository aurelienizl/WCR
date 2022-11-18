
namespace Windows_Server_Dashboard.Reports;

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

    public ushort[]? GetBiosCharacteristics { get; set; }

    public string[]? GetBIOSVersion { get; set; }

    public string? GetBuildNumber { get; set; }

    public string? GetCaption { get; set; }

    public string GetCodeSet { get; set; }

    public string? GetCurrentLanguage { get; set; }

    public string? GetDescription { get; set; }

    public byte? GetEmbeddedControllerMajorVersion { get; set; }

    public byte? GetEmbeddedControllerMinorVersion { get; set; }

    public string? GetIdentificationCode { get; set; }

    public ushort? GetInstallableLanguages { get; set; }

    public string? GetInstallDate { get; set; }

    public string? GetLanguageEdition { get; set; }

    public string[]? GetListOfLanguages { get; set; }

    public string? GetManufacturer { get; set; }

    public string? GetName { get; set; }

    public string? GetOtherTargetOS { get; set; }

    public bool? GetPrimaryBIOS { get; set; }

    public string? GetReleaseDate { get; set; }

    public string? GetSerialNumber { get; set; }

    public string? GetSMBIOSBIOSVersion { get; set; }

    public ushort? GetSMBIOSMajorVersion { get; set; }

    public ushort? GetSMBIOSMinorVersion { get; set; }

    public bool? GetSMBIOSPresent { get; set; }

    public string? GetSoftwareElementID { get; set; }

    public ushort? GetSoftwareElementState { get; set; }

    public string? GetStatus { get; set; }

    public byte? GetSystemBiosMajorVersion { get; set; }

    public byte? GetSystemBiosMinorVersion { get; set; }

    public ushort? GetTargetOperatingSystem { get; set; }

    public string? GetVersion { get; set; }


}