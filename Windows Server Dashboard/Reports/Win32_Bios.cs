
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

    
}