using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using WCRC_Service.Modules;

public class Win32_Tpm
{
    public Win32_Tpm(bool isActivated_InitialValue, bool isEnabled_InitialValue,
        bool isOwned_InitialValue, string specVersion, string manufacturerVersion,
        string manufacturerVersionInfo, uint manufacturerId, string physicalPresenceVersionInfo)
    {
        GetIsActivated_InitialValue = isActivated_InitialValue;
        GetIsEnabled_InitialValue = isEnabled_InitialValue;
        GetIsOwned_InitialValue = isOwned_InitialValue;
        GetSpecVersion = specVersion;
        GetManufacturerVersion = manufacturerVersion;
        GetManufacturerVersionInfo = manufacturerVersionInfo;
        GetManufacturerId = manufacturerId;
        GetPhysicalPresenceVersionInfo = physicalPresenceVersionInfo;
    }

    public bool GetIsActivated_InitialValue { get; }

    public bool GetIsEnabled_InitialValue { get; }

    public bool GetIsOwned_InitialValue { get; }

    public string GetSpecVersion { get; }

    public string GetManufacturerVersion { get; }

    public string GetManufacturerVersionInfo { get; }

    public uint GetManufacturerId { get; }

    public string GetPhysicalPresenceVersionInfo { get; }

    public static List<Win32_Tpm> GetTpm()
    {
        var list = new List<Win32_Tpm>();

        try
        {
            var searcher =
                new ManagementObjectSearcher("root\\CIMV2\\Security\\MicrosoftTpm",
                    "SELECT * FROM Win32_Tpm");

            foreach (var queryObj in searcher.Get().Cast<ManagementObject>())
                try
                {
                    list.Add(new Win32_Tpm(
                        (bool)queryObj["IsActivated_InitialValue"],
                        (bool)queryObj["IsEnabled_InitialValue"],
                        (bool)queryObj["IsOwned_InitialValue"],
                        !string.IsNullOrEmpty((string)queryObj["SpecVersion"])
                            ? (string)queryObj["SpecVersion"]
                            : "N/A",
                        !string.IsNullOrEmpty((string)queryObj["ManufacturerVersion"])
                            ? (string)queryObj["ManufacturerVersion"]
                            : "N/A",
                        !string.IsNullOrEmpty((string)queryObj["ManufacturerVersionInfo"])
                            ? (string)queryObj["ManufacturerVersionInfo"]
                            : "N/A",
                        (uint)queryObj["ManufacturerId"],
                        !string.IsNullOrEmpty((string)queryObj["PhysicalPresenceVersionInfo"])
                            ? (string)queryObj["PhysicalPresenceVersionInfo"]
                            : "N/A"
                    ));
                }
                catch (Exception)
                {
                    // ignored
                }

            Logs.LogWrite("Got tpms successfully");

            return list;
        }
        catch (Exception)
        {
            Logs.LogWrite("Error : tpms");

            return list;
        }
    }
}