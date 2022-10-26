using System.Management;

#pragma warning disable CA1416 // Valider la compatibilité de la plateforme

namespace Iedom_Client
{
    internal class Win32_Tpm
    {
        private  bool IsActivated_InitialValue;
        private  bool IsEnabled_InitialValue;
        private  bool IsOwned_InitialValue;
        private  string? SpecVersion;
        private  string? ManufacturerVersion;
        private  string? ManufacturerVersionInfo;
        private  UInt32 ManufacturerId;
        private  string? PhysicalPresenceVersionInfo;

        public bool GetIsActivated_InitialValue => IsActivated_InitialValue;
        public bool GetIsEnabled_InitialValue => IsEnabled_InitialValue;
        public bool GetIsOwned_InitialValue => IsOwned_InitialValue;
        public string? GetSpecVersion => SpecVersion;
        public string? GetManufacturerVersion => ManufacturerVersion;
        public string? GetManufacturerVersionInfo => ManufacturerVersionInfo;
        public UInt32 GetManufacturerId => ManufacturerId;
        public string? GetPhysicalPresenceVersionInfo => PhysicalPresenceVersionInfo;

        public Win32_Tpm(bool isActivated_InitialValue, bool isEnabled_InitialValue, 
            bool isOwned_InitialValue, string? specVersion, string? manufacturerVersion, 
            string? manufacturerVersionInfo, uint manufacturerId, string? physicalPresenceVersionInfo)
        {
            IsActivated_InitialValue = isActivated_InitialValue;
            IsEnabled_InitialValue = isEnabled_InitialValue;
            IsOwned_InitialValue = isOwned_InitialValue;
            SpecVersion = specVersion;
            ManufacturerVersion = manufacturerVersion;
            ManufacturerVersionInfo = manufacturerVersionInfo;
            ManufacturerId = manufacturerId;
            PhysicalPresenceVersionInfo = physicalPresenceVersionInfo;
        }

        public static Win32_Tpm GetTpm()
        {
            ManagementObject managementObject = new ManagementObject(@"ROOT\CIMV2\Security\MicrosoftTpm", "Win32_Tpm=@", null);

            Win32_Tpm tpm = new Win32_Tpm(

                (bool)managementObject.GetPropertyValue("IsActivated_InitialValue"),
                (bool)managementObject.GetPropertyValue("IsEnabled_InitialValue"),
                (bool)managementObject.GetPropertyValue("IsOwned_InitialValue"),

                (!string.IsNullOrEmpty((string)managementObject.GetPropertyValue("SpecVersion"))) ?
                (string)managementObject.GetPropertyValue("SpecVersion") : "N/A",

                (!string.IsNullOrEmpty((string)managementObject.GetPropertyValue("ManufacturerVersion"))) ?
                (string)managementObject.GetPropertyValue("ManufacturerVersion") : "N/A",

                (!string.IsNullOrEmpty((string)managementObject.GetPropertyValue("ManufacturerVersionInfo"))) ?
                (string)managementObject.GetPropertyValue("ManufacturerVersionInfo") : "N/A",

                (UInt32)managementObject.GetPropertyValue("manufacturerId"),

                (!string.IsNullOrEmpty((string)managementObject.GetPropertyValue("PhysicalPresenceVersionInfo"))) ?
                (string)managementObject.GetPropertyValue("PhysicalPresenceVersionInfo") : "N/A"
                
                );

            return tpm;
      
        }

       
    }
}

#pragma warning restore CA1416 // Valider la compatibilité de la plateforme