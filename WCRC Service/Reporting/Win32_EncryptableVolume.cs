using System;
using System.Collections.Generic;
using System.Management;
using WCRC_Service.Modules;


internal class Win32_EncryptableVolume
{
    public Win32_EncryptableVolume(string deviceID, string persistentVolumeID, string driveLetter,
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

    public static List<Win32_EncryptableVolume> GetEncryptableVolume()
    {
        var list = new List<Win32_EncryptableVolume>();

        try
        {
            var searcher =
                new ManagementObjectSearcher("root\\CIMV2\\Security\\MicrosoftVolumeEncryption",
                    "SELECT * FROM Win32_EncryptableVolume");

            foreach (var o in searcher.Get())
            {
                var queryObj = (ManagementObject)o;
                try
                {
                    list.Add(
                    new Win32_EncryptableVolume(
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
            Logs.LogWrite("Got volumes successfully");
            return list;
        }
        catch (Exception )
        {
            Logs.LogWrite("Error : volumes");
            return list;
        }
    }
}