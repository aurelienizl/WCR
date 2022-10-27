#pragma warning disable CA1416 // Valider la compatibilité de la plateforme

using System.Management;


namespace Win32ClassModule
{
    internal class Win32_EncryptableVolume
    {
        private string DeviceID;
        private string PersistentVolumeID;
        private string DriveLetter;
        private UInt32 ProtectionStatus;

        public string GetDeviceID => DeviceID;
        public string GetPersistentVolumeID => PersistentVolumeID;
        public string GetDriveLetter => DriveLetter;
        public UInt32 GetProtectionStatus => ProtectionStatus;

        public Win32_EncryptableVolume(string deviceID, string persistentVolumeID, string driveLetter, uint protectionStatus)
        {
            DeviceID = deviceID;
            PersistentVolumeID = persistentVolumeID;
            DriveLetter = driveLetter;
            ProtectionStatus = protectionStatus;
        }

        public static void GetEncryptableVolume()
        {
            try
            {
                ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("root\\CIMV2\\Security\\MicrosoftVolumeEncryption",
                    "SELECT * FROM Win32_EncryptableVolume");

                foreach (ManagementObject queryObj in searcher.Get())
                {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Win32_EncryptableVolume instance");
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("ProtectionStatus: {0}", queryObj["ProtectionStatus"]);
                }
            }
            catch (ManagementException e)
            {
                //MessageBox.Show("An error occurred while querying for WMI data: " + e.Message);
            }

        }
    }
}
