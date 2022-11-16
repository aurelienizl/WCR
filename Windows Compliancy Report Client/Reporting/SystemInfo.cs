using System.Management;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.RegularExpressions;

#pragma warning disable CA1416 // Valider la compatibilité de la plateforme
// see https://github.com/quasar


namespace Windows_Compliancy_Report_Client;

internal class SystemInfo
{
    public SystemInfo(string osVersion, string biosManufacturer, string mainboardName, string cpuName,
        int totalPhysicalMemoryInMb, string gpuName, string lanIpAddress,
        string macAddress, string timeDate, string hardwareID)
    {
        GetOsVersion = osVersion;
        GetBiosManufacturer = biosManufacturer;
        GetMainboardName = mainboardName;
        GetCpuName = cpuName;
        GetTotalPhysicalMemoryInMb = totalPhysicalMemoryInMb;
        GetGpuName = gpuName;
        GetLanIpAddress = lanIpAddress;
        GetMacAddress = macAddress;
        GetTimeDate = timeDate;
        GetHardwareID = hardwareID;
    }

    public string GetOsVersion { get; }

    public string GetBiosManufacturer { get; }

    public string GetMainboardName { get; }

    public string GetCpuName { get; }

    public int GetTotalPhysicalMemoryInMb { get; }

    public string GetGpuName { get; }

    public string GetLanIpAddress { get; }

    public string GetMacAddress { get; }

    public string GetTimeDate { get; }

    public string GetHardwareID { get; }

    public static SystemInfo? GetSystemInfo()
    {
        try
        {
            return new SystemInfo(
                GetOsVersion_(),
                GetBiosManufacturer_(),
                GetMainboardName_(),
                GetCpuName_(),
                GetTotalPhysicalMemoryInMb_(),
                GetGpuName_(),
                GetLanIpAddress_(),
                GetMacAddress_(),
                DateTime.Now.ToString("dd/M/yyyy"),
                Cryptography.Sha265.ComputeSha256Hash(GetCpuName_() + GetMainboardName_() + GetBiosManufacturer_())
            );
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static string RemoveLastChars(string input, int amount = 2)
    {
        if (input.Length > amount) input = input.Remove(input.Length - amount);
        return input;
    }

    private static string GetOsVersion_()
    {
        try
        {
            return Environment.OSVersion.ToString();
        }
        catch (Exception)
        {
            return "Unknown";
        }
    }

    private static string GetBiosManufacturer_()
    {
        try
        {
            var biosIdentifier = string.Empty;
            var query = "SELECT * FROM Win32_BIOS";

            using (var searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject mObject in searcher.Get())
                {
                    biosIdentifier = mObject["Manufacturer"].ToString();
                    break;
                }
            }

            return !string.IsNullOrEmpty(biosIdentifier) ? biosIdentifier : "N/A";
        }
        catch
        {
            return "Unknown";
        }
    }

    private static string GetMainboardName_()
    {
        try
        {
            var mainboardIdentifier = string.Empty;
            var query = "SELECT * FROM Win32_BaseBoard";

            using (var searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject mObject in searcher.Get())
                {
                    mainboardIdentifier = mObject["Manufacturer"] + " " + mObject["Product"];
                    break;
                }
            }

            return !string.IsNullOrEmpty(mainboardIdentifier) ? mainboardIdentifier : "N/A";
        }
        catch
        {
            return "Unknown";
        }
    }

    private static string GetCpuName_()
    {
        try
        {
            var cpuName = string.Empty;
            var query = "SELECT * FROM Win32_Processor";

            using (var searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject mObject in searcher.Get()) cpuName += mObject["Name"] + "; ";
            }

            cpuName = RemoveLastChars(cpuName);

            return !string.IsNullOrEmpty(cpuName) ? cpuName : "N/A";
        }
        catch
        {
            return "Unknown";
        }
    }

    private static int GetTotalPhysicalMemoryInMb_()
    {
        try
        {
            var installedRAM = 0;
            var query = "Select * From Win32_ComputerSystem";

            using (var searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject mObject in searcher.Get())
                {
                    var bytes = Convert.ToDouble(mObject["TotalPhysicalMemory"]);
                    installedRAM = (int)(bytes / 1048576); // bytes to MB
                    break;
                }
            }

            return installedRAM;
        }
        catch
        {
            return -1;
        }
    }

    private static string GetGpuName_()
    {
        try
        {
            var gpuName = string.Empty;
            var query = "SELECT * FROM Win32_DisplayConfiguration";

            using (var searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject mObject in searcher.Get()) gpuName += mObject["Description"] + "; ";
            }

            gpuName = RemoveLastChars(gpuName);

            return !string.IsNullOrEmpty(gpuName) ? gpuName : "N/A";
        }
        catch
        {
            return "Unknown";
        }
    }

    private static string GetLanIpAddress_()
    {
        foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
        {
            var gatewayAddress = ni.GetIPProperties().GatewayAddresses.FirstOrDefault();
            if (gatewayAddress != null) //exclude virtual physical nic with no default gateway
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                    (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                     ni.OperationalStatus == OperationalStatus.Up))
                    foreach (var ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily != AddressFamily.InterNetwork ||
                            ip.AddressPreferredLifetime == uint.MaxValue)
                            // exclude virtual network addresses
                            continue;
                        return ip.Address.ToString();
                    }
        }

        return "-";
    }

    private static string GetFormattedMacAddress(string macAddress)
    {
        return macAddress.Length != 12
            ? "00:00:00:00:00:00"
            : Regex.Replace(macAddress, "(.{2})(.{2})(.{2})(.{2})(.{2})(.{2})", "$1:$2:$3:$4:$5:$6");
    }


    private static string GetMacAddress_()
    {
        foreach (var ni in NetworkInterface.GetAllNetworkInterfaces())
            if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 ||
                (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                 ni.OperationalStatus == OperationalStatus.Up))
            {
                var foundCorrect = false;
                foreach (var ip in ni.GetIPProperties().UnicastAddresses)
                {
                    if (ip.Address.AddressFamily != AddressFamily.InterNetwork ||
                        ip.AddressPreferredLifetime == uint.MaxValue) // exclude virtual network addresses
                        continue;
                    foundCorrect = ip.Address.ToString() == GetLanIpAddress_();
                }

                if (foundCorrect) return GetFormattedMacAddress(ni.GetPhysicalAddress().ToString());
            }


        return "-";
    }
}