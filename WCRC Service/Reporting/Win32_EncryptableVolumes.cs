using System;
using System.Collections.Generic;
using System.Management;


internal class Win32_EncryptableVolumes
{
    public Win32_EncryptableVolumes(string deviceID, string persistentVolumeID, string driveLetter,
        uint protectionStatus)
    {
        GetDeviceID = deviceID;
        GetPersistentVolumeID = persistentVolumeID;
        GetDriveLetter = driveLetter;
        GetProtectionStatus = protectionStatus;
    }

    public string GetDeviceID { get; }

    public string GetPersistentVolumeID { get; }

    public string GetDriveLetter { get; }

    public uint? GetProtectionStatus { get; }

    public static List<Win32_EncryptableVolumes> GetEncryptableVolume()
    {
        try
        {
            var list = new List<Win32_EncryptableVolumes>();

            var searcher =
                new ManagementObjectSearcher("root\\CIMV2\\Security\\MicrosoftVolumeEncryption",
                    "SELECT * FROM Win32_EncryptableVolume");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                try
                {
                    list.Add(
                    new Win32_EncryptableVolumes(
                        !string.IsNullOrEmpty((string)queryObj["DeviceID"]) ? (string)queryObj["DeviceID"] : "N/A",
                        !string.IsNullOrEmpty((string)queryObj["PersistentVolumeID"])
                            ? (string)queryObj["PersistentVolumeID"]
                            : "N/A",
                        !string.IsNullOrEmpty((string)queryObj["DriveLetter"])
                            ? (string)queryObj["DriveLetter"]
                            : "N/A",
                        (uint)queryObj["ProtectionStatus"]
                    ));
                }
                catch (Exception ex)
                {
                    WCRC.log.LogWrite("Internal error on volumes...");
                    WCRC.log.LogWrite(ex.Message);
                    WCRC.Win32_Error_.EncryptableVolumes_error += 1;

                }
            }
               
            return list;
        }
        catch (Exception ex)
        {
            WCRC.log.LogWrite("Critical error on volumes...");
            WCRC.log.LogWrite(ex.Message);
            WCRC.Win32_Error_.Critical_EncryptableVolumes_error += 1;
            return null;
        }
    }
}