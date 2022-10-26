
using System.Management;


#pragma warning disable CA1416 // Valider la compatibilité de la plateforme


namespace Win32ClassModule
{
    internal class Win32_Bios
    {

        private UInt16[] BiosCharacteristics;
        private string[] BIOSVersion;
        private string BuildNumber;
        private string Caption;
        private string CodeSet;
        private string CurrentLanguage;
        private string Description;
        private byte EmbeddedControllerMajorVersion;
        private byte EmbeddedControllerMinorVersion;
        private string IdentificationCode;
        private UInt16 InstallableLanguages;
        private string InstallDate;
        private string LanguageEdition;
        private string[] ListOfLanguages;
        private string Manufacturer;
        private string Name;
        private string OtherTargetOS;
        private bool PrimaryBIOS;
        private string ReleaseDate;
        private string SerialNumber;
        private string SMBIOSBIOSVersion;
        private UInt16 SMBIOSMajorVersion;
        private UInt16 SMBIOSMinorVersion;
        private bool SMBIOSPresent;
        private string SoftwareElementID;
        private UInt16 SoftwareElementState;
        private string Status;
        private byte SystemBiosMajorVersion;
        private byte SystemBiosMinorVersion;
        private UInt16 TargetOperatingSystem;
        private string Version;

        public UInt16[] GetBiosCharacteristics => BiosCharacteristics;
        public string[] GetBIOSVersion => BIOSVersion;
        public string GetBuildNumber => BuildNumber;
        public string GetCaption => Caption;
        public string GetCodeSet => CodeSet;
        public string GetCurrentLanguage => CurrentLanguage;
        public string GetDescription => Description;
        public byte GetEmbeddedControllerMajorVersion => EmbeddedControllerMajorVersion;
        public byte GetEmbeddedControllerMinorVersion => EmbeddedControllerMinorVersion;
        public string GetIdentificationCode => IdentificationCode;
        public UInt16 GetInstallableLanguages => InstallableLanguages;
        public string GetInstallDate => InstallDate;
        public string GetLanguageEdition => LanguageEdition;
        public string[] GetListOfLanguages => ListOfLanguages;
        public string GetManufacturer => Manufacturer;
        public string GetName => Name;
        public string GetOtherTargetOS => OtherTargetOS;
        public bool GetPrimaryBIOS => PrimaryBIOS;
        public string GetReleaseDate => ReleaseDate;
        public string GetSerialNumber => SerialNumber;
        public string GetSMBIOSBIOSVersion => SMBIOSBIOSVersion;
        public UInt16 GetSMBIOSMajorVersion => SMBIOSMajorVersion;
        public UInt16 GetSMBIOSMinorVersion => SMBIOSMinorVersion;
        public bool GetSMBIOSPresent => SMBIOSPresent;
        public string GetSoftwareElementID => SoftwareElementID;
        public UInt16 GetSoftwareElementState => SoftwareElementState;
        public string GetStatus => Status;
        public byte GetSystemBiosMajorVersion => SystemBiosMajorVersion;
        public byte GetSystemBiosMinorVersion => SystemBiosMinorVersion;
        public UInt16 GetTargetOperatingSystem => TargetOperatingSystem;
        public string GetVersion => Version;

        public Win32_Bios(ushort[] biosCharacteristics, string[] bIOSVersion, string buildNumber, string caption, string codeSet, 
            string currentLanguage, string description, byte embeddedControllerMajorVersion, byte embeddedControllerMinorVersion, 
            string identificationCode, ushort installableLanguages, string installDate, string languageEdition, string[] listOfLanguages, 
            string manufacturer, string name, string otherTargetOS, bool primaryBIOS, string releaseDate, string serialNumber, 
            string sMBIOSBIOSVersion, ushort sMBIOSMajorVersion, ushort sMBIOSMinorVersion, bool sMBIOSPresent, string softwareElementID, 
            ushort softwareElementState, string status, byte systemBiosMajorVersion, byte systemBiosMinorVersion, 
            ushort targetOperatingSystem, string version)
        {
            BiosCharacteristics = biosCharacteristics;
            BIOSVersion = bIOSVersion;
            BuildNumber = buildNumber;
            Caption = caption;
            CodeSet = codeSet;
            CurrentLanguage = currentLanguage;
            Description = description;
            EmbeddedControllerMajorVersion = embeddedControllerMajorVersion;
            EmbeddedControllerMinorVersion = embeddedControllerMinorVersion;
            IdentificationCode = identificationCode;
            InstallableLanguages = installableLanguages;
            InstallDate = installDate;
            LanguageEdition = languageEdition;
            ListOfLanguages = listOfLanguages;
            Manufacturer = manufacturer;
            Name = name;
            OtherTargetOS = otherTargetOS;
            PrimaryBIOS = primaryBIOS;
            ReleaseDate = releaseDate;
            SerialNumber = serialNumber;
            SMBIOSBIOSVersion = sMBIOSBIOSVersion;
            SMBIOSMajorVersion = sMBIOSMajorVersion;
            SMBIOSMinorVersion = sMBIOSMinorVersion;
            SMBIOSPresent = sMBIOSPresent;
            SoftwareElementID = softwareElementID;
            SoftwareElementState = softwareElementState;
            Status = status;
            SystemBiosMajorVersion = systemBiosMajorVersion;
            SystemBiosMinorVersion = systemBiosMinorVersion;
            TargetOperatingSystem = targetOperatingSystem;
            Version = version;
        }

        public static Win32_Bios? GetBios()
        {
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_BIOS");
            ManagementObjectSearcher search = new ManagementObjectSearcher(query);

            ManagementObjectCollection moc = search.Get();
            

            foreach (ManagementObject mo in moc.Cast<ManagementObject>())
            {                              
               Win32_Bios Win32_Bios = new Win32_Bios(

                 (UInt16[])mo["BiosCharacteristics"],
                 (string[])mo["BIOSVersion"],
                 !(String.IsNullOrEmpty((string)mo["BuildNumber"])) ? (string)mo["BuildNumber"] : "N/A",
                 !(String.IsNullOrEmpty((string)mo["Caption"])) ? (string)mo["Caption"] : "N/A",
                 !(String.IsNullOrEmpty((string)mo["CodeSet"])) ? (string)mo["CodeSet"] : "N/A",
                 !(String.IsNullOrEmpty((string)mo["CurrentLanguage"])) ? (string)mo["CurrentLanguage"] : "N/A",
                 !(String.IsNullOrEmpty((string)mo["Description"])) ? (string)mo["Description"] : "N/A",               
                 (byte)mo["EmbeddedControllerMajorVersion"],
                 (byte)mo["EmbeddedControllerMinorVersion"],
                 !(String.IsNullOrEmpty((string)mo["IdentificationCode"])) ? (string)mo["IdentificationCode"] : "N/A",                
                 (UInt16)mo["InstallableLanguages"],
                 !(String.IsNullOrEmpty((string)mo["InstallDate"])) ? (string)mo["InstallDate"] : "N/A",
                 !(String.IsNullOrEmpty((string)mo["LanguageEdition"])) ? (string)mo["LanguageEdition"] : "N/A",
                 (string[])mo["ListOfLanguages"],
                 !(String.IsNullOrEmpty((string)mo["Manufacturer"])) ? (string)mo["Manufacturer"] : "N/A",
                 !(String.IsNullOrEmpty((string)mo["Name"])) ? (string)mo["Name"] : "N/A",
                 !(String.IsNullOrEmpty((string)mo["OtherTargetOS"])) ? (string)mo["OtherTargetOS"] : "N/A",
                 (bool)mo["PrimaryBIOS"],
                 !(String.IsNullOrEmpty((string)mo["ReleaseDate"])) ? (string)mo["ReleaseDate"] : "N/A",
                 !(String.IsNullOrEmpty((string)mo["SerialNumber"])) ? (string)mo["SerialNumber"] : "N/A",
                 !(String.IsNullOrEmpty((string)mo["SMBIOSBIOSVersion"])) ? (string)mo["SMBIOSBIOSVersion"] : "N/A",
                 (UInt16)mo["SMBIOSMajorVersion"],
                 (UInt16)mo["SMBIOSMinorVersion"],
                 (bool)mo["SMBIOSPresent"],
                 !(String.IsNullOrEmpty((string)mo["SoftwareElementID"])) ? (string)mo["SoftwareElementID"] : "N/A",
                 (UInt16)mo["SoftwareElementState"],
                 !(String.IsNullOrEmpty((string)mo["Status"])) ? (string)mo["Status"] : "N/A",
                 (byte)mo["SystemBiosMajorVersion"],
                 (byte)mo["SystemBiosMinorVersion"],
                 (UInt16)mo["TargetOperatingSystem"],
                 !(String.IsNullOrEmpty((string)mo["Version"])) ? (string)mo["Version"] : "N/A"          
               );

                return Win32_Bios;
                
            }


            return null;


        }
        

    }
}
