using System;
using System.Collections.Generic;
using System.Management;


internal class Win32_EncryptableVolumes
{
    public Win32_EncryptableVolumes(string deviceID, string persistentVolumeID, string driveLetter,
        string protectionStatus)
    {
        GetDeviceID = deviceID;
        GetPersistentVolumeID = persistentVolumeID;
        GetDriveLetter = driveLetter;
        GetProtectionStatus = protectionStatus;
    }

    public string GetDeviceID { get; }

    public string GetPersistentVolumeID { get; }

    public string GetDriveLetter { get; }

    public string GetProtectionStatus { get; }

    public static string QuerySafeGetter(ManagementObject obj, string query)
    {
        try
        {
            if (!String.IsNullOrEmpty(obj[query].ToString()))
            {
                return obj[query].ToString();
            }

            string res = (string)obj[query];
            if (!String.IsNullOrEmpty(res))
            {
                return res;
            }
            return "N/A";
        }
        catch (Exception)
        {
            return "N/A";
        }
    }

    public static List<Win32_EncryptableVolumes> GetEncryptableVolume()
    {
        var list = new List<Win32_EncryptableVolumes>();

        try
        {
            var searcher =
                new ManagementObjectSearcher("root\\CIMV2\\Security\\MicrosoftVolumeEncryption",
                    "SELECT * FROM Win32_EncryptableVolume");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                try
                {
                    list.Add(
                    new Win32_EncryptableVolumes(
                        QuerySafeGetter(queryObj, "DeviceID"),
                         QuerySafeGetter(queryObj, "PersistentVolumeID"),
                          QuerySafeGetter(queryObj, "DriveLetter"),
                           QuerySafeGetter(queryObj, "ProtectionStatus")
      
                    ));
                }
                catch (Exception)
                {

                }
            }
            WCRC.log.LogWrite("Got volumes successfully");
            return list;
        }
        catch (Exception )
        {
            WCRC.log.LogWrite("Error : volumes");
            return list;
        }
    }
}